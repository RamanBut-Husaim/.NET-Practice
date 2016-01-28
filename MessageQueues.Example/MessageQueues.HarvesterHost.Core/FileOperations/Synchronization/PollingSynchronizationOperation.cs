using System.IO;
using System.Threading.Tasks;

namespace MessageQueues.HarvesterHost.Core.FileOperations.Synchronization
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

        public string SourcePath
        {
            get { return _synchronizationOperation.SourcePath; }
        }

        public async Task Perform()
        {
            await _pollingManager.Perform<DirectoryNotFoundException>(() => _synchronizationOperation.Perform());
        }
    }
}
