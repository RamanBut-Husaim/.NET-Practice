using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing;

namespace MessageQueues.Core
{
    public sealed class TransferManager
    {
        private readonly string _queueName;
        private readonly IModel _channel;
        private readonly BinaryFormatter _binaryFormatter;

        public TransferManager(IModel channel, string queueName)
        {
            _queueName = queueName;
            _channel = channel;
            _binaryFormatter = new BinaryFormatter();
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
    }
}
