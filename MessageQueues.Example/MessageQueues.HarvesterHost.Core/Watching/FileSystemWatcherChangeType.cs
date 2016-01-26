using System;

namespace MessageQueues.HarvesterHost.Core.Watching
{
    [Flags]
    public enum FileSystemWatcherChangeType
    {
        Created = 1,
        Deleted = 2,
        Changed = 4,
        Renamed = 8,
        Synchronize = 16,
        All = Renamed | Changed | Deleted | Created | Synchronize
    }
}
