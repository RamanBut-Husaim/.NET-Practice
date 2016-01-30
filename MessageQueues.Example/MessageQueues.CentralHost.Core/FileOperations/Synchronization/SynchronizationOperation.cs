using System.IO;
using System.Threading.Tasks;

using MessageQueues.Core.Operations.Copy;
using MessageQueues.Core.Operations.Synchronization;

namespace MessageQueues.CentralHost.Core.FileOperations.Synchronization
{
    public sealed class SynchronizationOperation : ISynchronizationOperation
    {
        private readonly ICopyOperation _copyOperation;

        public SynchronizationOperation(ICopyOperation copyOperation)
        {
            _copyOperation = copyOperation;
        }

        public string Path
        {
            get { return _copyOperation.Path; }
        }

        public async Task Perform()
        {
            if (!File.Exists(this.Path))
            {
                await _copyOperation.Perform();
            }
        }
    }
}
