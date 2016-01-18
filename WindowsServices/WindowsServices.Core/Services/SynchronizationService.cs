using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WindowsServices.Core.FileOperations;
using WindowsServices.Core.Watching;
using NLog;

namespace WindowsServices.Core.Services
{
    public sealed class SynchronizationService : ISynchronizationService
    {
        private readonly string _sourcePath;
        private readonly string _destinationPath;
        private readonly FileOperationManagerFactory _fileOperationManagerFactory;
        private readonly ILogger _logger;

        public SynchronizationService(
            string sourcePath,
            string destinationPath,
            FileOperationManagerFactory fileOperationManagerFactory,
            ILogger logger)
        {
            _sourcePath = sourcePath;
            _destinationPath = destinationPath;
            _fileOperationManagerFactory = fileOperationManagerFactory;
            _logger = logger;
        }

        public async Task PerformSynchronization()
        {
            foreach (var file in Directory.EnumerateFiles(_sourcePath))
            {
                _logger.Trace("[Start]: Synchronization '{0}'", file);
                FileSystemWatcherEventArgs args = this.CreateArgs(file);
                var fileOperationManager = _fileOperationManagerFactory.Create(_destinationPath);
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
