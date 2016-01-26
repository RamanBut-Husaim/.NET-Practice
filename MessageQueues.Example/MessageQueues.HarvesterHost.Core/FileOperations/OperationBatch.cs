using System.Collections.Generic;
using System.IO;
using MessageQueues.HarvesterHost.Core.Watching;

namespace MessageQueues.HarvesterHost.Core.FileOperations
{
    public sealed class OperationBatch
    {
        private readonly string _fullPath;
        private readonly string _fileName;
        private readonly IList<FileSystemWatcherEventArgs> _operations;

        public OperationBatch(string fullPath, IList<FileSystemWatcherEventArgs> operations)
        {
            _fullPath = fullPath;
            _fileName = Path.GetFileName(fullPath);
            _operations = operations;
        }

        public string FullPath
        {
            get { return _fullPath; }
        }

        public string FileName
        {
            get { return _fileName; }
        }

        public IList<FileSystemWatcherEventArgs> Operations
        {
            get { return _operations; }
        }
    }
}
