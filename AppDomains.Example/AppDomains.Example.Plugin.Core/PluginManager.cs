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

        public void Unload<TPlugin>() where TPlugin : PluginBase, new()
        {
            TPlugin plugin = this.GetPluginIfExists<TPlugin>();
            if (plugin == null)
            {
                throw new InvalidOperationException("The plugin has already been unloaded.");
            }

            this.UnloadPlugins<TPlugin>();
        }

        private void UnloadPlugins<TPlugin>() where TPlugin : PluginBase, new()
        {
            var pluginType = typeof(TPlugin);

            IList<Exception> exceptions = new List<Exception>();
            IList<PluginTokenBase> remainingPlugins = new List<PluginTokenBase>();
            IList<PluginTokenBase> pluginTokens = _plugins[pluginType].ToList();

            foreach (var pluginTokenBase in pluginTokens)
            {
                try
                {
                    this.UnloadInternal(pluginTokenBase as PluginToken<TPlugin>);
                }
                catch (CannotUnloadAppDomainException ex)
                {
                    exceptions.Add(ex);
                    remainingPlugins.Add(pluginTokenBase);
                }
            }

            _plugins[pluginType] = remainingPlugins;

            if (exceptions.Count > 0)
            {
                throw new AggregateException("Some versions of the plugin has not been unloaded.", exceptions);
            }
        }

        private void UnloadInternal<TPlugin>(PluginToken<TPlugin> plugin) where TPlugin : PluginBase, new()
        {
            if (plugin != null)
            {
                AppDomain.Unload(plugin.AppDomain);
            }
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
            var appDomainSetup = new AppDomainSetup {ApplicationBase = _basePath};

            var appDomain = AppDomain.CreateDomain(Guid.NewGuid().ToString(), new Evidence(), appDomainSetup);
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
