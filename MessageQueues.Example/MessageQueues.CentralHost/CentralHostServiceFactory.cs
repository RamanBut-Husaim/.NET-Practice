using System;

namespace MessageQueues.CentralHost
{
    public sealed class CentralHostServiceFactory
    {
        private readonly Func<CentralHostServiceConfiguration, CentralHostService> _factory;

        public CentralHostServiceFactory(Func<CentralHostServiceConfiguration, CentralHostService> factory)
        {
            _factory = factory;
        }

        public CentralHostService Create(CentralHostServiceConfiguration configuration)
        {
            return _factory.Invoke(configuration);
        }
    }
}
