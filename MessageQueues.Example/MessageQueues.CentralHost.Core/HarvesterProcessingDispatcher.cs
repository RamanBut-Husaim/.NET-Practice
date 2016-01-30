using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MessageQueues.Core.Events;
using MessageQueues.Core.Messages;

namespace MessageQueues.CentralHost.Core
{
    public sealed class HarvesterProcessingDispatcher : IHarvesterProcessingDispatcher, IDisposable
    {
        private readonly object _syncObject;

        private readonly string _harvesterName;
        private readonly ManualResetEventSlim _shutdownEvent;
        private readonly Queue<BasicSerializedDeliveryEventArgs<FileMessage>> _operationQueue;
        private readonly Func<IFileBatchManager> _fileBatchManagerFactory;
        private readonly Task _dispatcher;

        private bool _disposed;

        public HarvesterProcessingDispatcher(Func<IFileBatchManager> fileBatchManagerFactory, string harvesterName)
        {
            _harvesterName = harvesterName;
            _fileBatchManagerFactory = fileBatchManagerFactory;

            _operationQueue = new Queue<BasicSerializedDeliveryEventArgs<FileMessage>>();
            _shutdownEvent = new ManualResetEventSlim(false);
            _disposed = true;

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

            lock (_syncObject)
            {
                _operationQueue.Enqueue(operation);

                Monitor.Pulse(_syncObject);
            }
        }

        public async Task CompleteAsync()
        {
            this.GuardDisposed();

            _shutdownEvent.Set();

            await _dispatcher;
        }

        private void Processing()
        {
            while (true)
            {
                IEnumerable<BasicSerializedDeliveryEventArgs<FileMessage>> operations;

                lock (_syncObject)
                {
                    while (_operationQueue.Count == 0)
                    {
                        if (_shutdownEvent.IsSet)
                        {
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
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _shutdownEvent.Dispose();
                    }

                    _disposed = true;
                }
            }
        }
    }
}
