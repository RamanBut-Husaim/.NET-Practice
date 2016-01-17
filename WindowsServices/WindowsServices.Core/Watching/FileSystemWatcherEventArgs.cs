using System;

namespace WindowsServices.Core.Watching
{
    public class FileSystemWatcherEventArgs : EventArgs
    {
        public FileSystemWatcherEventArgs(
            string name,
            string fullPath,
            FileSystemWatcherChangeType changeType)
        {
            Name = name;
            FullPath = fullPath;
            ChangeType = changeType;
        }

        public string Name { get; private set; }

        public string FullPath { get; private set; }

        public FileSystemWatcherChangeType ChangeType { get; private set; }
    }
}
