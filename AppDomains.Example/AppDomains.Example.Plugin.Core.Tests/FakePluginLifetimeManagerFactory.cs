using System.Threading;
using AppDomains.Example.Plugin.Contracts;

namespace AppDomains.Example.Plugin.Core.Tests
{
    public sealed class FakePluginLifetimeManagerFactory : IPluginLifetimeManagerFactory
    {
        private readonly AutoResetEvent _autoResetEvent;

        public FakePluginLifetimeManagerFactory(AutoResetEvent autoResetEvent)
        {
            _autoResetEvent = autoResetEvent;
        }

        public object LastPluginLifetimeManager
        {
            get; private set;
        }

        public IPluginLifetimeManager<TPlugin> Create<TPlugin>(IPluginManager pluginManager) where TPlugin : PluginBase
        {
            var pluginLifetimeManager = new FakePluginLifetimeManager<TPlugin>(pluginManager, _autoResetEvent);

            this.LastPluginLifetimeManager = pluginLifetimeManager;

            return pluginLifetimeManager;
        }
    }
}
