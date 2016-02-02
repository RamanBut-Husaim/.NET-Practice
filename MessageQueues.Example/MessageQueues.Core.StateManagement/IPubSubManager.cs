namespace MessageQueues.Core.StateManagement
{
    public interface IPubSubManager
    {
        void Publish<T>(T obj) where T : TransferableModel;

        void Subscribe<T>(EventingSerializationBasicConsumer<T> consumer) where T : TransferableModel;
    }
}
