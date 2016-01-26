using System;

namespace MessageQueues.HarvesterHost.Core.Services
{
    public sealed class SynchronizationServiceFactory
    {
        private readonly Func<string, string, ISynchronizationService> _factory;

        public SynchronizationServiceFactory(Func<string, string, ISynchronizationService> factory)
        {
            _factory = factory;
        }

        public ISynchronizationService Create(string sourcePath, string destinationPath)
        {
            return _factory.Invoke(sourcePath, destinationPath);
        }
    }
}
