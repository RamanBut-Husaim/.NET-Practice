using System;
using System.Windows;

namespace Async.Example
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IDisposable _downloaderViewModel;
    }
}
