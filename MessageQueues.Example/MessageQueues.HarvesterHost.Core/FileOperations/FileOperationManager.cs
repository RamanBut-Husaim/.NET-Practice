using System.Collections.Generic;
using System.Threading.Tasks;

using MessageQueues.Core.Operations;
using MessageQueues.HarvesterHost.Core.Watching;

namespace MessageQueues.HarvesterHost.Core.FileOperations
{
    public sealed class FileOperationManager : IFileOperationManager
    {
        private readonly OperationFactory _operationFactory;
        private readonly FileSenderFactory _fileSenderFactory;

        public FileOperationManager(OperationFactory operationFactory, FileSenderFactory fileSenderFactory)
        {
            _operationFactory = operationFactory;
            _fileSenderFactory = fileSenderFactory;
        }

        public async Task ProcessFileOperations(OperationBatch operationBatch)
        {
            // TODO: seems that operation list could be analyzed and simplified.
            // For ex., if we have sequential changes operations then need to investigate the possibility
            // to reduce the graph of such operations.
            IList<FileSystemWatcherEventArgs> fileOperations = operationBatch.Operations;
            IFileSender fileSender = _fileSenderFactory.Create();

            for (int i = 0; i < fileOperations.Count; ++i)
            {
                IOperation operation = _operationFactory.Create(fileOperations[i], fileSender);
                await operation.Perform();
            }
        }
    }
}
