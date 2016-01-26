using System.IO;
using System.Threading.Tasks;

namespace MessageQueues.HarvesterHost.Core.FileOperations.Copy
{
    public sealed class CopyOperation : ICopyOperation
    {
        private const int DefaultBufferSize = 1024;

        private readonly string _sourcePath;
        private readonly string _destinationPath;

        public CopyOperation(string sourcePath, string destinationPath)
        {
            _sourcePath = sourcePath;
            _destinationPath = destinationPath;
        }

        public string SourcePath
        {
            get { return _sourcePath; }
        }

        public string DestinationPath
        {
            get { return _destinationPath; }
        }

        public async Task Perform()
        {
            using (var sourceStream = new FileStream(_sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, FileOptions.Asynchronous))
            {
                using (var destinationStream = new FileStream(_destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, DefaultBufferSize, FileOptions.Asynchronous))
                {
                    await sourceStream.CopyToAsync(destinationStream);
                }
            }
        }
    }
}
