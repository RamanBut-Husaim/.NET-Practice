using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MessageQueues.CentralHost.Core.FileOperations;
using MessageQueues.Core;
using MessageQueues.Core.Events;
using MessageQueues.Core.Messages;

using NLog;

namespace MessageQueues.CentralHost.Core
{
    public sealed class FileBatchManager : IFileBatchManager
    {
        private readonly IFileTransferManager _fileTransferManager;
        private readonly Func<IFileTransferManager, IFileOperationProcessor> _fileOperationProcessorFactory;
        private readonly ILogger _logger;

        public FileBatchManager(
            IFileTransferManager fileTransferManager,
            Func<IFileTransferManager, IFileOperationProcessor> fileOperationProcessorFactory,
            ILogger logger)
        {
            _fileTransferManager = fileTransferManager;
            _fileOperationProcessorFactory = fileOperationProcessorFactory;
            _logger = logger;
        }

        public void Process(IEnumerable<BasicSerializedDeliveryEventArgs<FileMessage>> operations)
        {
            _logger.Trace("[Start]: Start file batch processing.");

            var groupedOperations = operations.GroupBy(p => p.Body.FileName);

            IList<Task> processingOperations = new List<Task>();

            foreach (var groupedOperation in groupedOperations)
            {
                var operationBatch = new OperationBatch(groupedOperation.Key, groupedOperation.ToList());
                IFileOperationProcessor fileOperationProcessor = _fileOperationProcessorFactory.Invoke(_fileTransferManager);

                var processingOperation = fileOperationProcessor.ProcessBatch(operationBatch);
                processingOperations.Add(processingOperation);
            }

            try
            {
                Task.WaitAll(processingOperations.ToArray());
            }
            catch (AggregateException ex)
            {
                for (int i = 0; i < ex.InnerExceptions.Count; ++i)
                {
                    _logger.Error(ex.InnerExceptions[i]);
                }
            }
            finally
            {
                _logger.Trace("[End]: End file batch processing.");
            }
        }
    }
}
