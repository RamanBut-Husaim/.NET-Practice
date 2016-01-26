using System.Threading.Tasks;
using NLog;

namespace MessageQueues.HarvesterHost.Core.FileOperations.Synchronization
{
    public sealed class LoggingPollingSynchronizationOperation : ISynchronizationOperation
    {
        private readonly ISynchronizationOperation _synchronizationOperation;
        private readonly ILogger _logger;

        public LoggingPollingSynchronizationOperation(
            ISynchronizationOperation synchronizationOperation,
            ILogger logger)
        {
            _synchronizationOperation = synchronizationOperation;
            _logger = logger;
        }

        public string SourcePath
        {
            get { return _synchronizationOperation.SourcePath; }
        }

        public string DestinationPath
        {
            get { return _synchronizationOperation.DestinationPath; }
        }

        public async Task Perform()
        {
            _logger.Trace("[Start]: Synchronization operation '{0}'", this.SourcePath);
            await _synchronizationOperation.Perform();
            _logger.Trace("[End]: Synchronization operation '{0}'", this.SourcePath);
        }
    }
}
