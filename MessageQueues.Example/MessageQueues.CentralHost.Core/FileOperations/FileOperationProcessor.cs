using System.Threading.Tasks;

using MessageQueues.Core;
using MessageQueues.Core.Events;
using MessageQueues.Core.Messages;
using MessageQueues.Core.Operations;

namespace MessageQueues.CentralHost.Core.FileOperations
{
    public sealed class FileOperationProcessor : IFileOperationProcessor
    {
        private readonly IFileTransferManager _fileTransferManager;
        private readonly OperationFactory _operationFactory;

        public FileOperationProcessor(
            IFileTransferManager fileTransferManager,
            OperationFactory operationFactory)
        {
            _fileTransferManager = fileTransferManager;
            _operationFactory = operationFactory;
        }

        public async Task ProcessBatch(OperationBatch operationBatch)
        {
            foreach (BasicSerializedDeliveryEventArgs<FileMessage> operation in operationBatch.Operations)
            {
                IOperation fileOperation = _operationFactory.Create(operation.Body);
                await fileOperation.Perform();

                _fileTransferManager.Acknowledge(operation.DeliveryTag);
            }
        }
    }
}
