using System.Threading.Tasks;

using MessageQueues.Core.Events;
using MessageQueues.Core.Messages;

namespace MessageQueues.CentralHost.Core
{
    public interface IHarvesterProcessingDispatcher
    {
        void EnqueueOperation(BasicSerializedDeliveryEventArgs<FileMessage> operation);

        Task CompleteAsync();
    }
}
