using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using MessageQueues.Core.Events;
using MessageQueues.Core.Messages;

namespace MessageQueues.CentralHost.Core
{
    public sealed class OperationBatch
    {
        private readonly string _fileName;
        private readonly IReadOnlyCollection<BasicSerializedDeliveryEventArgs<FileMessage>> _operations;

        public OperationBatch(string fileName, IList<BasicSerializedDeliveryEventArgs<FileMessage>> operations)
        {
            _fileName = fileName;
            _operations = new ReadOnlyCollection<BasicSerializedDeliveryEventArgs<FileMessage>>(operations.ToList());
        }

        public string FileName
        {
            get { return _fileName; }
        }

        public IReadOnlyCollection<BasicSerializedDeliveryEventArgs<FileMessage>> Operations
        {
            get { return _operations; }
        }
    }
}
