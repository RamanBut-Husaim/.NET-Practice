using Async.Example.Model;

namespace Async.Example.ViewModel
{
    public sealed class DownloadItemFactory
    {
        private readonly DownloaderFactory _downloaderFactory;
        private readonly HashProviderFactory _hashProviderFactory;

        public DownloadItemFactory()
        {
            _downloaderFactory = new DownloaderFactory();
            _hashProviderFactory = new HashProviderFactory();
        }

        public DownloaderItemModel Create(string url)
        {
            return new DownloaderItemModel(url, _downloaderFactory, _hashProviderFactory);
        }
    }
}
