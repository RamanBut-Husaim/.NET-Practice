using System.Threading.Tasks;

namespace MessageQueues.HarvesterHost.Core.FileOperations
{
    public interface IOperation
    {
        Task Perform();
    }
}
