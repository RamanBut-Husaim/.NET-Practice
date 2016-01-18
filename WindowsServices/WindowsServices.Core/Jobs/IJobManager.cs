using System.Collections.Generic;
using WindowsServices.Core.Watching;

namespace WindowsServices.Core.Jobs
{
    public interface IJobManager
    {
        void Process(IEnumerable<FileSystemWatcherEventArgs> watchArgs);
    }
}
