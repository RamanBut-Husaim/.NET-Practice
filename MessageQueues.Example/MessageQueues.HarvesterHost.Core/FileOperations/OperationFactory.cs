using System;
using System.IO;
using MessageQueues.HarvesterHost.Core.FileOperations.Copy;
using MessageQueues.HarvesterHost.Core.FileOperations.Rename;
using MessageQueues.HarvesterHost.Core.FileOperations.Synchronization;
using MessageQueues.HarvesterHost.Core.Watching;

namespace MessageQueues.HarvesterHost.Core.FileOperations
{
    public sealed class OperationFactory
    {
        private readonly Func<string, string, IRenameOperation> _renameOperationFactory;
        private readonly Func<string, string, ICopyOperation> _copyOperationFactory;
        private readonly Func<string, string, ISynchronizationOperation> _synchronizationOperationFactory;

        public OperationFactory(
            Func<string ,string, IRenameOperation> renameOperationFactory,
            Func<string ,string, ICopyOperation> copyOperationFactory,
            Func<string, string, ISynchronizationOperation> synchronizationOperationFactory)
        {
            _renameOperationFactory = renameOperationFactory;
            _copyOperationFactory = copyOperationFactory;
            _synchronizationOperationFactory = synchronizationOperationFactory;
        }

        public IOperation Create(FileSystemWatcherEventArgs eventArgs, string destinationPath)
        {
            IOperation result;

            switch (eventArgs.ChangeType)
            {
                case FileSystemWatcherChangeType.Renamed:
                {
                    result = this.CreateRenameOperation(eventArgs, destinationPath);
                    break;
                }
                case FileSystemWatcherChangeType.Synchronize:
                {
                    result = this.CreateSynchronizeOperation(eventArgs, destinationPath);
                    break;
                }
                default:
                {
                    result = this.CreateCopyOperation(eventArgs, destinationPath);
                    break;
                }
            }

            return result;
        }

        private IOperation CreateRenameOperation(FileSystemWatcherEventArgs eventArgs, string destinationPath)
        {
            var renameArgs = eventArgs as FileSystemWatcherRenameEventArgs;
            if (renameArgs == null)
            {
                throw new ArgumentException("The operation is specified as 'Rename' but the actual arguments is different.");
            }

            string oldFullPath = Path.Combine(destinationPath, renameArgs.OldName);
            string newFullPath = Path.Combine(destinationPath, renameArgs.Name);
            return _renameOperationFactory.Invoke(oldFullPath, newFullPath);
        }

        private IOperation CreateCopyOperation(FileSystemWatcherEventArgs eventArgs, string destinationPath)
        {
            string destinationFullPath = Path.Combine(destinationPath, eventArgs.Name);
            return _copyOperationFactory.Invoke(eventArgs.FullPath, destinationFullPath);
        }

        private IOperation CreateSynchronizeOperation(FileSystemWatcherEventArgs eventArgs, string destinationPath)
        {
            string destinationFullPath = Path.Combine(destinationPath, eventArgs.Name);
            return _synchronizationOperationFactory.Invoke(eventArgs.FullPath, destinationFullPath);
        }
    }
}
