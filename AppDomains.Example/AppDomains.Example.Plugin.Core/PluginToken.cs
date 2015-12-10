using System;
using AppDomains.Example.Plugin.Contracts;

namespace AppDomains.Example.Plugin.Core
{
    internal sealed class PluginToken<TPlugin> : PluginTokenBase where TPlugin: PluginBase
    {
        private readonly TPlugin _plugin;

        public PluginToken(string version, TPlugin plugin, AppDomain appDomain) : base(version, appDomain)
        {
            _plugin = plugin;
        }

        public TPlugin Plugin
        {
            get { return _plugin; }
        }
    }
}
