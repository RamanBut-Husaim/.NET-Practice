using System;

namespace WindowsServices.Core.Watching
{
    public sealed class FolderWatcherFactory : IFolderWatcherFactory
    {
        private readonly Func<string, IFolderWatcher> _factory;

        public FolderWatcherFactory(Func<string, IFolderWatcher> factory)
        {
            _factory = factory;
        }

        public IFolderWatcher Create(string folderPath)
        {
            return _factory.Invoke(folderPath);
        }

        public void Release(IFolderWatcher folderWatcher)
        {
            var disposableInstance = folderWatcher as IDisposable;
            if (disposableInstance != null)
            {
                disposableInstance.Dispose();
            }
        }
    }
}
