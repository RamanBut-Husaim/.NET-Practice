using WindowsServices.Core.Watching;
using NLog;

namespace WindowsServices.Core
{
    public sealed class LoggingFolderWatcherFactory : IFolderWatcherFactory
    {
        private readonly IFolderWatcherFactory _folderWatcherFactory;
        private readonly ILogger _logger;

        public LoggingFolderWatcherFactory(
            IFolderWatcherFactory folderWatcherFactory,
            ILogger logger)
        {
            _folderWatcherFactory = folderWatcherFactory;
            _logger = logger;
        }

        public IFolderWatcher Create(string folderPath)
        {
            _logger.Log(LogLevel.Trace, "Start: Folder watcher creation.");
            IFolderWatcher folderWatcher = _folderWatcherFactory.Create(folderPath);
            _logger.Log(LogLevel.Trace, "End: Folder watcher creation.");

            return folderWatcher;
        }

        public void Release(IFolderWatcher folderWatcher)
        {
            _logger.Log(LogLevel.Trace, "Releasing folder watcher.");

            _folderWatcherFactory.Release(folderWatcher);
        }
    }
}
