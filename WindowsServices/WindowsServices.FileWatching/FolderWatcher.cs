using System;
using System.IO;
using System.Runtime.InteropServices;
using WindowsServices.Core.Watching;

namespace WindowsServices.FileWatching
{
    public sealed class FolderWatcher : IFolderWatcher, IDisposable
    {
        private readonly string _folderToWatchPath;
        private readonly FileSystemWatcher _fileSystemWatcher;

        private bool _disposed;

        [DllImport("shlwapi.dll")]
        private static extern bool PathIsNetworkPath(string path);

        public FolderWatcher(string folderToWatchPath)
        {
            this.GuardDirectoryName(folderToWatchPath);

            _folderToWatchPath = folderToWatchPath;
            _fileSystemWatcher = new FileSystemWatcher(_folderToWatchPath)
            {
                EnableRaisingEvents = false
            };

            _fileSystemWatcher.Created += this.FileSystemWatcherOnCreated;
            _fileSystemWatcher.Changed += this.FileSystemWatcherOnChanged;
            _fileSystemWatcher.Renamed += this.FileSystemWatcherOnRenamed;
        }

        public event EventHandler<FileSystemWatcherEventArgs> Created = delegate { };
        public event EventHandler<FileSystemWatcherEventArgs> Changed = delegate { };
        public event EventHandler<FileSystemWatcherRenameEventArgs> Renamed = delegate { };

        public void StartWatching()
        {
            this.GuardDisposed();

            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        public void StopWatching()
        {
            this.GuardDisposed();

            _fileSystemWatcher.EnableRaisingEvents = false;
        }

        private void GuardDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("Folder Watcher");
            }
        }

        private void GuardDirectoryName(string directoryName)
        {
            if (string.IsNullOrEmpty(directoryName))
            {
                throw new ArgumentException("The directory name is invalid.");
            }

            FileAttributes fileAttributes = File.GetAttributes(directoryName);

            if (!fileAttributes.HasFlag(FileAttributes.Directory))
            {
                throw new ArgumentException("The file specified is not a directory.");
            }

            if (FolderWatcher.PathIsNetworkPath(directoryName))
            {
                throw new ArgumentException("Sorry, network addresses is forbidden for now.");
            }
        }

        private void FileSystemWatcherOnRenamed(object sender, RenamedEventArgs renamedEventArgs)
        {
            this.Renamed(this, renamedEventArgs.ToFileSystemWatcherRenameEventArgs());
        }

        private void FileSystemWatcherOnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            this.Changed(this, fileSystemEventArgs.ToFileSystemWatcherEventArgs());
        }

        private void FileSystemWatcherOnCreated(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            this.Created(this, fileSystemEventArgs.ToFileSystemWatcherEventArgs());
        }

        public override string ToString()
        {
            return _folderToWatchPath;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _fileSystemWatcher.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
