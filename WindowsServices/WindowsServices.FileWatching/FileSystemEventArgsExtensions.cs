using System.IO;
using WindowsServices.Core.Watching;

namespace WindowsServices.FileWatching
{
    public static class FileSystemEventArgsExtensions
    {
        public static FileSystemWatcherEventArgs ToFileSystemWatcherEventArgs(this FileSystemEventArgs @this)
        {
            int enumValue = (int) @this.ChangeType;

            return new FileSystemWatcherEventArgs(@this.Name, @this.FullPath, (FileSystemWatcherChangeType)enumValue);
        }

        public static FileSystemWatcherRenameEventArgs ToFileSystemWatcherRenameEventArgs(this RenamedEventArgs @this)
        {
            int enumValue = (int)@this.ChangeType;

            return new FileSystemWatcherRenameEventArgs(
                @this.Name,
                @this.FullPath,
                (FileSystemWatcherChangeType)enumValue,
                @this.OldName,
                @this.OldFullPath);
        }
    }
}
