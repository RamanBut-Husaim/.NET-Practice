using System.Threading.Tasks;

namespace WindowsServices.Core.FileOperations
{
    public interface IFileOperationManager
    {
        Task ProcessFileOperations(OperationBatch operationBatch);
    }
}
