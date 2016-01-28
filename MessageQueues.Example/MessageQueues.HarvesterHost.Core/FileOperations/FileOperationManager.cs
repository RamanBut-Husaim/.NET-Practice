using System.Collections.Generic;
using System.Threading.Tasks;
using MessageQueues.Core;
using MessageQueues.HarvesterHost.Core.Watching;
using RabbitMQ.Client;

namespace MessageQueues.HarvesterHost.Core.FileOperations
{
    public sealed class FileOperationManager : IFileOperationManager
    {
        private readonly OperationFactory _operationFactory;
        private readonly ITransferManager _transferManager;

        public FileOperationManager(OperationFactory operationFactory, ITransferManager transferManager)
        {
            _operationFactory = operationFactory;
            _transferManager = transferManager;
        }

        public async Task ProcessFileOperations(OperationBatch operationBatch)
        {
            // TODO: seems that operation list could be analyzed and simplified.
            // For ex., if we have sequential changes operations then need to investigate the possibility
            // to reduce the graph of such operations.
            IList<FileSystemWatcherEventArgs> fileOperations = operationBatch.Operations;

            for (int i = 0; i < fileOperations.Count; ++i)
            {
                IOperation operation = _operationFactory.Create(fileOperations[i], "replace");
                await operation.Perform();
            }
        }
    }
}
