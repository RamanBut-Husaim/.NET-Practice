using MessageQueues.Core;
using RabbitMQ.Client;

namespace MessageQueues.HarvesterHost.Core.FileOperations
{
    public sealed class FileSenderFactory
    {
        private readonly IConnectionManager _connectionManager;
        private readonly SerializationAssistantFactory _serializationAssistantFactory;
        private readonly string _harvesterName;

        public FileSenderFactory(
            IConnectionManager connectionManager,
            SerializationAssistantFactory serializationAssistantFactory,
            string harvesterName)
        {
            _connectionManager = connectionManager;
            _serializationAssistantFactory = serializationAssistantFactory;
            _harvesterName = harvesterName;
        }

        public IFileSender Create()
        {
            IModel channel = _connectionManager.GetChannel();

            var transferManager = new FileTransferManager(_serializationAssistantFactory.Create(), channel, Queues.Files);
            var fileSender = new FileSender(transferManager, _harvesterName);

            return fileSender;
        }
    }
}
