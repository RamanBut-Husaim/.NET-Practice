using MessageQueues.Core;
using RabbitMQ.Client;

namespace MessageQueues.HarvesterHost.Core.FileOperations
{
    public sealed class FileSenderFactory
    {
        private readonly IConnectionManager _connectionManager;
        private readonly SerializationAssistantFactory _serializationAssistantFactory;

        public FileSenderFactory(IConnectionManager connectionManager, SerializationAssistantFactory serializationAssistantFactory)
        {
            _connectionManager = connectionManager;
            _serializationAssistantFactory = serializationAssistantFactory;
        }

        public IFileSender Create()
        {
            IModel channel = _connectionManager.GetChannel();

            var transferManager = new FileTransferManager(_serializationAssistantFactory.Create(), channel, Queues.Files);
            var fileSender = new FileSender(transferManager, "harvester");

            return fileSender;
        }
    }
}
