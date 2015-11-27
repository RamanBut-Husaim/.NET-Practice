using System;

namespace Async.Example.View
{
    /// <summary>
    /// Interaction logic for DownloaderControl.xaml
    /// </summary>
    public partial class DownloaderControl
    {
        public DownloaderControl()
        {
            this.InitializeComponent();
            this.Dispatcher.ShutdownStarted += this.OnDispatcherShutDownStarted;
        }

        private void OnDispatcherShutDownStarted(object sender, EventArgs e)
        {
            var disposable = this.DataContext as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
