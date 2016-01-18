using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using WindowsServices.Core.FileOperations;
using WindowsServices.Core.Jobs;
using WindowsServices.Core.Watching;
using NLog;

namespace WindowsServices.Host
{
    public sealed class FileSystemMonitorService : ServiceBase
    {
        private const string DefaultServiceName = "FileWatchingService";

        private readonly object _sync = new object();

        private readonly ILogger _logger;
        private readonly IFolderWatcherFactory _folderWatcherFactory;
        private readonly IFolderWatcher _folderWatcher;
        private readonly JobManagerFactory _jobManagerFactory;
        private readonly string _destinationPath;

        private readonly ManualResetEventSlim _serviceShutdownEvent;
        private readonly Task _fileProcessingRoutine;
        private readonly Queue<FileSystemWatcherEventArgs> _operationQueue;

        private bool _disposed;

        public FileSystemMonitorService(
            FileSystemMonitorServiceConfiguration configuration,
            IFolderWatcherFactory folderWatcherFactory,
            JobManagerFactory jobManagerFactory,
            ILogger logger)
        {
            _logger = logger;
            _folderWatcherFactory = folderWatcherFactory;
            _folderWatcher = folderWatcherFactory.Create(configuration.FolderToMonitor);
            _jobManagerFactory = jobManagerFactory;
            _destinationPath = configuration.TargetFolder;

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

        private void ProcessOperations(IList<FileSystemWatcherEventArgs> operations)
        {
            var jobManager = _jobManagerFactory.Create(_destinationPath);

            jobManager.Process(operations);
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
