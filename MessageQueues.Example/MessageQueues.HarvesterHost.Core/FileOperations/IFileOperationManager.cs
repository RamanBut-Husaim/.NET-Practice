using System.Threading.Tasks;

namespace MessageQueues.HarvesterHost.Core.FileOperations
{
    public interface IFileOperationManager
    {
        Task ProcessFileOperations(OperationBatch operationBatch);
    }
}
