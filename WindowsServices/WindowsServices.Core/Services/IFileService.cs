using System.Collections.Generic;
using WindowsServices.Core.Watching;

namespace WindowsServices.Core.Services
{
    public interface IFileService
    {
        void Process(IEnumerable<FileSystemWatcherEventArgs> watchArgs);
    }
}
