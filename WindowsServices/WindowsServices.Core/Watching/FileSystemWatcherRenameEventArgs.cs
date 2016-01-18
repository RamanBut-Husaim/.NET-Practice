namespace WindowsServices.Core.Watching
{
    public sealed class FileSystemWatcherRenameEventArgs : FileSystemWatcherEventArgs
    {
        public FileSystemWatcherRenameEventArgs(
            string name,
            string fullPath,
            FileSystemWatcherChangeType changeType,
            string oldName,
            string oldPath) : base(name, fullPath, changeType)
        {
            this.OldName = oldName;
            this.OldPath = oldPath;
        }

        public string OldName { get; private set; }

        public string OldPath { get; private set; }

        public override string ToString()
        {
            return string.Format("File renamed from '{0}' to '{1}'", this.OldName, this.Name);
        }
    }
}
