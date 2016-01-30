using System;

namespace MessageQueues.CentralHost.Core
{
    public sealed class HarvesterProcessingDispatcherFactory
    {
        private readonly Func<string, IHarvesterProcessingDispatcher> _factory;

        public HarvesterProcessingDispatcherFactory(Func<string, IHarvesterProcessingDispatcher> factory)
        {
            _factory = factory;
        }

        public IHarvesterProcessingDispatcher Create(string harvesterName)
        {
            return _factory.Invoke(harvesterName);
        }

        public void Release(IHarvesterProcessingDispatcher harvesterProcessingDispatcher)
        {
            var disposable = harvesterProcessingDispatcher as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
