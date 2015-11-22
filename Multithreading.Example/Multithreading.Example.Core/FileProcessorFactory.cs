using System;

namespace Multithreading.Example.Core
{
    public sealed class FileProcessorFactory
    {
        public IFileProcessor Create(string fileString)
        {
            if (fileString == null)
            {
                throw new ArgumentNullException("fileString");
            }

            return new FileProcessor(fileString);
        }
    }
}
