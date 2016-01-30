using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageQueues.HarvesterHost.Core.FileOperations;
using MessageQueues.HarvesterHost.Core.Watching;
using NLog;

namespace MessageQueues.HarvesterHost.Core.Services
{
    public sealed class FileService : IFileService
    {
        private readonly Func<IFileOperationManager> _fileOperationManagerFactory;
        private readonly ILogger _logger;

        public FileService(
            Func<IFileOperationManager> fileOperationManagerFactory,
            ILogger logger)
        {
            _fileOperationManagerFactory = fileOperationManagerFactory;
            _logger = logger;
        }

        public void Process(IEnumerable<FileSystemWatcherEventArgs> watchArgs)
        {
            IEnumerable<IGrouping<string, FileSystemWatcherEventArgs>> groupedFileModifications = watchArgs
                .GroupBy(this.GetKey)
                .Select(p => p);

            IList<Task> jobs = new List<Task>();

            foreach (var fileModifications in groupedFileModifications)
            {
                _logger.Trace("[Start]: File processing {0}", fileModifications.Key);
                var operationBatch = new OperationBatch(fileModifications.Key, fileModifications.ToList());
                IFileOperationManager operationManager = _fileOperationManagerFactory.Invoke();
                Task task = operationManager.ProcessFileOperations(operationBatch);
                jobs.Add(task);
            }

            try
            {
                Task.WaitAll(jobs.ToArray());
            }
            catch (AggregateException ex)
            {
                for (int i = 0; i < ex.InnerExceptions.Count; ++i)
                {
                    _logger.Error(ex.InnerExceptions[i]);
                }
            }
        }

        private string GetKey(FileSystemWatcherEventArgs watchArg)
        {
            var renameArgs = watchArg as FileSystemWatcherRenameEventArgs;
            if (renameArgs != null)
            {
                return renameArgs.OldPath;
            }

            return watchArg.FullPath;
        }
    }
}
