using System.Threading.Tasks;

using MessageQueues.Core.Messages;
using MessageQueues.Core.Operations.Rename;

namespace MessageQueues.HarvesterHost.Core.FileOperations.Rename
{
    public sealed class RenameOperation : IRenameOperation
    {
        private readonly string _oldPath;
        private readonly string _newPath;
        private readonly IFileSender _fileSender;

        public RenameOperation(string oldPath, string newPath, IFileSender fileSender)
        {
            _oldPath = oldPath;
            _newPath = newPath;
            _fileSender = fileSender;
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
            var fileMessage = new FileMessage
            {
                FileName = this.OldPath,
                NewName = this.NewPath,
                OperationType = OperationType.Renamed
            };

            _fileSender.Send(fileMessage);
        }
    }
}
