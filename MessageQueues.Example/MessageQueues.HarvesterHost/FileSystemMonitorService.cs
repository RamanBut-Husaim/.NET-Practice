using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using MessageQueues.HarvesterHost.Core.Services;
using MessageQueues.HarvesterHost.Core.Watching;
using NLog;

namespace MessageQueues.HarvesterHost
{
    public sealed class FileSystemMonitorService : ServiceBase
    {
        private const string DefaultServiceName = "FileWatchingService";

        private readonly object _sync = new object();

        private readonly ILogger _logger;
        private readonly IFolderWatcherFactory _folderWatcherFactory;
        private readonly IFolderWatcher _folderWatcher;
        private readonly Func<IFileService> _fileServiceFactory;
        private readonly SynchronizationServiceFactory _synchronizationServiceFactory;
        private readonly FileSystemMonitorServiceConfiguration _configuration;

        private readonly ManualResetEventSlim _serviceShutdownEvent;
        private readonly Task _fileProcessingRoutine;
        private readonly Queue<FileSystemWatcherEventArgs> _operationQueue;

        private bool _disposed;

        public FileSystemMonitorService(
            FileSystemMonitorServiceConfiguration configuration,
            IFolderWatcherFactory folderWatcherFactory,
            Func<IFileService> fileServiceFactory,
            SynchronizationServiceFactory synchronizationServiceFactory,
            ILogger logger)
        {
            _logger = logger;
            _folderWatcherFactory = folderWatcherFactory;
            _folderWatcher = folderWatcherFactory.Create(configuration.FolderToMonitor);
            _fileServiceFactory = fileServiceFactory;
            _synchronizationServiceFactory = synchronizationServiceFactory;
            _configuration = configuration;

            _serviceShutdownEvent = new ManualResetEventSlim(false);
            _fileProcessingRoutine = new Task(this.RunServiceOperation, TaskCreationOptions.LongRunning);
            _operationQueue = new Queue<FileSystemWatcherEventArgs>();

            this.SubscribeToEvents();
            this.InitializeServiceState(configuration);
        }

        private void InitializeServiceState(FileSystemMonitorServiceConfiguration configuration)
        {
            this.CanStop = true;
            this.AutoLog = false;
            this.ServiceName = configuration.ServiceName ?? DefaultServiceName;
        }

        private void SubscribeToEvents()
        {
            _folderWatcher.Created += this.OnFolderStateChanged;
            _folderWatcher.Changed += this.OnFolderStateChanged;
            _folderWatcher.Renamed += this.OnFolderStateChanged;
        }

        private void OnFolderStateChanged(object sender, FileSystemWatcherEventArgs fileSystemWatcherEventArgs)
        {
            lock (_sync)
            {
                _operationQueue.Enqueue(fileSystemWatcherEventArgs);
                Monitor.Pulse(_sync);
            }
        }

        protected override void OnStart(string[] args)
        {
            _logger.Trace("[Start]: File system monitor service is starting.");
            _serviceShutdownEvent.Reset();
            _folderWatcher.StartWatching();
            _fileProcessingRoutine.Start();
            _logger.Trace("[End]: File system monitor service is starting.");

            base.OnStart(args);
        }

        protected override void OnStop()
        {
            _logger.Trace("[Start]: File system monitor service is shutting down.");
            _serviceShutdownEvent.Set();
            _folderWatcher.StopWatching();
            _fileProcessingRoutine.Wait();
            this.UnsubscribeFromEvents();
            _logger.Trace("[End]: File system monitor service is shutting down.");

            base.OnStop();
        }

        private void UnsubscribeFromEvents()
        {
            _folderWatcher.Created -= this.OnFolderStateChanged;
            _folderWatcher.Changed -= this.OnFolderStateChanged;
            _folderWatcher.Renamed -= this.OnFolderStateChanged;
        }

        private void RunServiceOperation()
        {
            _logger.Trace("[Start]: Service routine operation.");

            this.PerformSynchronization();

            while (true)
            {
                IList<FileSystemWatcherEventArgs> workToPerform;

                lock (_sync)
                {
                    while (_operationQueue.Count == 0)
                    {
                        if (_serviceShutdownEvent.IsSet)
                        {
                            _logger.Trace("[End]: Service routine operation.");
                            return;
                        }

                        Monitor.Wait(_sync, TimeSpan.FromSeconds(5));
                    }

                    workToPerform = _operationQueue.ToList();
                    _operationQueue.Clear();

                    Monitor.Pulse(_sync);
                }

                this.ProcessOperations(workToPerform);
            }
        }

        private void PerformSynchronization()
        {
            //ISynchronizationService synchronizationService = _synchronizationServiceFactory.Create(
            //    _configuration.FolderToMonitor);

            //synchronizationService.PerformSynchronization().Wait();
        }

        private void ProcessOperations(IList<FileSystemWatcherEventArgs> operations)
        {
            IFileService fileService = _fileServiceFactory.Invoke();

            fileService.Process(operations);
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
