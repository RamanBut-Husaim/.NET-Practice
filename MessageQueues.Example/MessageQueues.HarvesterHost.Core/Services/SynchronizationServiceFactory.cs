using System;

namespace MessageQueues.HarvesterHost.Core.Services
{
    public sealed class SynchronizationServiceFactory
    {
        private readonly Func<string, ISynchronizationService> _factory;

        public SynchronizationServiceFactory(Func<string, ISynchronizationService> factory)
        {
            _factory = factory;
        }

        public ISynchronizationService Create(string sourcePath)
        {
            return _factory.Invoke(sourcePath);
        }
    }
}
