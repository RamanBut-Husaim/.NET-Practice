using System.IO;
using System.Threading.Tasks;
using MessageQueues.HarvesterHost.Core.FileOperations.Copy;

namespace MessageQueues.HarvesterHost.Core.FileOperations.Synchronization
{
    public sealed class SynchronizationOperation : ISynchronizationOperation
    {
        private readonly ICopyOperation _copyOperation;

        public SynchronizationOperation(ICopyOperation copyOperation)
        {
            _copyOperation = copyOperation;
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
            if (!File.Exists(this.DestinationPath))
            {
                await _copyOperation.Perform();
            }
        }
    }
}
