using System.IO;
using System.Threading.Tasks;

namespace WindowsServices.Core.FileOperations
{
    public sealed class PollingRenameOperation : IRenameOperation
    {
        private readonly IRenameOperation _renameOperation;
        private readonly IPollingManager _pollingManager;

        public PollingRenameOperation(
            IRenameOperation renameOperation,
            IPollingManager pollingManager)
        {
            _renameOperation = renameOperation;
            _pollingManager = pollingManager;
        }

        public string OldPath
        {
            get { return _renameOperation.OldPath; }
        }

        public string NewPath
        {
            get { return _renameOperation.NewPath; }
        }

        public async Task Perform()
        {
            await _pollingManager.Perform<DirectoryNotFoundException>(() => _renameOperation.Perform());
        }
    }
}
