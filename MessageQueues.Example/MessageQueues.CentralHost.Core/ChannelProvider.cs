using System;

using MessageQueues.Core;

using RabbitMQ.Client;

namespace MessageQueues.CentralHost.Core
{
    public sealed class ChannelProvider : IChannelProvider, IDisposable
    {
        private readonly ConnectionManagerFactory _connectionManagerFactory;
        private readonly IConnectionManager _connectionManager;
        private readonly IModel _channel;

        public ChannelProvider(ConnectionManagerFactory connectionManagerFactory)
        {
            _connectionManagerFactory = connectionManagerFactory;
            _connectionManager = _connectionManagerFactory.Create();
            _channel = _connectionManager.GetChannel();
        }

        private bool _disposed;

        public IModel GetChannel()
        {
            this.GuardDisposed();

            return _channel;
        }

        private void GuardDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("ChannelProvider");
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
                    _channel.Dispose();
                    _connectionManagerFactory.Release(_connectionManager);
                }

                _disposed = true;
            }
        }
    }
}
