using System;

using MessageQueues.Core;
using MessageQueues.Core.Events;
using MessageQueues.Core.Messages;

namespace MessageQueues.CentralHost.Core
{
    public sealed class FileMessageListenerService : IDisposable
    {
        private readonly EventingSerializationBasicConsumer<FileMessage> _consumer;
        private readonly IFileTransferManager _fileTransferManager;
        private readonly FileMessageDispatcherFactory _fileMessageDispatcherFactory;
        private readonly IFileMessageDispatcher _fileMessageDispatcher;

        private bool _disposed;

        public FileMessageListenerService(
            EventingSerializationBasicConsumer<FileMessage> consumer,
            IFileTransferManager fileTransferManager,
            FileMessageDispatcherFactory fileMessageDispatcherFactory)
        {
            _consumer = consumer;
            _fileTransferManager = fileTransferManager;
            _fileMessageDispatcherFactory = fileMessageDispatcherFactory;
            _fileMessageDispatcher = _fileMessageDispatcherFactory.Create();
        }

        public void StartProcessing()
        {
            this.GuardDisposed();

            _consumer.Received += this.ConsumerOnReceived;
            _fileTransferManager.Receive(_consumer);
        }

        public void RequestShutdown(TimeSpan waitTime)
        {
            this.GuardDisposed();

            _consumer.Received -= this.ConsumerOnReceived;
            _fileMessageDispatcher.CompleteProcessing(waitTime);
        }

        private void GuardDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("FileMessageListenerService");
            }
        }

        private void ConsumerOnReceived(object sender, BasicSerializedDeliveryEventArgs<FileMessage> basicSerializedDeliveryEventArgs)
        {
            _fileMessageDispatcher.EnqueueOperation(basicSerializedDeliveryEventArgs);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _fileMessageDispatcherFactory.Release(_fileMessageDispatcher);
                }

                _disposed = true;
            }
        }
    }
}
