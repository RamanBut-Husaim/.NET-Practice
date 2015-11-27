using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Async.Example.Model
{
    public sealed class Downloader : IDisposable
    {
        private readonly WebClient _webClient;
        private readonly IProgressNotifier _progressNotifier;
        private bool _disposed;

        public Downloader(IProgressNotifier progressNotifier)
        {
            _webClient = new WebClient();
            _progressNotifier = progressNotifier;
        }

        public async Task DownloadPagesAsync(IList<string> pages, CancellationToken token)
        {
            this.GuardDisposed();

            if (pages == null)
            {
                throw new ArgumentNullException("pages");
            }

            var resultContent = new List<string>(pages.Count);

            for (int i = 0; i < pages.Count; ++i)
            {
                token.ThrowIfCancellationRequested();
                var content = await _webClient.DownloadStringTaskAsync(pages[i]);
                resultContent.Add(content);
                _progressNotifier.NotifyProgress(1);
            }
        }

        private void GuardDisposed()
        {
            if (_disposed)
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
