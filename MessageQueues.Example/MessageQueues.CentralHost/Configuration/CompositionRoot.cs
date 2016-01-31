using System;

using LightInject;

using MessageQueues.CentralHost.Core;
using MessageQueues.CentralHost.Core.FileOperations;
using MessageQueues.CentralHost.Core.FileOperations.Copy;
using MessageQueues.CentralHost.Core.FileOperations.Rename;
using MessageQueues.CentralHost.Core.FileOperations.Synchronization;
using MessageQueues.Core;
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

            serviceRegistry.Register<ConnectionManagerFactory>(new PerContainerLifetime());
            serviceRegistry.Register<SerializationAssistantFactory>(new PerContainerLifetime());
            serviceRegistry.Register<IConnectionManager>(factory => factory.GetInstance<ConnectionManagerFactory>().Create(), new PerContainerLifetime());

            serviceRegistry.Register<ChannelProviderFactory>(new PerContainerLifetime());
            serviceRegistry.Register<IChannelProvider>(factory => new ChannelProvider(factory.GetInstance<ConnectionManagerFactory>()), new PerContainerLifetime());

            serviceRegistry.Register<FileTransferManagerFactory>(new PerContainerLifetime());
            serviceRegistry.Register<IFileTransferManager>(factory => factory.GetInstance<FileTransferManagerFactory>().Create(), new PerContainerLifetime());

            serviceRegistry.Register<FileMessageDispatcherFactory>(new PerContainerLifetime());

            serviceRegistry.Register<EventingSerializationBasicConsumer<FileMessage>>(
                factory => new EventingSerializationBasicConsumer<FileMessage>(
                    factory.GetInstance<IChannelProvider>().GetChannel(),
                    factory.GetInstance<SerializationAssistantFactory>()));

            serviceRegistry.Register<FileMessageListenerService>(
                factory => new FileMessageListenerService(
                    factory.GetInstance<EventingSerializationBasicConsumer<FileMessage>>(),
                    factory.GetInstance<IFileTransferManager>(),
                    factory.GetInstance<FileMessageDispatcherFactory>()));

            serviceRegistry.Register<IFileMessageDispatcher, FileMessageDispatcher>();

            serviceRegistry.Register<HarvesterProcessingDispatcherFactory>(new PerContainerLifetime());
            serviceRegistry.Register<string, IHarvesterProcessingDispatcher>(
                ((factory, harvesterName) => new HarvesterProcessingDispatcher(
                    factory.GetInstance<ILogger>(),
                    factory.GetInstance<Func<IFileBatchManager>>(),
                    harvesterName)));

            serviceRegistry.Register<IFileBatchManager, FileBatchManager>();
            serviceRegistry.Register<IFileOperationProcessor>(factory =>
                new LoggingFileOperationProcessor(
                    new FileOperationProcessor(factory.GetInstance<IFileTransferManager>(), factory.GetInstance<OperationFactory>()),
                    factory.GetInstance<ILogger>()));

            serviceRegistry.Register<OperationFactory>(factory => new OperationFactory(
                factory.GetInstance<Func<string, FileMessage, ICopyOperation>>(),
                factory.GetInstance<Func<string, string, IRenameOperation>>(),
                factory.GetInstance<Func<ICopyOperation, ISynchronizationOperation>>(),
                ConfigurationProvider.Instance.Configuration.ResultFolder), new PerContainerLifetime());

            serviceRegistry.Register<CentralHostServiceFactory>(new PerContainerLifetime());
            serviceRegistry.Register<ServiceConfiguration, CentralHostService>(
                (factory, configuration) => new CentralHostService(
                    factory.GetInstance<ILogger>(),
                    factory.GetInstance<FileMessageListenerService>(),
                    configuration));
        }
    }
}
