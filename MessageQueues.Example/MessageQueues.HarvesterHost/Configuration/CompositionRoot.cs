using System;
using LightInject;
using MessageQueues.Core;
using MessageQueues.HarvesterHost.Core.FileOperations;
using MessageQueues.HarvesterHost.Core.FileOperations.Copy;
using MessageQueues.HarvesterHost.Core.FileOperations.Rename;
using MessageQueues.HarvesterHost.Core.FileOperations.Synchronization;
using MessageQueues.HarvesterHost.Core.Services;
using MessageQueues.HarvesterHost.Core.Watching;
using NLog;

namespace MessageQueues.HarvesterHost.Configuration
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
                        factory.GetInstance<Func<IFileService>>(),
                        factory.GetInstance<SynchronizationServiceFactory>(),
                        factory.GetInstance<ILogger>()));

            serviceRegistry.Register<string, string, ISynchronizationService>(
                (factory, sourcePath, destinationPath) =>
                    new SynchronizationService(
                        sourcePath,
                        factory.GetInstance<Func<IFileOperationManager>>(),
                        factory.GetInstance<ILogger>()));
            serviceRegistry.Register<SynchronizationServiceFactory>(new PerContainerLifetime());

            serviceRegistry.RegisterInstance<ILogger>(LogManager.GetLogger("Application Logger"));
            serviceRegistry.Register<IFileSystemMonitorServiceFactory, LoggingFileSystemMonitorServiceFactory>(CompositionRoot.LoggingFileSystemMonitorServiceFactory);
            serviceRegistry.Register<IFileSystemMonitorServiceFactory, FileSystemMonitorServiceFactory>(new PerContainerLifetime());
            serviceRegistry.Register<string, IFolderWatcher>((factory, path) => new FolderWatcher(path));
            serviceRegistry.Register<IFolderWatcherFactory, LoggingFolderWatcherFactory>(CompositionRoot.LoggingFolderWatcherFactory);
            serviceRegistry.Register<IFolderWatcherFactory, FolderWatcherFactory>(new PerContainerLifetime());

            serviceRegistry.Register<PollingManagerFactory>(new PerContainerLifetime());
            serviceRegistry.Register<IPollingManager>(factory => factory.GetInstance<PollingManagerFactory>().Create(), new PerContainerLifetime());

            serviceRegistry.Register<string, IFileSender, ICopyOperation>((factory, sourcePath, fileSender) =>
                new LoggingPollingCopyOperation(
                    new PollingCopyOperation(new CopyOperation(sourcePath, fileSender),
                        factory.GetInstance<IPollingManager>()),
                    factory.GetInstance<ILogger>()));

            serviceRegistry.Register<string, string, IFileSender, IRenameOperation>(((factory, oldPath, newPath, fileSender) =>
                new LoggingPollingRenameOperation(
                    new PollingRenameOperation(new RenameOperation(oldPath, newPath, fileSender),
                        factory.GetInstance<IPollingManager>()),
                    factory.GetInstance<ILogger>())));

            serviceRegistry.Register<string, IFileSender, ISynchronizationOperation>(
                ((factory, sourcePath, fileSender) => new LoggingPollingSynchronizationOperation(
                    new PollingSynchronizationOperation(
                        new SynchronizationOperation(sourcePath, fileSender),
                        factory.GetInstance<IPollingManager>()),
                    factory.GetInstance<ILogger>())));

            serviceRegistry.Register<OperationFactory>(new PerContainerLifetime());

            serviceRegistry.Register<IFileService, FileService>();
            serviceRegistry.Register<IFileOperationManager, FileOperationManager>();
            serviceRegistry.Register<FileSenderFactory>(new PerContainerLifetime());
            serviceRegistry.Register<ConnectionManagerFactory>(new PerContainerLifetime());
            serviceRegistry.Register<IConnectionManager>(factory => factory.GetInstance<ConnectionManagerFactory>().Create(), new PerContainerLifetime());
        }
    }
}
