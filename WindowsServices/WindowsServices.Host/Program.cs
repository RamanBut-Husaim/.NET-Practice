using System.ServiceProcess;
using WindowsServices.Host.Configuration;
using Fclp;
using LightInject;
using NLog;

namespace WindowsServices.Host
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
                    logger.Log(LogLevel.Trace, "From: {0} To: {1}", configuration.FolderToMonitor, configuration.TargetFolder);

                    FileSystemMonitorService service = factory.Create(configuration);

                    ServiceBase.Run(service);
                }
            }
            else
            {
                argumentParser.HelpOption.ShowHelp(argumentParser.Options);
            }
        }

        public static FileSystemMonitorServiceConfiguration CreateConfiguration(ApplicationArguments arguments)
        {
            return new FileSystemMonitorServiceConfiguration(arguments.FolderToMonitor, arguments.StorageFolder, arguments.ServiceName);
        }
    }
}
