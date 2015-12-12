using AppDomains.Example.Plugin.Contracts;

namespace AppDomains.Example.Plugin.Core
{
    public interface IPluginLifetimeManagerFactory
    {
        IPluginLifetimeManager<TPlugin> Create<TPlugin>(IPluginManager pluginManager) where TPlugin : PluginBase;
    }
}
