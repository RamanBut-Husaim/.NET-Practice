using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using AppDomains.Example.Plugin.Contracts;

namespace AppDomains.Example.Plugin.Core
{
    public sealed class PluginManager : IPluginManager
    {
        private readonly string _basePath;
        private readonly IDictionary<Type, IList<PluginTokenBase>> _plugins;

        public PluginManager()
        {
            _basePath = AppDomain.CurrentDomain.BaseDirectory;
            _plugins = new Dictionary<Type, IList<PluginTokenBase>>();
        }

        public PluginManager(string basePath)
        {
            _basePath = basePath;
            _plugins = new Dictionary<Type, IList<PluginTokenBase>>();
        }

        public TPlugin Load<TPlugin>() where TPlugin : PluginBase, new()
        {
            TPlugin plugin = this.GetPluginIfExists<TPlugin>();
            if (plugin != null)
            {
                return plugin;
            }

            TPlugin pluginInstance = this.CreatePluginInstance<TPlugin>();

            return pluginInstance;
        }

        private TPlugin GetPluginIfExists<TPlugin>() where TPlugin : PluginBase, new()
        {
            Type pluginType = typeof(TPlugin);

            IList<PluginTokenBase> pluginTokens;
            if (_plugins.TryGetValue(pluginType, out pluginTokens))
            {
                var pluginToken = pluginTokens.First() as PluginToken<TPlugin>;
                if (pluginToken == null)
                {
                    throw new ArgumentNullException("The stored plugin is invalid!");
                }

                return pluginToken.Plugin;
            }

            return null;
        }

        private TPlugin CreatePluginInstance<TPlugin>() where TPlugin : PluginBase, new()
        {
            var appDomianSetup = new AppDomainSetup {ApplicationBase = _basePath};

            var appDomain = AppDomain.CreateDomain(Guid.NewGuid().ToString(), new Evidence(), appDomianSetup);
            var pluginType = typeof(TPlugin);
            var pluginInstance = appDomain.CreateInstanceAndUnwrap(pluginType.Assembly.FullName, pluginType.FullName) as TPlugin;

            IList<PluginTokenBase> pluginTokens;
            var pluginToken = new PluginToken<TPlugin>("1", pluginInstance, appDomain);
            if (!_plugins.TryGetValue(pluginType, out pluginTokens))
            {
                pluginTokens = new List<PluginTokenBase>();
                _plugins.Add(pluginType, pluginTokens);
            }

            pluginTokens.Add(pluginToken);

            return pluginInstance;
        }
    }
}
