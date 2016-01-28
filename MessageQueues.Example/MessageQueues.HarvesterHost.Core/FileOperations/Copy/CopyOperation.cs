using System.IO;
using System.Threading.Tasks;
using MessageQueues.Core;

namespace MessageQueues.HarvesterHost.Core.FileOperations.Copy
{
    public sealed class CopyOperation : ICopyOperation
    {
        private const int DefaultBufferSize = 1024;

        private readonly string _sourcePath;
        private readonly ITransferManager _transferManager;

        public CopyOperation(string sourcePath, ITransferManager transferManager)
        {
            _sourcePath = sourcePath;
            _transferManager = transferManager;
        }

        public string SourcePath
        {
            get { return _sourcePath; }
        }

        public async Task Perform()
        {
            using (
                var sourceStream = new FileStream(_sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read,
                    DefaultBufferSize, FileOptions.Asynchronous))
            {
                byte[] fileContent = new byte[sourceStream.Length];
                await sourceStream.ReadAsync(fileContent, 0, fileContent.Length);

            }
        }
    }
}
