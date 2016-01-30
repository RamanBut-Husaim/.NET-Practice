using System;
using System.Threading.Tasks;

namespace MessageQueues.Core.Polling
{
    public interface IPollingManager
    {
        int WaitInterval { get; }

        int AccessCount { get; }

        Task Perform<TException>(Func<Task> action) where TException : Exception;
    }
}
