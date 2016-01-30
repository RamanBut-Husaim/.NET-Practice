namespace MessageQueues.Core
{
    public interface ISerializationAssistant
    {
        byte[] Serialize<T>(T obj) where T : TransferableModel;

        T Deserialize<T>(byte[] objBytes) where T : TransferableModel;
    }
}
