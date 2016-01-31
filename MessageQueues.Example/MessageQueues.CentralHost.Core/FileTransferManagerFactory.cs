using MessageQueues.Core;

namespace MessageQueues.CentralHost.Core
{
    public sealed class FileTransferManagerFactory
    {
        private readonly SerializationAssistantFactory _serializationAssistantFactory;
        private readonly IChannelProvider _channelProvider;

        public FileTransferManagerFactory(
            SerializationAssistantFactory serializationAssistantFactory,
            IChannelProvider channelProvider)
        {
            _serializationAssistantFactory = serializationAssistantFactory;
            _channelProvider = channelProvider;
        }

        public IFileTransferManager Create()
        {
            return new FileTransferManager(_serializationAssistantFactory.Create(), _channelProvider.GetChannel(), Queues.Files);
        }
    }
}
