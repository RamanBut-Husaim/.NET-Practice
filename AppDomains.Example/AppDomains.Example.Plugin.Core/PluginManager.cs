using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using AppDomains.Example.Plugin.Contracts;

namespace AppDomains.Example.Plugin.Core
{
    public sealed class PluginManager : IPluginManager
    {
        private readonly string _basePath;
        private readonly TimeSpan _initialLifeTime;
        private readonly TimeSpan _renewalOnCallTime;
        private readonly IDictionary<Type, IList<PluginTokenBase>> _plugins;
        private readonly IPluginLifetimeManagerFactory _pluginLifetimeManagerFactory;

        public PluginManager() : this(AppDomain.CurrentDomain.BaseDirectory)
        {
        }

        public PluginManager(IPluginLifetimeManagerFactory pluginLifetimeManagerFactory) : this(AppDomain.CurrentDomain.BaseDirectory, pluginLifetimeManagerFactory)
        {

        }

        public PluginManager(string basePath) : this(basePath, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(2), null)
        {
        }

        public PluginManager(string basePath, IPluginLifetimeManagerFactory pluginLifetimeManagerFactory) : this(basePath, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(2), pluginLifetimeManagerFactory)
        {
        }

        public PluginManager(TimeSpan initialLifeTime, TimeSpan renewalOnCallTime) : this(AppDomain.CurrentDomain.BaseDirectory, initialLifeTime, renewalOnCallTime, null)
        {
        }

        public PluginManager(TimeSpan initialLifeTime, TimeSpan renewalOnCallTime, IPluginLifetimeManagerFactory lifetimeManagerFactory) : this(AppDomain.CurrentDomain.BaseDirectory, initialLifeTime, renewalOnCallTime, lifetimeManagerFactory)
        {
        }

        public PluginManager(string basePath, TimeSpan initialLifeTime, TimeSpan renewalOnCallTime, IPluginLifetimeManagerFactory pluginLifetimeManagerFactory)
        {
            _pluginLifetimeManagerFactory = pluginLifetimeManagerFactory ?? new PluginLifetimeManagerFactory();
            _basePath = basePath;
            _initialLifeTime = initialLifeTime;
            _renewalOnCallTime = renewalOnCallTime;
            _plugins = new Dictionary<Type, IList<PluginTokenBase>>();
        }

        public TPlugin Load<TPlugin>() where TPlugin : PluginBase
        {
            TPlugin plugin = this.GetPluginIfExists<TPlugin>();
            if (plugin != null)
            {
                return plugin;
            }

            TPlugin pluginInstance = this.CreatePluginInstance<TPlugin>();

            return pluginInstance;
        }

        public bool IsLoaded<TPlugin>() where TPlugin : PluginBase
        {
            TPlugin plugin = this.GetPluginIfExists<TPlugin>();

            return plugin != null;
        }

        public void Unload<TPlugin>() where TPlugin : PluginBase
        {
            if (!this.IsLoaded<TPlugin>())
            {
                throw new InvalidOperationException("The plugin has already been unloaded.");
            }

            this.UnloadPlugins<TPlugin>();
        }

        private void UnloadPlugins<TPlugin>() where TPlugin : PluginBase
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

        private void UnloadInternal<TPlugin>(PluginToken<TPlugin> plugin) where TPlugin : PluginBase
        {
            if (plugin != null)
            {
                AppDomain.Unload(plugin.AppDomain);
            }
        }

        private TPlugin GetPluginIfExists<TPlugin>() where TPlugin : PluginBase
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

        private TPlugin CreatePluginInstance<TPlugin>() where TPlugin : PluginBase
        {
            var appDomainSetup = new AppDomainSetup
            {
                ApplicationBase = _basePath
            };

            var appDomain = AppDomain.CreateDomain(Guid.NewGuid().ToString(), new Evidence(), appDomainSetup);
            var pluginType = typeof(TPlugin);
            var pluginInstance = appDomain.CreateInstanceAndUnwrap(
                pluginType.Assembly.FullName,
                pluginType.FullName,
                true,
                BindingFlags.Default,
                null,
                new object[] {_initialLifeTime, _renewalOnCallTime},
                null,
                null
                ) as TPlugin;

            IList<PluginTokenBase> pluginTokens;
            var pluginToken = new PluginToken<TPlugin>("1", pluginInstance, appDomain, _pluginLifetimeManagerFactory.Create<TPlugin>(this));
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
