using System;

namespace MessageQueues.HarvesterHost
{
    public sealed class FileSystemMonitorServiceFactory : IFileSystemMonitorServiceFactory
    {
        private readonly Func<FileSystemMonitorServiceConfiguration, FileSystemMonitorService> _factory;

        public FileSystemMonitorServiceFactory(Func<FileSystemMonitorServiceConfiguration, FileSystemMonitorService> factory)
        {
            _factory = factory;
        }

        public FileSystemMonitorService Create(FileSystemMonitorServiceConfiguration configuration)
        {
            return _factory.Invoke(configuration);
        }
    }
}
