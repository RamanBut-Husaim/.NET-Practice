using System;
using System.Threading.Tasks;

using NLog;

namespace MessageQueues.CentralHost.Core.FileOperations
{
    public sealed class LoggingFileOperationProcessor : IFileOperationProcessor
    {
        private readonly IFileOperationProcessor _fileOperationProcessor;
        private readonly ILogger _logger;

        public LoggingFileOperationProcessor(IFileOperationProcessor fileOperationProcessor, ILogger logger)
        {
            _fileOperationProcessor = fileOperationProcessor;
            _logger = logger;
        }

        public async Task ProcessBatch(OperationBatch operationBatch)
        {
            _logger.Trace("[Start]: Batch processing for the file [{0}]", operationBatch.FileName);

            try
            {
                await _fileOperationProcessor.ProcessBatch(operationBatch);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            finally
            {
                _logger.Trace("[End]: Batch processing for the file [{0}]", operationBatch.FileName);
            }
        }
    }
}
