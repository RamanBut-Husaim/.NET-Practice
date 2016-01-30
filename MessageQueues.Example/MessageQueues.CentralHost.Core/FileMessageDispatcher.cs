using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MessageQueues.Core.Events;
using MessageQueues.Core.Messages;

namespace MessageQueues.CentralHost.Core
{
    public sealed class FileMessageDispatcher : IFileMessageDispatcher, IDisposable
    {
        private readonly object _syncObject;
        private readonly IDictionary<string, IHarvesterProcessingDispatcher> _harvesterProcessingDispatchers;
        private readonly HarvesterProcessingDispatcherFactory _harvesterDispatcherFactory;

        private bool _disposed;

        public FileMessageDispatcher(HarvesterProcessingDispatcherFactory harvesterProcessingDispatcher)
        {
            _syncObject = new object();
            _harvesterProcessingDispatchers = new Dictionary<string, IHarvesterProcessingDispatcher>();
            _harvesterDispatcherFactory = harvesterProcessingDispatcher;
        }

        public void EnqueueOperation(BasicSerializedDeliveryEventArgs<FileMessage> operation)
        {
            this.GuardDisposed();

            if (operation == null)
            {
                throw new ArgumentNullException("operation");
            }

            lock (_syncObject)
            {
                IHarvesterProcessingDispatcher harvesterDispatcher;
                string harvesterName = operation.Body.Harvester;
                if (!_harvesterProcessingDispatchers.TryGetValue(harvesterName, out harvesterDispatcher))
                {
                    harvesterDispatcher = _harvesterDispatcherFactory.Create(harvesterName);
                    _harvesterProcessingDispatchers.Add(harvesterName, harvesterDispatcher);
                }

                harvesterDispatcher.EnqueueOperation(operation);
            }
        }

        public void CompleteProcessing(TimeSpan waitPeriod)
        {
            this.GuardDisposed();

            lock (_syncObject)
            {
                var harvesterDispatchersCompletionRoutines = _harvesterProcessingDispatchers.Values.Select(p => p.CompleteAsync()).ToArray();
                Task.WaitAll(harvesterDispatchersCompletionRoutines, waitPeriod);
            }
        }

        private void GuardDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("FileMessageDispatcher");
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
                        foreach (var harvesterProcessingDispatcher in _harvesterProcessingDispatchers)
                        {
                            var dispatcher = harvesterProcessingDispatcher.Value;
                            _harvesterDispatcherFactory.Release(dispatcher);
                        }
                    }

                    _disposed = true;
                }
            }
        }
    }
}
