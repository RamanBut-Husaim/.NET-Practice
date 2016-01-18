using System.Threading.Tasks;

namespace WindowsServices.Core.FileOperations
{
    public interface IOperation
    {
        Task Perform();
    }
}
