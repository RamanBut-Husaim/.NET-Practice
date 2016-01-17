using WindowsServices.Core.Watching;

namespace WindowsServices.Core
{
    public interface IFolderWatcherFactory
    {
        IFolderWatcher Create(string folderPath);

        void Release(IFolderWatcher folderWatcher);
    }
}
