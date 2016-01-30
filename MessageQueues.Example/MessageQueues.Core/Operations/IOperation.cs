using System.Threading.Tasks;

namespace MessageQueues.Core.Operations
{
    public interface IOperation
    {
        Task Perform();
    }
}
