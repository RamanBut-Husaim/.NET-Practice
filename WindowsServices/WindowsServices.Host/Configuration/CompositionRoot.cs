using WindowsServices.Core.FileOperations;
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
                        factory.GetInstance<IFileOperationManager>(),
                        factory.GetInstance<ILogger>()));

            serviceRegistry.RegisterInstance<ILogger>(LogManager.GetLogger("Application Logger"));
            serviceRegistry.Register<IFileSystemMonitorServiceFactory, LoggingFileSystemMonitorServiceFactory>(CompositionRoot.LoggingFileSystemMonitorServiceFactory);
            serviceRegistry.Register<IFileSystemMonitorServiceFactory, FileSystemMonitorServiceFactory>(new PerContainerLifetime());
            serviceRegistry.Register<string, IFolderWatcher>((factory, path) => new FolderWatcher(path));
            serviceRegistry.Register<IFolderWatcherFactory, LoggingFolderWatcherFactory>(CompositionRoot.LoggingFolderWatcherFactory);
            serviceRegistry.Register<IFolderWatcherFactory, FolderWatcherFactory>(new PerContainerLifetime());

            serviceRegistry.Register<PollingManagerFactory>(new PerContainerLifetime());
            serviceRegistry.Register<IPollingManager>(factory => factory.GetInstance<PollingManagerFactory>().Create(), new PerContainerLifetime());

            serviceRegistry.Register<string, string, ICopyOperation>((factory, sourcePath, destinationPath) =>
                new LoggingPollingCopyOperation(
                    new PollingCopyOperation(new CopyOperation(sourcePath, destinationPath), factory.GetInstance<IPollingManager>()),
                    factory.GetInstance<ILogger>()));
            serviceRegistry.Register<string, string, IRenameOperation>(
                ((factory, oldPath, newPath) => new LoggingPollingRenameOperation(
                    new PollingRenameOperation(new RenameOperation(oldPath, newPath), factory.GetInstance<IPollingManager>()),
                    factory.GetInstance<ILogger>())));
            serviceRegistry.Register<OperationFactory>(new PerContainerLifetime());
            serviceRegistry.Register<IFileOperationManager, FileOperationManager>();
        }
    }
}
