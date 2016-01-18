using System;
using System.IO;
using WindowsServices.Core.Watching;

namespace WindowsServices.Core.FileOperations
{
    public sealed class OperationFactory
    {
        private readonly string _destinationPath;
        private readonly Func<string, string, IRenameOperation> _renameOperationFactory;
        private readonly Func<string, string, ICopyOperation> _copyOperationFactory;

        public OperationFactory(
            string destinationPath,
            Func<string ,string, IRenameOperation> renameOperationFactory,
            Func<string ,string, ICopyOperation> copyOperationFactory)
        {
            _renameOperationFactory = renameOperationFactory;
            _copyOperationFactory = copyOperationFactory;
            _destinationPath = destinationPath;
        }

        public IOperation Create(FileSystemWatcherEventArgs eventArgs)
        {
            var renameArgs = eventArgs as FileSystemWatcherRenameEventArgs;
            if (renameArgs != null)
            {
                string oldFullPath = Path.Combine(_destinationPath, renameArgs.OldName);
                string newFullPath = Path.Combine(_destinationPath, renameArgs.Name);
                return _renameOperationFactory.Invoke(oldFullPath, newFullPath);
            }
            else
            {
                string  destinationFullPath = Path.Combine(_destinationPath, eventArgs.Name);
                return _copyOperationFactory.Invoke(eventArgs.FullPath, destinationFullPath);
            }
        }
    }
}
