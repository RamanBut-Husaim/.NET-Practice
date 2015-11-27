using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Async.Example.Model;

namespace Async.Example.ViewModel
{
    public sealed class DownloaderViewModel : ObservableObject<DownloaderViewModel>, IDisposable
    {
        private readonly ObservableCollection<string> _urlList;
        private readonly UrlValidator _urlValidator;
        private readonly DownloaderFactory _downloaderFactory;
        private readonly DelegateCommand _addUrlCommand;
        private readonly AsyncCommand _downloadCommand;
        private readonly DelegateCommand _abortCommand;
        private readonly DelegateCommand _clearListCommand;

        private bool _isUrlValid;
        private string _url;
        private OperationResult _operationResult;
        private bool _processing;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _disposed;
        private IProgressNotifier _progressNotifier;
        private double _progressValue;


        public DownloaderViewModel()
        {
            _isUrlValid = true;
            _url = string.Empty;
            _processing = false;
            _urlList = new ObservableCollection<string>();
            _urlValidator = new UrlValidator();
            _downloaderFactory = new DownloaderFactory();
            _downloadCommand = new AsyncCommand(this.DownloadPages);
            _addUrlCommand = new DelegateCommand(this.AddUrl);
            _abortCommand = new DelegateCommand(this.AbortDownload);
            _clearListCommand = new DelegateCommand(_urlList.Clear);
        }

        public IEnumerable<string> UrlList
        {
            get { return _urlList; }
        }

        public bool IsUrlValid
        {
            get { return _isUrlValid; }
            set
            {
                _isUrlValid = value;
                this.RaisePropertyChangedEvent(p => p.IsUrlValid);
            }
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

        public string Url
        {
            get { return _url;}
            set
            {
                _url = value;
                this.RaisePropertyChangedEvent(p => p.Url);
            }
        }

        public double Progress
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                this.RaisePropertyChangedEvent(p => p.Progress);
            }
        }

        public bool NotProcessing
        {
            get { return !this.Processing; }
        }

        public bool Processing
        {
            get { return _processing;}
            set
            {
                _processing = value;
                this.RaisePropertyChangedEvent(p => p.Processing);
                this.RaisePropertyChangedEvent(p => p.NotProcessing);
            }
        }

        public ICommand AddUrlCommand
        {
            get { return _addUrlCommand; }
        }

        public ICommand DownloadPagesCommand
        {
            get { return _downloadCommand; }
        }

        public ICommand AbortCommand
        {
            get { return _abortCommand; }
        }

        public ICommand ClearUrlListCommand
        {
            get { return _clearListCommand; }
        }

        private void AddUrl()
        {
            this.ValidateUrl();
            if (this.IsUrlValid)
            {
                _urlList.Add(this.Url);
                this.Url = string.Empty;
            }
        }

        private void ValidateUrl()
        {
            if (string.IsNullOrEmpty(this.Url))
            {
                this.IsUrlValid = false;
                return;
            }

            this.IsUrlValid = _urlValidator.IsUrlValid(this.Url);
        }

        private void AbortDownload()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        private async Task DownloadPages()
        {
            this.Processing = true;
            this.Progress = 0;
            Downloader downloader = _downloaderFactory.Create(new ProgressNotifier((value) => this.Progress += value, _urlList.Count));
            try
            {
                _cancellationTokenSource = new CancellationTokenSource();
                await downloader.DownloadPagesAsync(_urlList, _cancellationTokenSource.Token);
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
                this.CleanUpDownloader(downloader);
                this.CleanUpCancellationTokenSource();

                this.Processing = false;
            }
        }

        private void CleanUpDownloader(Downloader downloader)
        {
            if (downloader != null)
            {
                downloader.Dispose();
            }
        }

        private void CleanUpCancellationTokenSource()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Dispose();
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
                    this.CleanUpCancellationTokenSource();
                }

                _disposed = true;
            }
        }
    }
}
