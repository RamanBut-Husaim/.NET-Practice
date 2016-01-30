using System.Collections.Generic;

using MessageQueues.Core.Events;
using MessageQueues.Core.Messages;

namespace MessageQueues.CentralHost.Core
{
    public interface IFileBatchManager
    {
        void Process(IEnumerable<BasicSerializedDeliveryEventArgs<FileMessage>> operations);
    }
}
