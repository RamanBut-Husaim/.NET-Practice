using NLog;

namespace MessageQueues.Core.Polling
{
    public sealed class PollingManagerFactory
    {
        private const int WaitInterval = 500;
        private const int PollingCount = 3;

        private readonly ILogger _logger;

        public PollingManagerFactory(ILogger logger)
        {
            _logger = logger;
        }

        public IPollingManager Create()
        {
            return new PollingManager(_logger, WaitInterval, PollingCount);
        }
    }
}
