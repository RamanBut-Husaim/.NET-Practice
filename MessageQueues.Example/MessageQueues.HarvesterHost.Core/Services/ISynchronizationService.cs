using System.Threading.Tasks;

namespace MessageQueues.HarvesterHost.Core.Services
{
    public interface ISynchronizationService
    {
        Task PerformSynchronization();
    }
}
