using System.ServiceProcess;
using System.Threading;
using WindowsServices.Core;
using WindowsServices.Core.Watching;

namespace WindowsServices.Host
{
    public sealed class FileSystemMonitorService : ServiceBase
    {
        private const string DefaultServiceName = "FileWatchingService";

        private readonly object _sync = new object();

        private readonly IFolderWatcherFactory _folderWatcherFactory;
        private readonly IFolderWatcher _folderWatcher;
        private readonly ManualResetEventSlim _serviceShutdownEvent;

        private bool _disposed;

        public FileSystemMonitorService(
            FileSystemMonitorServiceConfiguration configuration,

            IFolderWatcherFactory folderWatcherFactory)
        {
            _folderWatcherFactory = folderWatcherFactory;
            _folderWatcher = folderWatcherFactory.Create(configuration.FolderToMonitor);
            _serviceShutdownEvent = new ManualResetEventSlim(false);

            this.CanStop = true;
            this.AutoLog = false;
            this.ServiceName = configuration.ServiceName ?? DefaultServiceName;
        }

        protected override void OnStart(string[] args)
        {
            _serviceShutdownEvent.Reset();
            _folderWatcher.StartWatching();

            base.OnStart(args);
        }

        protected override void OnStop()
        {
            _serviceShutdownEvent.Set();
            _folderWatcher.StopWatching();

            base.OnStop();
        }


        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _serviceShutdownEvent.Dispose();
                    _folderWatcherFactory.Release(_folderWatcher);
                }

                _disposed = true;
            }

            base.Dispose(disposing);
        }
    }
}
