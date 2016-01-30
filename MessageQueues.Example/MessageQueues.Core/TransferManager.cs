using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using RabbitMQ.Client;

namespace MessageQueues.Core
{
    public sealed class TransferManager : ITransferManager
    {
        private readonly string _queueName;
        private readonly IModel _channel;
        private readonly BinaryFormatter _binaryFormatter;

        public TransferManager(IModel channel, string queueName)
        {
            _queueName = queueName;
            _channel = channel;
            _binaryFormatter = new BinaryFormatter();
            this.DeclareQueue();
        }

        public void Send<T>(T obj) where T : TransferableModel
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            byte[] objectBytes = new byte[0];

            using (var memoryStream = new MemoryStream())
            {
                _binaryFormatter.Serialize(memoryStream, obj);
                objectBytes = memoryStream.ToArray();
            }

            var basicProperties = _channel.CreateBasicProperties();
            basicProperties.Persistent = true;

            _channel.BasicPublish("", _queueName, basicProperties, objectBytes);
        }

        private void DeclareQueue()
        {
            _channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }
    }
}
