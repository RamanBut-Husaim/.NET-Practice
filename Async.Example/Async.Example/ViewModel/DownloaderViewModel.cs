using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Async.Example.Model;

namespace Async.Example.ViewModel
{
    public sealed class DownloaderViewModel : ObservableObject<DownloaderViewModel>
    {
        private readonly ObservableCollection<string> _urlList;
        private readonly UrlValidator _urlValidator;

        private bool _isUrlValid;
        private string _url;


        public DownloaderViewModel()
        {
            _isUrlValid = true;
            _url = string.Empty;
            _urlList = new ObservableCollection<string>();
            _urlValidator = new UrlValidator();
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
            get { return new DelegateCommand(this.AddUrl); }
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
    }
}
