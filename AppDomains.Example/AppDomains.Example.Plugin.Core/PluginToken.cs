using System;
using System.Runtime.Remoting.Lifetime;
using AppDomains.Example.Plugin.Contracts;

namespace AppDomains.Example.Plugin.Core
{
    internal sealed class PluginToken<TPlugin> : PluginTokenBase where TPlugin: PluginBase
    {
        private readonly TPlugin _plugin;
        private readonly IPluginLifetimeManager<TPlugin> _lifetimeManager;

        public PluginToken(string version, TPlugin plugin, AppDomain appDomain) : base(version, appDomain)
        {
            _plugin = plugin;
            _lifetimeManager = null;
        }

        public PluginToken(
            string version,
            TPlugin plugin,
            AppDomain appDomain,
            IPluginLifetimeManager<TPlugin> lifetimeManager) : this(version, plugin, appDomain)
        {
            _lifetimeManager = lifetimeManager;
            var lease = _plugin.GetLifetimeService() as ILease;
            lease.Register(lifetimeManager);
        }

        public TPlugin Plugin
        {
            get { return _plugin; }
        }

        public IPluginLifetimeManager<TPlugin> LifetimeManager
        {
            get { return _lifetimeManager; }
        }
    }
}
