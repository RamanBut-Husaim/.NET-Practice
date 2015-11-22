using System;
using System.Collections.Generic;
using System.Linq;
using Fclp;
using Multithreading.Example.Core;

namespace Multithreading.Example
{
    public sealed class Program
    {
        private const int WarmCounter = 4;

        public static void Main(string[] args)
        {
            FluentCommandLineParser<Arguments> parser = new ArgumentConfiguration().Create();
            ICommandLineParserResult parseResult = parser.Parse(args);

            if (parseResult.HasErrors || parseResult.EmptyArgs)
            {
                parser.HelpOption.ShowHelp(parser.Options);
                return;
            }

            var fileContentProviderFactory = new FileContentProviderFactory();

            IEnumerable<string> fileStrings = Enumerable.Empty<string>();

            using (var fileContentProvider = fileContentProviderFactory.Create(parser.Object.FileName))
            {
                fileStrings = fileContentProvider.GetFileContent();
            }

            var sequentialResult = ProcessSequential(fileStrings);
            var parallelResult = ProcessParallel(fileStrings);

            bool equal = sequentialResult.SequenceEqual(parallelResult);

            Console.WriteLine("The sequences are equal: {0}", equal);
            Console.ReadLine();
        }

        private static IEnumerable<string> ProcessSequential(IEnumerable<string> strings)
        {
            var fileProcessorFactory = new FileProcessorFactory();
            var sequenceManager = new SequentialFileProcessorManager(fileProcessorFactory);

            IEnumerable<string> resultStrings = sequenceManager.ProcessFile(strings);

            return resultStrings;
        }

        private static IEnumerable<string> ProcessParallel(IEnumerable<string> strings)
        {
            var fileProcessorFactory = new FileProcessorFactory();
            var sequenceManager = new ParallelFileProcessorManager(fileProcessorFactory);

            IEnumerable<string> resultStrings = sequenceManager.ProcessFile(strings);

            return resultStrings;
        }
    }
}
