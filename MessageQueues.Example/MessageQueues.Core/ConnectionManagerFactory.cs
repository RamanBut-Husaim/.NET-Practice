using System;

namespace MessageQueues.Core
{
    public sealed class ConnectionManagerFactory
    {
        public IConnectionManager Create()
        {
            return new ConnectionManager();
        }

        public void Release(IConnectionManager connectionManager)
        {
            var disposable = connectionManager as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
