using System;
using System.ComponentModel;
using System.IO;

using MessageQueues.Core.Messages;
using MessageQueues.Core.Operations;
using MessageQueues.Core.Operations.Copy;
using MessageQueues.Core.Operations.Rename;
using MessageQueues.Core.Operations.Synchronization;

namespace MessageQueues.CentralHost.Core.FileOperations
{
    public sealed class OperationFactory
    {
        private readonly Func<string, FileMessage, ICopyOperation> _copyOperationFactory;
        private readonly Func<string, string, IRenameOperation> _renameOperationFactory;
        private readonly Func<ICopyOperation, ISynchronizationOperation> _synchronizationOperationFactory;

        private readonly string _destinationPath;

        public OperationFactory(
            Func<string, FileMessage, ICopyOperation> copyOperationFactory,
            Func<string, string, IRenameOperation> renameOperationFactory,
            Func<ICopyOperation, ISynchronizationOperation> synchronizationOperationFactory,
            string destinationPath)
        {
            _copyOperationFactory = copyOperationFactory;
            _renameOperationFactory = renameOperationFactory;
            _synchronizationOperationFactory = synchronizationOperationFactory;
            _destinationPath = destinationPath;
        }

        public IOperation Create(FileMessage message)
        {
            IOperation result;

            switch (message.OperationType)
            {
                case OperationType.Renamed:
                {
                    result = this.CreateRenameOperation(message);
                    break;
                }
                case OperationType.Synchronize:
                {
                    result = this.CreateSynchronizationOperation(message);
                    break;
                }
                default:
                {
                    result = this.CreateCopyOperation(message);
                    break;
                }
            }

            return result;
        }

        private ICopyOperation CreateCopyOperation(FileMessage message)
        {
            string pathWithHarvesterName = Path.Combine(_destinationPath, message.Harvester);

            return _copyOperationFactory.Invoke(pathWithHarvesterName, message);
        }

        private IRenameOperation CreateRenameOperation(FileMessage message)
        {
            string oldPath = Path.Combine(_destinationPath, message.Harvester, message.FileName);
            string newPath = Path.Combine(_destinationPath, message.Harvester, message.NewName);

            return _renameOperationFactory.Invoke(oldPath, newPath);
        }

        private ISynchronizationOperation CreateSynchronizationOperation(FileMessage message)
        {
            ICopyOperation copyOperation = this.CreateCopyOperation(message);

            return _synchronizationOperationFactory.Invoke(copyOperation);
        }
    }
}
