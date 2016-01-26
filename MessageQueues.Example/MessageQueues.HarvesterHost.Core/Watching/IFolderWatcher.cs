using System;

namespace MessageQueues.HarvesterHost.Core.Watching
{
    public interface IFolderWatcher
    {
        event EventHandler<FileSystemWatcherEventArgs> Created;
        event EventHandler<FileSystemWatcherEventArgs> Changed;
        event EventHandler<FileSystemWatcherRenameEventArgs> Renamed;

        void StartWatching();
        void StopWatching();
    }
}
