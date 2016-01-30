using MessageQueues.Core;
using RabbitMQ.Client;

namespace MessageQueues.HarvesterHost.Core.FileOperations
{
    public sealed class FileSenderFactory
    {
        private readonly IConnectionManager _connectionManager;

        public FileSenderFactory(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public IFileSender Create()
        {
            IModel channel = _connectionManager.GetChannel();

            var transferManager = new TransferManager(channel, Queues.Files);
            var fileSender = new FileSender(transferManager, "harvester");

            return fileSender;
        }

    }
}
