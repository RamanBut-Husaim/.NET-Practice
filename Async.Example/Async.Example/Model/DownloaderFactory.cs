namespace Async.Example.Model
{
    public sealed class DownloaderFactory
    {
        public Downloader Create(IProgressNotifier progressNotifier)
        {
            return new Downloader(progressNotifier);
        }
    }
}
