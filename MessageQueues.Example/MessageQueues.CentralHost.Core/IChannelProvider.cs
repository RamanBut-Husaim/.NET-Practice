using RabbitMQ.Client;

namespace MessageQueues.CentralHost.Core
{
    public interface IChannelProvider
    {
        IModel GetChannel();
    }
}
