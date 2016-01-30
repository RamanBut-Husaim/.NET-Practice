using System;
using MessageQueues.HarvesterHost.Core.FileOperations.Copy;
using MessageQueues.HarvesterHost.Core.FileOperations.Rename;
using MessageQueues.HarvesterHost.Core.FileOperations.Synchronization;
using MessageQueues.HarvesterHost.Core.Watching;

namespace MessageQueues.HarvesterHost.Core.FileOperations
{
    public sealed class OperationFactory
    {
        private readonly Func<string, string, IFileSender, IRenameOperation> _renameOperationFactory;
        private readonly Func<string, IFileSender, ICopyOperation> _copyOperationFactory;
        private readonly Func<string, IFileSender, ISynchronizationOperation> _synchronizationOperationFactory;

        public OperationFactory(
            Func<string ,string, IFileSender, IRenameOperation> renameOperationFactory,
            Func<string , IFileSender, ICopyOperation> copyOperationFactory,
            Func<string, IFileSender, ISynchronizationOperation> synchronizationOperationFactory)
        {
            _renameOperationFactory = renameOperationFactory;
            _copyOperationFactory = copyOperationFactory;
            _synchronizationOperationFactory = synchronizationOperationFactory;
        }

        public IOperation Create(FileSystemWatcherEventArgs eventArgs, IFileSender fileSender)
        {
            IOperation result;

            switch (eventArgs.ChangeType)
            {
                case FileSystemWatcherChangeType.Renamed:
                {
                    result = this.CreateRenameOperation(eventArgs, fileSender);
                    break;
                }
                case FileSystemWatcherChangeType.Synchronize:
                {
                    result = this.CreateSynchronizeOperation(eventArgs, fileSender);
                    break;
                }
                default:
                {
                    result = this.CreateCopyOperation(eventArgs, fileSender);
                    break;
                }
            }

            return result;
        }

        private IOperation CreateRenameOperation(FileSystemWatcherEventArgs eventArgs, IFileSender fileSender)
        {
            var renameArgs = eventArgs as FileSystemWatcherRenameEventArgs;
            if (renameArgs == null)
            {
                throw new ArgumentException("The operation is specified as 'Rename' but the actual arguments is different.");
            }

            return _renameOperationFactory.Invoke(renameArgs.OldName, renameArgs.Name, fileSender);
        }

        private IOperation CreateCopyOperation(FileSystemWatcherEventArgs eventArgs, IFileSender fileSender)
        {
            return _copyOperationFactory.Invoke(eventArgs.FullPath, fileSender);
        }

        private IOperation CreateSynchronizeOperation(FileSystemWatcherEventArgs eventArgs, IFileSender fileSender)
        {
            return _synchronizationOperationFactory.Invoke(eventArgs.FullPath, fileSender);
        }
    }
}
