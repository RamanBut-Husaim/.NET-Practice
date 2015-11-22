using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            WarmSequential(fileStrings);
            WarmParallel(fileStrings);

            stopWatch.Stop();

            stopWatch.Reset();

            stopWatch.Start();
            var sequentialResult = ProcessSequential(fileStrings);
            stopWatch.Stop();

            Console.WriteLine("Elapsed time for sequential processing: {0}", stopWatch.ElapsedTicks.ToString());

            stopWatch.Reset();

            stopWatch.Start();

            var parallelResult = ProcessParallel(fileStrings);

            stopWatch.Stop();

            Console.WriteLine("Elapsed time for parallel processing: {0}", stopWatch.ElapsedTicks.ToString());

            bool equal = sequentialResult.SequenceEqual(parallelResult);

            Console.WriteLine("The sequences are equal: {0}", equal);
            Console.ReadLine();
        }

        private static void WarmSequential(IEnumerable<string> strings)
        {
            for (int i = 0; i < WarmCounter; ++i)
            {
                var fileProcessorFactory = new FileProcessorFactory();
                var sequenceManager = new SequentialFileProcessorManager(fileProcessorFactory);

                sequenceManager.ProcessFile(strings);
            }
        }

        private static void WarmParallel(IEnumerable<string> strings)
        {
            for (int i = 0; i < WarmCounter; ++i)
            {
                var fileProcessorFactory = new FileProcessorFactory();
                var sequenceManager = new ParallelFileProcessorManager(fileProcessorFactory);

                sequenceManager.ProcessFile(strings);
            }
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
