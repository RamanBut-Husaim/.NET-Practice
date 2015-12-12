using AppDomains.Example.Plugin.Contracts;

namespace AppDomains.Example.Plugin.Core
{
    public sealed class PluginLifetimeManagerFactory : IPluginLifetimeManagerFactory
    {
        public IPluginLifetimeManager<TPlugin> Create<TPlugin>(IPluginManager pluginManager) where TPlugin : PluginBase
        {
            return new PluginLifetimeManager<TPlugin>(pluginManager);
        }
    }
}
