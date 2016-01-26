using System.Collections.Generic;
using MessageQueues.HarvesterHost.Core.Watching;

namespace MessageQueues.HarvesterHost.Core.Services
{
    public interface IFileService
    {
        void Process(IEnumerable<FileSystemWatcherEventArgs> watchArgs);
    }
}
