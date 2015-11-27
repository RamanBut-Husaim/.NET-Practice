namespace Async.Example.Model
{
    public sealed class DownloaderFactory
    {
        public Downloader Create()
        {
            return new Downloader();
        }
    }
}
