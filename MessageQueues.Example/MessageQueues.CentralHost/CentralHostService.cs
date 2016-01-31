using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

using MessageQueues.CentralHost.Core;

using NLog;

namespace MessageQueues.CentralHost
{
    public sealed class CentralHostService : ServiceBase
    {
        private const string DefaultServiceName = "CentralHost";

        private readonly ILogger _logger;

        private readonly ManualResetEventSlim _shutdownEvent;
        private readonly CentralHostServiceConfiguration _centralHostServiceConfiguration;
        private readonly FileMessageListenerService _fileMessageListenerService;

        private readonly Task _processingRoutine;

        private bool _disposed;

        public CentralHostService(
            ILogger logger,
            FileMessageListenerService fileMessageListenerService,
            CentralHostServiceConfiguration centralHostServiceConfiguration)
        {
            _logger = logger;

            _centralHostServiceConfiguration = centralHostServiceConfiguration;
            _fileMessageListenerService = fileMessageListenerService;
            _processingRoutine = new Task(this.ProcessOperations, TaskCreationOptions.LongRunning);
            _shutdownEvent = new ManualResetEventSlim(false);

            this.InitializeServiceState(centralHostServiceConfiguration);
        }

        private void InitializeServiceState(CentralHostServiceConfiguration configuration)
        {
            this.CanStop = true;
            this.AutoLog = false;
            this.ServiceName = configuration.ServiceName ?? DefaultServiceName;
        }

        protected override void OnStart(string[] args)
        {
            _logger.Trace("[Start]: Central host service is starting.");
            _shutdownEvent.Reset();
            _processingRoutine.Start();
            _logger.Trace("[End]: Central host service is starting.");

            base.OnStart(args);
        }

        protected override void OnStop()
        {
            _logger.Trace("[Start]: Central host service is shutting down.");
            _shutdownEvent.Set();
            _processingRoutine.Wait();
            _logger.Trace("[End]: Central host service is shutting down.");

            base.OnStop();
        }

        private void ProcessOperations()
        {
            _fileMessageListenerService.StartProcessing();

            _shutdownEvent.Wait();

            _fileMessageListenerService.RequestShutdown(TimeSpan.FromMinutes(2));
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _fileMessageListenerService.Dispose();
                    _shutdownEvent.Dispose();
                }

                _disposed = true;
            }

            base.Dispose(disposing);
        }
    }
}
