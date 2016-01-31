using System.ServiceProcess;

using Fclp;

using LightInject;

using MessageQueues.CentralHost.Configuration;

using NLog;

namespace MessageQueues.CentralHost
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            var argumentParser = ApplicationArgumentsConfiguration.CreateCommandLineParser();

            ICommandLineParserResult parseResult = argumentParser.Parse(args);

            if (!parseResult.HasErrors && !parseResult.EmptyArgs)
            {
                var configuration = CreateConfiguration(argumentParser.Object);
                ConfigurationProvider.Instance.Configuration = configuration;

                using (var serviceContainer = new ServiceContainer())
                {
                    serviceContainer.RegisterFrom<CompositionRoot>();
                    var factory = serviceContainer.GetInstance<CentralHostServiceFactory>();

                    CentralHostService service = factory.Create(configuration);

                    var logger = serviceContainer.GetInstance<ILogger>();

                    logger.Trace("[Before Service base run]");
                    ServiceBase.Run(service);
                    logger.Trace("[After Service base run]");
                }
            }
            else
            {
                argumentParser.HelpOption.ShowHelp(argumentParser.Options);
            }
        }

        internal static CentralHostServiceConfiguration CreateConfiguration(ApplicationArguments arguments)
        {
            return new CentralHostServiceConfiguration(arguments.ServiceName, arguments.ResultFolder);
        }
    }
}
