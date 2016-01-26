using System;

namespace MessageQueues.HarvesterHost.Core.Watching
{
    public class FileSystemWatcherEventArgs : EventArgs
    {
        public FileSystemWatcherEventArgs(
            string name,
            string fullPath,
            FileSystemWatcherChangeType changeType)
        {
            this.Name = name;
            this.FullPath = fullPath;
            this.ChangeType = changeType;
        }

        public string Name { get; private set; }

        public string FullPath { get; private set; }

        public FileSystemWatcherChangeType ChangeType { get; private set; }

        public override string ToString()
        {
            return string.Format("File '{0}' has been changed: {1}", this.Name, this.ChangeType);
        }
    }
}
