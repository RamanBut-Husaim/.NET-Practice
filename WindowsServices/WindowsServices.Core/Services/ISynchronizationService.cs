using System.Threading.Tasks;

namespace WindowsServices.Core.Services
{
    public interface ISynchronizationService
    {
        Task PerformSynchronization();
    }
}
