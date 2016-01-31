using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MessageQueues.Core.Events;
using MessageQueues.Core.Messages;

using NLog;

namespace MessageQueues.CentralHost.Core
{
    public sealed class HarvesterProcessingDispatcher : IHarvesterProcessingDispatcher, IDisposable
    {
        private readonly object _syncObject;

        private readonly string _harvesterName;
        private readonly ManualResetEventSlim _shutdownEvent;
        private readonly Queue<BasicSerializedDeliveryEventArgs<FileMessage>> _operationQueue;
        private readonly Func<IFileBatchManager> _fileBatchManagerFactory;
        private readonly ILogger _logger;
        private readonly Task _dispatcher;

        private bool _disposed;

        public HarvesterProcessingDispatcher(ILogger logger, Func<IFileBatchManager> fileBatchManagerFactory, string harvesterName)
        {
            _logger = logger;
            _harvesterName = harvesterName;
            _fileBatchManagerFactory = fileBatchManagerFactory;

            _operationQueue = new Queue<BasicSerializedDeliveryEventArgs<FileMessage>>();
            _shutdownEvent = new ManualResetEventSlim(false);
            _shutdownEvent.Reset();

            _dispatcher = new Task(this.Processing, TaskCreationOptions.LongRunning);
            _dispatcher.Start();

            _syncObject = new object();
        }

        public string HarvesterName
        {
            get { return _harvesterName; }
        }


        public void EnqueueOperation(BasicSerializedDeliveryEventArgs<FileMessage> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException("operation");
            }

            this.GuardDisposed();

            _logger.Trace("[Start]: Harvester [{0}] enqueue operation with message ID {1}", _harvesterName, operation.BasicProperties.MessageId);
            lock (_syncObject)
            {
                _operationQueue.Enqueue(operation);

                Monitor.Pulse(_syncObject);
            }

            _logger.Trace("[End]: Harvester [{0}] enqueue operation with message ID {1}", _harvesterName, operation.BasicProperties.MessageId);
        }

        public async Task CompleteAsync()
        {
            this.GuardDisposed();

            _shutdownEvent.Set();

            await _dispatcher;
        }

        private void Processing()
        {
            _logger.Trace("[Start]: Harvester [{0}] start processing", _harvesterName);

            while (true)
            {
                IEnumerable<BasicSerializedDeliveryEventArgs<FileMessage>> operations;

                lock (_syncObject)
                {
                    while (_operationQueue.Count == 0)
                    {
                        if (_shutdownEvent.IsSet)
                        {
                            _logger.Trace("[End]: Harvester [{0}] start processing", _harvesterName);
                            return;
                        }

                        Monitor.Wait(_syncObject, TimeSpan.FromSeconds(5));
                    }

                    operations = _operationQueue.ToList();
                    _operationQueue.Clear();

                    Monitor.Pulse(_syncObject);
                }

                IFileBatchManager fileBatchManager = _fileBatchManagerFactory.Invoke();
                fileBatchManager.Process(operations);
            }
        }

        private void GuardDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("HarvesterProcessingDispatcher");
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            lock (_syncObject)
            {
                _logger.Trace("[Start]: Harvester [{0}] disposing", _harvesterName);
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _shutdownEvent.Dispose();
                    }

                    _disposed = true;
                }

                _logger.Trace("[End]: Harvester [{0}] disposing", _harvesterName);
            }
        }
    }
}
