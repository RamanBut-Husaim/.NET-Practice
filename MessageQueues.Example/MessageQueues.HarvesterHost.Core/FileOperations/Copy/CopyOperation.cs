using System.IO;
using System.Threading.Tasks;
using MessageQueues.Core.Messages;

namespace MessageQueues.HarvesterHost.Core.FileOperations.Copy
{
    public sealed class CopyOperation : ICopyOperation
    {
        private const int DefaultBufferSize = 1024;

        private readonly string _sourcePath;
        private readonly IFileSender _fileSender;

        public CopyOperation(string sourcePath, IFileSender fileSender)
        {
            _sourcePath = sourcePath;
            _fileSender = fileSender;
        }

        public string SourcePath
        {
            get { return _sourcePath; }
        }

        public async Task Perform()
        {
            using (var sourceStream = new FileStream(_sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, FileOptions.Asynchronous))
            {
                byte[] fileContent = new byte[sourceStream.Length];
                await sourceStream.ReadAsync(fileContent, 0, fileContent.Length);
                var message = new FileMessage
                {
                    FileContent = fileContent,
                    FileName = Path.GetFileName(this.SourcePath),
                    OperationType = OperationType.Changed
                };

                _fileSender.Send(message);
            }
        }
    }
}
