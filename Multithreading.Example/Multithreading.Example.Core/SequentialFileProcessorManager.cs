using System.Collections.Generic;
using System.Linq;

namespace Multithreading.Example.Core
{
    public sealed class SequentialFileProcessorManager : IFileProcessorManager
    {
        private readonly FileProcessorFactory _fileProcessorFactory;

        public SequentialFileProcessorManager(FileProcessorFactory fileProcessorFactory)
        {
            _fileProcessorFactory = fileProcessorFactory;
        }

        public IEnumerable<string> ProcessFile(IEnumerable<string> stringsToProcess)
        {
            string[] fileStrings = stringsToProcess.ToArray();
            var resultStrings = new List<string>(fileStrings.Length);

            for (int i = 0; i < fileStrings.Length; ++i)
            {
                string processedString = this.ProcessFileString(fileStrings[i]);
                resultStrings.Add(processedString);
            }

            return resultStrings;
        }

        private string ProcessFileString(string fileString)
        {
            IFileProcessor fileProcessor = _fileProcessorFactory.Create(fileString);

            string resultString = fileProcessor.Process();

            return resultString;
        }
    }
}
