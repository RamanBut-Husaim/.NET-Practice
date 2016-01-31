using System;

namespace MessageQueues.HarvesterHost
{
    public sealed class FileSystemMonitorServiceFactory : IFileSystemMonitorServiceFactory
    {
        private readonly Func<ServiceConfiguration, FileSystemMonitorService> _factory;

        public FileSystemMonitorServiceFactory(Func<ServiceConfiguration, FileSystemMonitorService> factory)
        {
            _factory = factory;
        }

        public FileSystemMonitorService Create(ServiceConfiguration configuration)
        {
            return _factory.Invoke(configuration);
        }
    }
}
