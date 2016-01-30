using System.Threading.Tasks;

namespace MessageQueues.CentralHost.Core.FileOperations
{
    public interface IFileOperationProcessor
    {
        Task ProcessBatch(OperationBatch operationBatch);
    }
}
