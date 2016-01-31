using NLog;

namespace MessageQueues.HarvesterHost
{
    public sealed class LoggingFileSystemMonitorServiceFactory : IFileSystemMonitorServiceFactory
    {
        private readonly IFileSystemMonitorServiceFactory _fileSystemMonitorServiceFactory;
        private readonly ILogger _logger;

        public LoggingFileSystemMonitorServiceFactory(IFileSystemMonitorServiceFactory fileSystemMonitorServiceFactory, ILogger logger)
        {
            _fileSystemMonitorServiceFactory = fileSystemMonitorServiceFactory;
            _logger = logger;
        }

        public FileSystemMonitorService Create(ServiceConfiguration configuration)
        {
            _logger.Log(LogLevel.Trace, "[Start]: File system monitor service creation");
            var fileSystemMonitorService = _fileSystemMonitorServiceFactory.Create(configuration);
            _logger.Log(LogLevel.Trace, "[End]: File system monitor service creation");

            return fileSystemMonitorService;
        }
    }
}
