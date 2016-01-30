using System.Threading.Tasks;

using NLog;

namespace MessageQueues.Core.Operations.Synchronization
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

        public string Path
        {
            get { return _synchronizationOperation.Path; }
        }

        public async Task Perform()
        {
            _logger.Trace("[Start]: Synchronization operation '{0}'", this.Path);
            await _synchronizationOperation.Perform();
            _logger.Trace("[End]: Synchronization operation '{0}'", this.Path);
        }
    }
}
