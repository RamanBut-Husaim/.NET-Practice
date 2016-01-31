using System;

namespace MessageQueues.CentralHost.Core
{
    public sealed class ChannelProviderFactory
    {
        private readonly Func<IChannelProvider> _factory;

        public ChannelProviderFactory(Func<IChannelProvider> factory)
        {
            _factory = factory;
        }

        public IChannelProvider Create()
        {
            return _factory.Invoke();
        }

        public void Release(IChannelProvider channelProvider)
        {
            var disposable = channelProvider as IDisposable;

            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
