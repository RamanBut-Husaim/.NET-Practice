using WindowsServices.Core.Jobs;

namespace WindowsServices.Core.FileOperations
{
    public sealed class FileOperationManager : IFileOperationManager
    {
        private readonly string _sourcePath;
        private readonly string _destinationPath;

        public FileOperationManager(string sourcePath, string destinationPath)
        {
            _sourcePath = sourcePath;
            _destinationPath = destinationPath;
        }


    }
}
