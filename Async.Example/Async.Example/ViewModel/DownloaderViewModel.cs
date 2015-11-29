using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Async.Example.Model;

namespace Async.Example.ViewModel
{
    public sealed class DownloaderViewModel : ObservableObject<DownloaderViewModel>, IDisposable
    {
        private readonly ObservableCollection<DownloaderItemModel> _downloaderItemModels;

        private readonly UrlValidator _urlValidator;
        private readonly DownloadItemFactory _downloadItemFactory;
        private readonly DelegateCommand _addUrlCommand;
        private readonly DelegateCommand _clearListCommand;

        private bool _isUrlValid;
        private string _url;
        private bool _disposed;

        public DownloaderViewModel()
        {
            _isUrlValid = true;
            _url = string.Empty;
            _downloaderItemModels = new ObservableCollection<DownloaderItemModel>();

            _urlValidator = new UrlValidator();
            _downloadItemFactory = new DownloadItemFactory();
            _addUrlCommand = new DelegateCommand(this.AddUrl);
            _clearListCommand = new DelegateCommand(this.CleanUpItems);
        }

        public IEnumerable<DownloaderItemModel> DownloaderItems
        {
            get {  return _downloaderItemModels; }
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

        public string Url
        {
            get { return _url;}
            set
            {
                _url = value;
                this.RaisePropertyChangedEvent(p => p.Url);
            }
        }

        public ICommand AddUrlCommand
        {
            get { return _addUrlCommand; }
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
                _downloaderItemModels.Add(_downloadItemFactory.Create(this.Url));
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
                    this.CleanUpItems();
                }

                _disposed = true;
            }
        }

        private void CleanUpItems()
        {
            var items = _downloaderItemModels.ToList();
            _downloaderItemModels.Clear();
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] != null)
                {
                    items[i].Dispose();
                }
            }
        }
    }
}
