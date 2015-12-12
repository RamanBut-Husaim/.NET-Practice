using System.Runtime.Remoting.Lifetime;
using AppDomains.Example.Plugin.Contracts;

namespace AppDomains.Example.Plugin.Core
{
    public interface IPluginLifetimeManager<TPlugin> : ISponsor where TPlugin: PluginBase
    {
        IPluginManager PluginManager { get; }
    }
}
