using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Async.Example.ViewModel
{
    public sealed class AsyncCommand : ICommand
    {
        private readonly Func<Task> _func;
        private bool _isExecuting;

        public AsyncCommand(Func<Task> func)
        {
            _isExecuting = false;
            _func = func;
        }

        public bool CanExecute(object parameter)
        {
            return !_isExecuting;
        }

        public async void Execute(object parameter)
        {
            try
            {
                _isExecuting = true;
                this.OnCanExecuteChanged();
                await _func();
            }
            finally
            {
                _isExecuting = false;
                this.OnCanExecuteChanged();
            }
        }

        public event EventHandler CanExecuteChanged;

        private void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
