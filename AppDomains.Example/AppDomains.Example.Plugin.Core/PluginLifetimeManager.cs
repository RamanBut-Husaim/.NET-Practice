using System;
using System.Runtime.Remoting.Lifetime;
using AppDomains.Example.Plugin.Contracts;

namespace AppDomains.Example.Plugin.Core
{
    public sealed class PluginLifetimeManager<TPlugin> : MarshalByRefObject, IPluginLifetimeManager<TPlugin> where TPlugin: PluginBase
    {
        private readonly IPluginManager _pluginManager;

        public PluginLifetimeManager(IPluginManager pluginManager)
        {
            _pluginManager = pluginManager;
        }

        public IPluginManager PluginManager
        {
            get { return _pluginManager; }
        }

        public TimeSpan Renewal(ILease lease)
        {
            _pluginManager.Unload<TPlugin>();

            return TimeSpan.FromSeconds(0);
        }
    }
}
