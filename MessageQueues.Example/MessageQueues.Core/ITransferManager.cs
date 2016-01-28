namespace MessageQueues.Core
{
    public interface ITransferManager
    {
        void Send<T>(T obj) where T : TransferableModel;
    }
}
