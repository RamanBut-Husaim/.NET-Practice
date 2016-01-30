using System;

namespace MessageQueues.Core.Messages
{
    [Flags]
    [Serializable]
    public enum OperationType
    {
        Created = 1,
        Deleted = 2,
        Changed = 4,
        Renamed = 8,
        Synchronize = 16,
        All = Renamed | Changed | Deleted | Created | Synchronize
    }
}
