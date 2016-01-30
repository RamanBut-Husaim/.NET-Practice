using System;

using MessageQueues.Core.Events;
using MessageQueues.Core.Messages;

namespace MessageQueues.CentralHost.Core
{
    public interface IFileMessageDispatcher
    {
        void EnqueueOperation(BasicSerializedDeliveryEventArgs<FileMessage> operation);

        void CompleteProcessing(TimeSpan waitPeriod);
    }
}
