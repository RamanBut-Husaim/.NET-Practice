using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Async.Example.Model
{
    public sealed class Downloader : IDisposable
    {
        private readonly HttpClient _httpClient;
        private bool _disposed;

        public Downloader()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> DownloadPagesAsync(string page, CancellationToken token)
        {
            this.GuardDisposed();

            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            await Task.Delay(TimeSpan.FromSeconds(5), token);

            HttpResponseMessage response = await _httpClient.GetAsync(page, token);
            string content = await response.Content.ReadAsStringAsync();

            return content;
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
                    _httpClient.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
