using System;

namespace MessageQueues.CentralHost.Core
{
    public sealed class FileMessageDispatcherFactory
    {
        private readonly Func<IFileMessageDispatcher> _factory;

        public FileMessageDispatcherFactory(Func<IFileMessageDispatcher> factory)
        {
            _factory = factory;
        }

        public IFileMessageDispatcher Create()
        {
            return _factory.Invoke();
        }

        public void Release(IFileMessageDispatcher dispatcher)
        {
            var disposable = dispatcher as IDisposable;

            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
