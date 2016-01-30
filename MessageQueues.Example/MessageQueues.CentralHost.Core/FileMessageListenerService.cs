using System;

using MessageQueues.Core;
using MessageQueues.Core.Events;
using MessageQueues.Core.Messages;

namespace MessageQueues.CentralHost.Core
{
    public sealed class FileMessageListenerService
    {
        private readonly EventingSerializationBasicConsumer<FileMessage> _consumer;
        private readonly IFileTransferManager _fileTransferManager;
        private readonly IFileMessageDispatcher _fileMessageDispatcher;

        public FileMessageListenerService(
            EventingSerializationBasicConsumer<FileMessage> consumer,
            IFileTransferManager fileTransferManager,
            IFileMessageDispatcher fileMessageDispatcher)
        {
            _consumer = consumer;
            _fileTransferManager = fileTransferManager;
            _fileMessageDispatcher = fileMessageDispatcher;
        }

        public void StartProcessing()
        {
            _consumer.Received += this.ConsumerOnReceived;
            _fileTransferManager.Receive(_consumer);
        }

        public void RequestShutdown(TimeSpan waitTime)
        {
            _consumer.Received -= this.ConsumerOnReceived;
            _fileMessageDispatcher.CompleteProcessing(waitTime);
        }

        private void ConsumerOnReceived(object sender, BasicSerializedDeliveryEventArgs<FileMessage> basicSerializedDeliveryEventArgs)
        {
            _fileMessageDispatcher.EnqueueOperation(basicSerializedDeliveryEventArgs);
        }
    }
}
