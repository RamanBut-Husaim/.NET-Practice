using System.Threading.Tasks;
using NLog;

namespace MessageQueues.HarvesterHost.Core.FileOperations.Copy
{
    public sealed class LoggingPollingCopyOperation : ICopyOperation
    {
        private readonly ICopyOperation _copyOperation;
        private readonly ILogger _logger;

        public LoggingPollingCopyOperation(ICopyOperation copyOperation, ILogger logger)
        {
            _copyOperation = copyOperation;
            _logger = logger;
        }

        public string SourcePath
        {
            get { return _copyOperation.SourcePath; }
        }

        public async Task Perform()
        {
            _logger.Trace("[Start]: Copy file from '{0}'", _copyOperation.SourcePath);
            await _copyOperation.Perform();
            _logger.Trace("[End]: Copy file from '{0}'", _copyOperation.SourcePath);
        }
    }
}
