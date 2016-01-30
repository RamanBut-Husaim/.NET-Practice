namespace MessageQueues.Core
{
    public interface IFileTransferManager
    {
        void Send<T>(T obj) where T : TransferableModel;

        void Receive<T>(EventingSerializationBasicConsumer<T> consumer) where T : TransferableModel;

        void Acknowledge(ulong deliveryTag);
    }
}
