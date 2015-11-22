using System;
using Fclp;

namespace Multithreading.Example.FileGeneration
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

            parser.Setup(arg => arg.LineCount)
                .As('l', "lineCount")
                .WithDescription("Optional. The number of lines in the result file. Default is 12.")
                .SetDefault(12);

            parser.Setup(arg => arg.NumbersPerLine)
                .As('n', "numbersPerLine")
                .WithDescription("Optional. How many numbers will be placed in the line. Default is 30.")
                .SetDefault(30);

            parser.SetupHelp("?", "help")
                .Callback(text => Console.WriteLine(text));

            return parser;
        }
    }
}
