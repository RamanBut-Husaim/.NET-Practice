using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Async.Example.Model;

namespace Async.Example.ViewModel
{
    public sealed class DownloaderItemModel : ObservableObject<DownloaderItemModel>, IDisposable
    {
        private readonly string _url;
        private readonly DownloaderFactory _downloaderFactory;
        private readonly HashProviderFactory _hashProviderFactory;
        private readonly AsyncCommand _downloadCommand;
        private readonly DelegateCommand _abortCommand;

        private bool _disposed;
        private OperationResult _operationResult;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _operationInProgress;
        private string _pageHash;

        public DownloaderItemModel(string url, DownloaderFactory downloaderFactory, HashProviderFactory hashProviderFactory)
        {
            _url = url;
            _downloaderFactory = downloaderFactory;
            _hashProviderFactory = hashProviderFactory;

            _downloadCommand = new AsyncCommand(this.DownloadWebContent);
            _abortCommand = new DelegateCommand(this.AbortDownload);
        }

        public string Url
        {
            get { return _url; }
        }

        public OperationResult OperationResult
        {
            get { return _operationResult; }
            set
            {
                _operationResult = value;
                this.RaisePropertyChangedEvent(p => p.OperationResult);
            }
        }

        public bool OperationInProgress
        {
            get { return _operationInProgress; }
            set
            {
                _operationInProgress = value;
                this.RaisePropertyChangedEvent(p => p.OperationInProgress);
                this.RaisePropertyChangedEvent(p => p.NotOperationInProgress);
            }
        }

        public string PageHash
        {
            get { return _pageHash; }
            set
            {
                _pageHash = value;
                this.RaisePropertyChangedEvent(p => p.PageHash);
            }
        }

        public bool NotOperationInProgress
        {
            get { return !_operationInProgress; }
        }

        public ICommand DownloadCommand
        {
            get { return _downloadCommand; }
        }

        public ICommand AbortCommand
        {
            get { return _abortCommand; }
        }

        private void AbortDownload()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        private async Task DownloadWebContent()
        {
            this.OperationInProgress = true;

            Downloader downloader = _downloaderFactory.Create();
            HashProvider hashProvider = _hashProviderFactory.Create();

            try
            {
                _cancellationTokenSource = new CancellationTokenSource();
                string pageContent = await downloader.DownloadPagesAsync(_url, _cancellationTokenSource.Token);
                this.PageHash = hashProvider.ComputeHash(pageContent ?? string.Empty);
                this.OperationResult = OperationResult.Success;
            }
            catch (OperationCanceledException ex)
            {
                this.OperationResult = OperationResult.Aborted;
            }
            catch (Exception ex)
            {
                this.OperationResult = OperationResult.Failed;
            }
            finally
            {
                downloader.Dispose();
                _cancellationTokenSource.Dispose();
                hashProvider.Dispose();

                this.OperationInProgress = false;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _cancellationTokenSource.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
