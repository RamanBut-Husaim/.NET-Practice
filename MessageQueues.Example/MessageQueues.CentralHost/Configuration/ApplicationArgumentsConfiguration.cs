using System;

using Fclp;

namespace MessageQueues.CentralHost.Configuration
{
    internal sealed class ApplicationArgumentsConfiguration
    {
        public static FluentCommandLineParser<ApplicationArguments> CreateCommandLineParser()
        {
            var parser = new FluentCommandLineParser<ApplicationArguments>();

            parser.Setup(arg => arg.ResultFolder)
                .As('o', "outputFolder")
                .Required()
                .WithDescription("A folder that will contain data from different harvesters");

            parser.Setup(arg => arg.ServiceName)
                  .As('n', "name")
                  .WithDescription("The name of the service to be registered.");

            parser.SetupHelp("?", "help")
                .Callback(text => Console.WriteLine(text));

            return parser;
        }
    }
}
