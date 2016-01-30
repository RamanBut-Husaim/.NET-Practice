using System.IO;
using System.Threading.Tasks;

using MessageQueues.Core.Polling;

namespace MessageQueues.Core.Operations.Copy
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

        public string Path
        {
            get { return _copyOperation.Path; }
        }

        public async Task Perform()
        {
            await _pollingManager.Perform<DirectoryNotFoundException>(() => _copyOperation.Perform());
        }
    }
}
