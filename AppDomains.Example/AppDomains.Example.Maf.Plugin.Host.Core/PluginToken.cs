using System;
using System.AddIn.Hosting;
using AppDomains.Example.Maf.Plugin.HostViewAddIn;

namespace AppDomains.Example.Maf.Plugin.Host.Core
{
    public sealed class PluginToken<TPlugin> : PluginTokenBase where TPlugin: IHostViewAddIn
    {
        private readonly Lazy<TPlugin> _plugin;

        public PluginToken(AddInToken addInToken) : base(addInToken)
        {
            _plugin = new Lazy<TPlugin>(() => addInToken.Activate<TPlugin>(AddInSecurityLevel.Internet));
        }

        public TPlugin Plugin
        {
            get { return _plugin.Value; }
        }
    }
}
