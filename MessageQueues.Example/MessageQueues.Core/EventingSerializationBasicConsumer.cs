using System;

using MessageQueues.Core.Events;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageQueues.Core
{
    public sealed class EventingSerializationBasicConsumer<T> : DefaultBasicConsumer where T: TransferableModel
    {
        private readonly SerializationAssistantFactory _serializationAssistantFactory;

        public EventingSerializationBasicConsumer(IModel channel, SerializationAssistantFactory serializationAssistantFactory) : base(channel)
        {
            _serializationAssistantFactory = serializationAssistantFactory;
        }

        /// <summary>
        /// Event fired on HandleBasicDeliver.
        /// </summary>
        public event EventHandler<BasicSerializedDeliveryEventArgs<T>> Received = delegate { };

        /// <summary>
        /// Event fired on HandleBasicConsumeOk.
        /// </summary>
        public event EventHandler<ConsumerEventArgs> Registered = delegate { };

        /// <summary>
        /// Event fired on HandleModelShutdown.
        /// </summary>
        public event EventHandler<ShutdownEventArgs> Shutdown = delegate { };

        /// <summary>
        /// Event fired on HandleBasicCancelOk.
        /// </summary>
        public event EventHandler<ConsumerEventArgs> Unregistered = delegate { };

        /// <summary>
        /// Fires the Unregistered event.
        /// </summary>
        public override void HandleBasicCancelOk(string consumerTag)
        {
            base.HandleBasicCancelOk(consumerTag);
            this.Unregistered(this, new ConsumerEventArgs(consumerTag));
        }

        /// <summary>
        /// Fires the Registered event.
        /// </summary>
        public override void HandleBasicConsumeOk(string consumerTag)
        {
            base.HandleBasicConsumeOk(consumerTag);
            this.Registered(this, new ConsumerEventArgs(consumerTag));
        }

        /// <summary>
        /// Fires the Received event.
        /// </summary>
        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {
            base.HandleBasicDeliver(consumerTag, deliveryTag, redelivered, exchange, routingKey, properties, body);

            var serializationAssistant = _serializationAssistantFactory.Create();
            T obj = serializationAssistant.Deserialize<T>(body);

            this.Received(this, new BasicSerializedDeliveryEventArgs<T>(consumerTag, deliveryTag, redelivered, exchange, routingKey, properties, obj));
        }

        /// <summary>
        /// Fires the Shutdown event.
        /// </summary>
        public override void HandleModelShutdown(object model, ShutdownEventArgs reason)
        {
            base.HandleModelShutdown(model, reason);
            this.Shutdown(this, reason);
        }
    }
}
