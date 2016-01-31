using System;

namespace MessageQueues.CentralHost
{
    public sealed class CentralHostServiceFactory
    {
        private readonly Func<ServiceConfiguration, CentralHostService> _factory;

        public CentralHostServiceFactory(Func<ServiceConfiguration, CentralHostService> factory)
        {
            _factory = factory;
        }

        public CentralHostService Create(ServiceConfiguration configuration)
        {
            return _factory.Invoke(configuration);
        }
    }
}
