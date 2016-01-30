using System.IO;
using System.Threading.Tasks;

using MessageQueues.Core.Polling;

namespace MessageQueues.Core.Operations.Synchronization
{
    public sealed class PollingSynchronizationOperation : ISynchronizationOperation
    {
        private readonly ISynchronizationOperation _synchronizationOperation;
        private readonly IPollingManager _pollingManager;

        public PollingSynchronizationOperation(
            ISynchronizationOperation synchronizationOperation,
            IPollingManager pollingManager)
        {
            _synchronizationOperation = synchronizationOperation;
            _pollingManager = pollingManager;
        }

        public string Path
        {
            get { return _synchronizationOperation.Path; }
        }

        public async Task Perform()
        {
            await _pollingManager.Perform<DirectoryNotFoundException>(() => _synchronizationOperation.Perform());
        }
    }
}
