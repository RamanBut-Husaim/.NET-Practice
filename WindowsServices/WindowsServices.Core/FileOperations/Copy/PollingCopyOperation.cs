using System.IO;
using System.Threading.Tasks;

namespace WindowsServices.Core.FileOperations.Copy
{
    public sealed class PollingCopyOperation : ICopyOperation
    {
        private readonly ICopyOperation _copyOperation;
        private readonly IPollingManager _pollingManager;

        public PollingCopyOperation(ICopyOperation copyOperation, IPollingManager pollingManager)
        {
            _copyOperation = copyOperation;
            _pollingManager = pollingManager;
        }

        public string SourcePath
        {
            get { return _copyOperation.SourcePath; }
        }

        public string DestinationPath
        {
            get { return _copyOperation.DestinationPath; }
        }

        public async Task Perform()
        {
            await _pollingManager.Perform<DirectoryNotFoundException>(() => _copyOperation.Perform());
        }
    }
}
