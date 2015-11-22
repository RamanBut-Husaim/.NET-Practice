using System;
using Fclp;

namespace Multithreading.Example
{
    public sealed class ArgumentConfiguration
    {
        public FluentCommandLineParser<Arguments> Create()
        {
            var parser = new FluentCommandLineParser<Arguments>();
            parser.Setup(arg => arg.FileName)
                .As('f', "file")
                .WithDescription("The path to the file.")
                .Required();

            parser.SetupHelp("?", "help")
                .Callback(text => Console.WriteLine(text));

            return parser;
        }
    }
}
