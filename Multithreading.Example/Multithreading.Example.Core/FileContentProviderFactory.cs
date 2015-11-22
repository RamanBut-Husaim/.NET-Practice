using System;

namespace Multithreading.Example.Core
{
    public sealed class FileContentProviderFactory
    {
        public FileContentProvider Create(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File name is invalid.", "filePath");
            }

            return new FileContentProvider(filePath);
        }
    }
}
