using System.ServiceProcess;
using Fclp;
using LightInject;
using MessageQueues.HarvesterHost.Configuration;
using NLog;

namespace MessageQueues.HarvesterHost
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            var argumentParser = ApplicationArgumentsConfigurationSetup.CreateCommandLineParser();

            ICommandLineParserResult parseResult = argumentParser.Parse(args);

            if (!parseResult.HasErrors && !parseResult.EmptyArgs)
            {
                using (var serviceContainer = new ServiceContainer())
                {
                    serviceContainer.RegisterFrom<CompositionRoot>();
                    IFileSystemMonitorServiceFactory factory = serviceContainer.GetInstance<IFileSystemMonitorServiceFactory>(CompositionRoot.LoggingFileSystemMonitorServiceFactory);
                    FileSystemMonitorServiceConfiguration configuration = CreateConfiguration(argumentParser.Object);

                    var logger = serviceContainer.GetInstance<ILogger>();
                    logger.Log(LogLevel.Trace, "From: {0}", configuration.FolderToMonitor);

                    FileSystemMonitorService service = factory.Create(configuration);

                    ServiceBase.Run(service);
                }
            }
            else
            {
                argumentParser.HelpOption.ShowHelp(argumentParser.Options);
            }
        }

        internal static FileSystemMonitorServiceConfiguration CreateConfiguration(ApplicationArguments arguments)
        {
            return new FileSystemMonitorServiceConfiguration(arguments.FolderToMonitor, arguments.ServiceName);
        }
    }
}
