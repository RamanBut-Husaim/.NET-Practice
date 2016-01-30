using System;

using LightInject;

using MessageQueues.CentralHost.Core;
using MessageQueues.CentralHost.Core.FileOperations;
using MessageQueues.CentralHost.Core.FileOperations.Copy;
using MessageQueues.CentralHost.Core.FileOperations.Rename;
using MessageQueues.CentralHost.Core.FileOperations.Synchronization;
using MessageQueues.Core.Messages;
using MessageQueues.Core.Operations.Copy;
using MessageQueues.Core.Operations.Rename;
using MessageQueues.Core.Operations.Synchronization;
using MessageQueues.Core.Polling;

using NLog;

namespace MessageQueues.CentralHost.Configuration
{
    public sealed class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterInstance<ILogger>(LogManager.GetLogger("Application Logger"));
            serviceRegistry.Register<PollingManagerFactory>(new PerContainerLifetime());
            serviceRegistry.Register<IPollingManager>(factory => factory.GetInstance<PollingManagerFactory>().Create(), new PerContainerLifetime());

            serviceRegistry.Register<string, FileMessage, ICopyOperation>((factory, sourcePath, fileMessage) =>
                new LoggingPollingCopyOperation(
                    new PollingCopyOperation(new CopyOperation(sourcePath, fileMessage),
                        factory.GetInstance<IPollingManager>()),
                    factory.GetInstance<ILogger>()));

            serviceRegistry.Register<string, string, IRenameOperation>(((factory, oldPath, newPath) =>
                new LoggingPollingRenameOperation(
                    new PollingRenameOperation(new RenameOperation(oldPath, newPath),
                        factory.GetInstance<IPollingManager>()),
                    factory.GetInstance<ILogger>())));

            serviceRegistry.Register<ICopyOperation, ISynchronizationOperation>(
                ((factory, copyOperation) => new LoggingPollingSynchronizationOperation(
                    new PollingSynchronizationOperation(
                        new SynchronizationOperation(copyOperation),
                        factory.GetInstance<IPollingManager>()),
                    factory.GetInstance<ILogger>())));

            serviceRegistry.Register<FileMessageListenerService>();
            serviceRegistry.Register<HarvesterProcessingDispatcherFactory>(new PerContainerLifetime());
            serviceRegistry.Register<string, IHarvesterProcessingDispatcher>(
                ((factory, harvesterName) => new HarvesterProcessingDispatcher(factory.GetInstance<Func<IFileBatchManager>>(), harvesterName)));

            serviceRegistry.Register<IFileBatchManager, FileBatchManager>();
            serviceRegistry.Register<IFileOperationProcessor, FileOperationProcessor>();

            serviceRegistry.Register<OperationFactory>(factory => new OperationFactory(
                factory.GetInstance<Func<string, FileMessage, ICopyOperation>>(),
                factory.GetInstance<Func<string, string, IRenameOperation>>(),
                factory.GetInstance<Func<ICopyOperation, ISynchronizationOperation>>(),
                ConfigurationProvider.Instance.Configuration.ResultFolder), new PerContainerLifetime());
        }
    }
}
