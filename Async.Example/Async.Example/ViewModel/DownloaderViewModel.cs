using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Async.Example.Model;

namespace Async.Example.ViewModel
{
    public sealed class DownloaderViewModel : ObservableObject<DownloaderViewModel>
    {
        private readonly ObservableCollection<string> _urlList;
        private readonly UrlValidator _urlValidator;
        private readonly DownloaderFactory _downloaderFactory;
        private readonly DelegateCommand _addUrlCommand;
        private readonly AsyncCommand _downloadCommand;

        private bool _isUrlValid;
        private string _url;
        private OperationResult _operationResult;
        private bool _processing;

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

        private void AddUrl()
        {
            this.ValidateUrl();
            if (this.IsUrlValid)
            {
                _urlList.Add(this.Url);
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

        private async Task DownloadPages()
        {
            this.Processing = true;
            Downloader downloader = _downloaderFactory.Create();
            try
            {
                await downloader.DownloadPagesAsync(_urlList);
                this.OperationResult = OperationResult.Success;
            }
            catch (Exception ex)
            {
                this.OperationResult = OperationResult.Failed;
            }
            finally
            {
                if (downloader != null)
                {
                    downloader.Dispose();
                }

                this.Processing = false;
            }
        }
    }
}
