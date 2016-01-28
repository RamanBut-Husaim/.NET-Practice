using System;
using RabbitMQ.Client;

namespace MessageQueues.Core
{
    internal sealed class ConnectionManager : IConnectionManager, IDisposable
    {
        private readonly object _sync = new object();

        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private bool _disposed;

        public ConnectionManager()
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            _connection = null;
        }

        private IConnection Connection
        {
            get
            {
                lock (_sync)
                {
                    if (_connection == null)
                    {
                        _connection = _connectionFactory.CreateConnection();
                    }
                }

                return _connection;
            }
        }

        public IModel GetChannel()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("ConnectionManager");
            }

            IModel channel = this.Connection.CreateModel();

            return channel;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            lock (_sync)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        if (_connection != null)
                        {
                            _connection.Dispose();
                        }
                    }

                    _disposed = true;
                }
            }

        }
    }
}
