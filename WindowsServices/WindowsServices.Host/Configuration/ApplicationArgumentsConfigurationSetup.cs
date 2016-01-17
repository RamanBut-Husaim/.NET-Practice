using System;
using Fclp;

namespace WindowsServices.Host.Configuration
{
    public sealed class ApplicationArgumentsConfigurationSetup
    {
        public static FluentCommandLineParser<ApplicationArguments> CreateCommandLineParser()
        {
            var parser = new FluentCommandLineParser<ApplicationArguments>();

            parser.Setup(arg => arg.FolderToMonitor)
                .As('i', "inputFolder")
                .Required()
                .WithDescription("A folder that will be monitored for file changes.");

            parser.Setup(arg => arg.StorageFolder)
                .As('o', "outputFolder")
                .Required()
                .WithDescription("A destination folder for the files from the source folder.");

            parser.Setup(arg => arg.ServiceName)
                  .As('n', "name")
                  .WithDescription("The name of the service to be registered.");

            parser.SetupHelp("?", "help")
                .Callback(text => Console.WriteLine(text));

            return parser;
        }
    }
}
