using System.IO;
using System.Threading.Tasks;
using MessageQueues.Core.Messages;
using MessageQueues.Core.Operations;
using MessageQueues.Core.Operations.Copy;

namespace MessageQueues.HarvesterHost.Core.FileOperations.Copy
{
    public sealed class CopyOperation : ICopyOperation
    {
        private const int DefaultBufferSize = 1024;

        private readonly string _path;
        private readonly IFileSender _fileSender;

        public CopyOperation(string path, IFileSender fileSender)
        {
            _path = path;
            _fileSender = fileSender;
        }

        public string Path
        {
            get { return _path; }
        }

        public async Task Perform()
        {
            using (var sourceStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, FileOptions.Asynchronous))
            {
                byte[] fileContent = new byte[sourceStream.Length];
                await sourceStream.ReadAsync(fileContent, 0, fileContent.Length);
                var message = new FileMessage
                {
                    FileContent = fileContent,
                    FileName = System.IO.Path.GetFileName(this.Path),
                    OperationType = OperationType.Changed
                };

                _fileSender.Send(message);
            }
        }
    }
}
