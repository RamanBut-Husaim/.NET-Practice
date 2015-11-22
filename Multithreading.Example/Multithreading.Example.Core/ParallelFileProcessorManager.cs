using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multithreading.Example.Core
{
    public sealed class ParallelFileProcessorManager : IFileProcessorManager
    {
        private readonly FileProcessorFactory _fileProcessorFactory;

        public ParallelFileProcessorManager(FileProcessorFactory fileProcessorFactory)
        {
            _fileProcessorFactory = fileProcessorFactory;
        }

        public IEnumerable<string> ProcessFile(IEnumerable<string> stringsToProcess)
        {
            string[] fileStrings = stringsToProcess.ToArray();

            Task<string>[] computationTasks = new Task<string>[fileStrings.Length];

            for (int i = 0; i < fileStrings.Length; ++i)
            {
                computationTasks[i] = new Task<string>(this.ProcessFileString, fileStrings[i]);
                computationTasks[i].Start();
            }

            Task.WaitAll(computationTasks);

            var resultStrings = new List<string>();
            for (int i = 0; i < computationTasks.Length; ++i)
            {
                resultStrings.Add(computationTasks[i].Result);
            }

            return resultStrings;
        }

        private string ProcessFileString(object stringObject)
        {
            string fileString = stringObject as string;

            IFileProcessor fileProcessor = _fileProcessorFactory.Create(fileString);

            string resultString = fileProcessor.Process();

            return resultString;
        }
    }
}
