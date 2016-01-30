using System.IO;
using System.Threading.Tasks;

using MessageQueues.Core.Operations.Rename;

namespace MessageQueues.CentralHost.Core.FileOperations.Rename
{
    public sealed class RenameOperation : IRenameOperation
    {
        private const int DefaultBufferSize = 1024;

        private readonly string _oldPath;
        private readonly string _newPath;

        public RenameOperation(string oldPath, string newPath)
        {
            _oldPath = oldPath;
            _newPath = newPath;
        }

        public string OldPath
        {
            get { return _oldPath; }
        }

        public string NewPath
        {
            get { return _newPath; }
        }

        public async Task Perform()
        {
            using (var sourceStream = new FileStream(_oldPath, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, FileOptions.Asynchronous | FileOptions.DeleteOnClose))
            {
                using (var destinationStream = new FileStream(_newPath, FileMode.Create, FileAccess.Write, FileShare.None, DefaultBufferSize, FileOptions.Asynchronous))
                {
                    await sourceStream.CopyToAsync(destinationStream);
                }
            }
        }
    }
}
