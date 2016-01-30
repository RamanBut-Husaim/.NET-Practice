using System.Threading.Tasks;

using NLog;

namespace MessageQueues.Core.Operations.Copy
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

        public string Path
        {
            get { return _copyOperation.Path; }
        }

        public async Task Perform()
        {
            _logger.Trace("[Start]: Copy file '{0}'", _copyOperation.Path);
            await _copyOperation.Perform();
            _logger.Trace("[End]: Copy file '{0}'", _copyOperation.Path);
        }
    }
}
