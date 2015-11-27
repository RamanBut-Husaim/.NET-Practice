using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Async.Example.Model
{
    public sealed class Downloader : IDisposable
    {
        private readonly WebClient _webClient;
        private bool _disposed;

        public Downloader()
        {
            _webClient = new WebClient();
        }

        public async Task DownloadPagesAsync(IList<string> pages)
        {
            this.GuardDisposed();

            if (pages == null)
            {
                throw new ArgumentNullException("pages");
            }

            await Task.Delay(TimeSpan.FromSeconds(10));

            var resultContent = new List<string>();

            foreach (var page in pages)
            {
                var content = await _webClient.DownloadStringTaskAsync(page);
                resultContent.Add(content);
            }
        }

        private void GuardDisposed()
        {
            if (_disposed == true)
            {
                throw new ObjectDisposedException("The object has been disposed.");
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                if (disposing)
                {
                    _webClient.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
