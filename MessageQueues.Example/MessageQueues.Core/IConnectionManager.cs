using RabbitMQ.Client;

namespace MessageQueues.Core
{
    public interface IConnectionManager
    {
        IModel GetChannel();
    }
}
