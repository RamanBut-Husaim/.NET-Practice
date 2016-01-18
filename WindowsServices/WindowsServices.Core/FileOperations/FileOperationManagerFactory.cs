using System;

namespace WindowsServices.Core.FileOperations
{
    public sealed class FileOperationManagerFactory
    {
        private readonly Func<string, IFileOperationManager> _factory;

        public FileOperationManagerFactory(Func<string, IFileOperationManager> factory)
        {
            _factory = factory;
        }

        public IFileOperationManager Create(string destinationPath)
        {
            return _factory.Invoke(destinationPath);
        }
    }
}
