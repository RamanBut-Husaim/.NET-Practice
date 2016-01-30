using System.IO;
using System.Threading.Tasks;

using MessageQueues.Core.Messages;
using MessageQueues.Core.Operations.Copy;

namespace MessageQueues.CentralHost.Core.FileOperations.Copy
{
    public sealed class CopyOperation : ICopyOperation
    {
        private const int DefaultBufferSize = 1024;

        private readonly FileMessage _fileMessage;
        private readonly string _path;

        public CopyOperation(string destinationPath, FileMessage fileMessage)
        {
            _fileMessage = fileMessage;
            _path = System.IO.Path.Combine(destinationPath, fileMessage.FileName);
        }

        public string Path
        {
            get { return _path; }
        }

        public async Task Perform()
        {
            using (var sourceStream = new FileStream(_path, FileMode.Create, FileAccess.Write, FileShare.None, DefaultBufferSize, FileOptions.Asynchronous))
            {
                await sourceStream.WriteAsync(_fileMessage.FileContent, 0, _fileMessage.FileContent.Length);
            }
        }
    }
}
