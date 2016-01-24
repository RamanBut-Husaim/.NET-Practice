using System;
using Fclp;

namespace MessageQueues.HarvesterHost.Configuration
{
    internal sealed class ApplicationArgumentsConfigurationSetup
    {
        public static FluentCommandLineParser<ApplicationArguments> CreateCommandLineParser()
        {
            var parser = new FluentCommandLineParser<ApplicationArguments>();

            parser.Setup(arg => arg.FolderToMonitor)
                .As('i', "inputFolder")
                .Required()
                .WithDescription("A folder that will be monitored for file changes.");

            parser.Setup(arg => arg.HostName)
                .As('h', "hostName")
                .Required()
                .WithDescription("A host name for messaging queue.");

            parser.Setup(arg => arg.ServiceName)
                  .As('n', "name")
                  .WithDescription("The name of the service to be registered.");

            parser.SetupHelp("?", "help")
                .Callback(text => Console.WriteLine(text));

            return parser;
        }
    }
}
