using System;

using RabbitMQ.Client;

namespace MessageQueues.Core.StateManagement
{
    public sealed class PubSubManager : IPubSubManager
    {
        private readonly object _sync;

        private readonly string _exchangeName;
        private readonly IModel _channel;
        private readonly ISerializationAssistant _serializationAssistant;

        public PubSubManager(string exchangeName, IModel channel, ISerializationAssistant serializationAssistant)
        {
            _exchangeName = exchangeName;
            _channel = channel;
            _serializationAssistant = serializationAssistant;

            _sync = new object();
        }

        public void Publish<T>(T obj) where T : TransferableModel
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            lock (_sync)
            {
                this.DeclareExchange();

                var serializedMessage = _serializationAssistant.Serialize(obj);
                _channel.BasicPublish(_exchangeName, string.Empty, null, serializedMessage);
            }
        }

        public void Subscribe<T>(EventingSerializationBasicConsumer<T> consumer) where T: TransferableModel
        {
            if (consumer == null)
            {
                throw new ArgumentNullException("consumer");
            }

            lock (_sync)
            {
                this.DeclareExchange();

                var queueName = _channel.QueueDeclare().QueueName;
                _channel.QueueBind(queue: queueName, exchange: _exchangeName, routingKey: string.Empty);

                _channel.BasicConsume(queue: queueName, noAck: true, consumer: consumer);
            }
        }

        private void DeclareExchange()
        {
            _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Fanout);
        }
    }
}
