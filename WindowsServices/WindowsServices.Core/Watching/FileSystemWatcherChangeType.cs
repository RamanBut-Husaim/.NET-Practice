using System;

namespace WindowsServices.Core.Watching
{
    [Flags]
    public enum FileSystemWatcherChangeType
    {
        Created = 1,
        Deleted = 2,
        Changed = 4,
        Renamed = 8,
        All = Renamed | Changed | Deleted | Created,
    }
}
