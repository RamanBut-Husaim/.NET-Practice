using WindowsServices.Core.Jobs;

namespace WindowsServices.Core.FileOperations
{
    public sealed class FileOperationManager : IFileOperationManager
    {
        private readonly OperationFactory _operationFactory;

        public FileOperationManager(OperationFactory operationFactory)
        {
            _operationFactory = operationFactory;
        }


    }
}
