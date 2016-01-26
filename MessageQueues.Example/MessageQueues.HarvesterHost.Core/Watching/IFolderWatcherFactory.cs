namespace MessageQueues.HarvesterHost.Core.Watching
{
    public interface IFolderWatcherFactory
    {
        IFolderWatcher Create(string folderPath);

        void Release(IFolderWatcher folderWatcher);
    }
}
