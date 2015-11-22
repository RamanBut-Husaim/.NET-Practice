using System.Collections.Generic;

namespace Multithreading.Example.Core
{
    public interface IFileProcessorManager
    {
        IEnumerable<string> ProcessFile(IEnumerable<string> stringsToProcess);
    }
}
