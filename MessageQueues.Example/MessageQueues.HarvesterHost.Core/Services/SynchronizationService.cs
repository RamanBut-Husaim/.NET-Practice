using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MessageQueues.HarvesterHost.Core.FileOperations;
using MessageQueues.HarvesterHost.Core.Watching;
using NLog;

namespace MessageQueues.HarvesterHost.Core.Services
{
    public sealed class SynchronizationService : ISynchronizationService
    {
        private readonly string _sourcePath;
        private readonly Func<IFileOperationManager> _fileOperationManagerFactory;
        private readonly ILogger _logger;

        public SynchronizationService(
            string sourcePath,
            Func<IFileOperationManager> fileOperationManagerFactory,
            ILogger logger)
        {
            _sourcePath = sourcePath;
            _fileOperationManagerFactory = fileOperationManagerFactory;
            _logger = logger;
        }

        public async Task PerformSynchronization()
        {
            foreach (var file in Directory.EnumerateFiles(_sourcePath))
            {
                _logger.Trace("[Start]: Synchronization '{0}'", file);
                FileSystemWatcherEventArgs args = this.CreateArgs(file);
                IFileOperationManager fileOperationManager = _fileOperationManagerFactory.Invoke();
                var operationBatch = new OperationBatch(file, new List<FileSystemWatcherEventArgs> {args});

                try
                {
                    await fileOperationManager.ProcessFileOperations(operationBatch);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
                finally
                {
                    _logger.Trace("[End]: Synchronization '{0}'", file);
                }
            }
        }

        private FileSystemWatcherEventArgs CreateArgs(string fullPath)
        {
            return new FileSystemWatcherEventArgs(Path.GetFileName(fullPath), fullPath, FileSystemWatcherChangeType.Synchronize);
        }
    }
}
