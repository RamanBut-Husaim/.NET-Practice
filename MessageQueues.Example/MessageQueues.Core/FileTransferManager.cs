using RabbitMQ.Client;

namespace MessageQueues.Core
{
    public sealed class FileTransferManager : IFileTransferManager
    {
        private readonly object _syncObject;

        private readonly string _queueName;
        private readonly IModel _channel;
        private readonly ISerializationAssistant _serializationAssistant;

        public FileTransferManager(ISerializationAssistant serializationAssistant, IModel channel, string queueName)
        {
            _syncObject = new object();
            _queueName = queueName;
            _channel = channel;
            _serializationAssistant = serializationAssistant;

            this.DeclareQueue();
        }

        public void Send<T>(T obj) where T : TransferableModel
        {
            byte[] objectBytes = _serializationAssistant.Serialize(obj);

            lock (_syncObject)
            {
                var basicProperties = _channel.CreateBasicProperties();
                basicProperties.Persistent = true;

                _channel.BasicPublish(string.Empty, _queueName, basicProperties, objectBytes);
            }
        }

        public void Receive<T>(EventingSerializationBasicConsumer<T> consumer) where T : TransferableModel
        {
            lock (_syncObject)
            {
                _channel.BasicConsume(_queueName, noAck: false, consumer: consumer);
            }
        }

        public void Acknowledge(ulong deliveryTag)
        {
            lock (_syncObject)
            {
                _channel.BasicAck(deliveryTag, false);
            }
        }

        private void DeclareQueue()
        {
            _channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }
    }
}
