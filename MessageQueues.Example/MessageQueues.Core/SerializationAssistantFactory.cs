namespace MessageQueues.Core
{
    public sealed class SerializationAssistantFactory
    {
        public ISerializationAssistant Create()
        {
            return new SerializationAssistant();
        }
    }
}
