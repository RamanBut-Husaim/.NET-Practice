using System.Threading.Tasks;
using NLog;

namespace WindowsServices.Core.FileOperations.Rename
{
    public sealed class LoggingPollingRenameOperation : IRenameOperation
    {
        private readonly IRenameOperation _renameOperation;
        private readonly ILogger _logger;

        public LoggingPollingRenameOperation(IRenameOperation renameOperation, ILogger logger)
        {
            _renameOperation = renameOperation;
            _logger = logger;
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
            _logger.Trace("[Start]: Renaming from '{0}' to '{1}'", this.OldPath, this.NewPath);
            await _renameOperation.Perform();
            _logger.Trace("[End]: Renaming from '{0}' to '{1}'", this.OldPath, this.NewPath);
        }
    }
}
