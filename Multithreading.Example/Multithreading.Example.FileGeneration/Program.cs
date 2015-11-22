using System;
using Fclp;

namespace Multithreading.Example.FileGeneration
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            FluentCommandLineParser<Arguments> parser = new ArgumentConfiguration().Create();
            ICommandLineParserResult parseResult = parser.Parse(args);

            if (parseResult.HasErrors || parseResult.EmptyArgs)
            {
                parser.HelpOption.ShowHelp(parser.Options);
                return;
            }

            var fileGeneratorFactory = new FileGeneratorFactory();
            var fileGenerator = fileGeneratorFactory.Create(parser.Object.LineCount, parser.Object.NumbersPerLine);
            fileGenerator.Generate(parser.Object.FileName);

            Console.WriteLine("The file '{0}' has been generated successfully. ", parser.Object.FileName);
        }
    }
}
