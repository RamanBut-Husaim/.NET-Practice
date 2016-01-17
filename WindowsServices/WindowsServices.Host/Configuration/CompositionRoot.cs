using WindowsServices.Core;
using WindowsServices.Core.Watching;
using WindowsServices.FileWatching;
using LightInject;
using NLog;

namespace WindowsServices.Host.Configuration
{
    public sealed class CompositionRoot : ICompositionRoot
    {
        public const string LoggingFileSystemMonitorServiceFactory = "LoggingFileSystemMonitorServiceFactory";
        private const string LoggingFolderWatcherFactory = "LoggingFolderWatcherFactory";

        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<FileSystemMonitorServiceConfiguration, FileSystemMonitorService>(
                (factory, configuration) =>
                    new FileSystemMonitorService(
                        configuration,
                        factory.GetInstance<IFolderWatcherFactory>(CompositionRoot.LoggingFolderWatcherFactory),
                        factory.GetInstance<ILogger>()));

            serviceRegistry.RegisterInstance<ILogger>(LogManager.GetLogger("Application Logger"));
            serviceRegistry.Register<IFileSystemMonitorServiceFactory, LoggingFileSystemMonitorServiceFactory>(CompositionRoot.LoggingFileSystemMonitorServiceFactory);
            serviceRegistry.Register<IFileSystemMonitorServiceFactory, FileSystemMonitorServiceFactory>(new PerContainerLifetime());
            serviceRegistry.Register<string, IFolderWatcher>((factory, path) => new FolderWatcher(path));
            serviceRegistry.Register<IFolderWatcherFactory, LoggingFolderWatcherFactory>(CompositionRoot.LoggingFolderWatcherFactory);
            serviceRegistry.Register<IFolderWatcherFactory, FolderWatcherFactory>(new PerContainerLifetime());
        }
    }
}
