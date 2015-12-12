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
            TPlugin plugin = this.GetPlugin<TPlugin>();
            if (plugin != null)
            {
                return plugin;
            }

            TPlugin pluginInstance = this.CreatePluginInstance<TPlugin>();

            return pluginInstance;
        }

        public bool IsLoaded<TPlugin>() where TPlugin : PluginBase
        {
            TPlugin plugin = this.GetPlugin<TPlugin>();

            return plugin != null;
        }

        public void Unload<TPlugin>() where TPlugin : PluginBase
        {
            if (!this.IsLoaded<TPlugin>())
            {
                throw new InvalidOperationException("The plugin has already been unloaded.");
            }

            PluginToken<TPlugin> pluginToken = this.GetPluginToken<TPlugin>();
            this.UnloadInternal(pluginToken);
        }

        private void UnloadInternal<TPlugin>(PluginToken<TPlugin> plugin) where TPlugin : PluginBase
        {
            if (plugin != null)
            {
                var pluginVersionAndType = this.GetPluginMetaInfo<TPlugin>();
                AppDomain.Unload(plugin.AppDomain);
                _plugins[pluginVersionAndType.Item2].Remove(plugin);
            }
        }

        private PluginToken<TPlugin> GetPluginToken<TPlugin>() where TPlugin : PluginBase
        {
            var pluginVersionAndType = this.GetPluginMetaInfo<TPlugin>();

            IList<PluginTokenBase> pluginTokens;
            if (_plugins.TryGetValue(pluginVersionAndType.Item2, out pluginTokens))
            {
                var pluginToken = pluginTokens.FirstOrDefault(p => p.Version == pluginVersionAndType.Item1) as PluginToken<TPlugin>;

                return pluginToken;
            }

            return null;
        }

        private TPlugin GetPlugin<TPlugin>() where TPlugin : PluginBase
        {
            var pluginToken = this.GetPluginToken<TPlugin>();

            if (pluginToken != null)
            {
                return pluginToken.Plugin;
            }

            return null;
        }

        private TPlugin CreatePluginInstance<TPlugin>() where TPlugin : PluginBase
        {
            var pluginVersionAndType = this.GetPluginMetaInfo<TPlugin>();

            this.GuardPluginVersion(pluginVersionAndType.Item2, pluginVersionAndType.Item1);

            PluginToken<TPlugin> pluginToken = this.CreatePluginToken<TPlugin>(pluginVersionAndType.Item1);

            IList<PluginTokenBase> pluginTokens;

            if (!_plugins.TryGetValue(pluginVersionAndType.Item2, out pluginTokens))
            {
                pluginTokens = new List<PluginTokenBase>();
                _plugins.Add(pluginVersionAndType.Item2, pluginTokens);
            }

            pluginTokens.Add(pluginToken);

            return pluginToken.Plugin;
        }

        private PluginToken<TPlugin> CreatePluginToken<TPlugin>(string pluginVersion) where TPlugin : PluginBase
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
                new object[] { _initialLifeTime, _renewalOnCallTime },
                null,
                null
                ) as TPlugin;


            var pluginToken = new PluginToken<TPlugin>(pluginVersion, pluginInstance, appDomain, _pluginLifetimeManagerFactory.Create<TPlugin>(this));

            return pluginToken;
        }

        private Tuple<string, Type> GetPluginMetaInfo<TPlugin>() where TPlugin : PluginBase
        {
            var pluginVersion = this.GetVersionName<TPlugin>();
            this.GuardVersion(pluginVersion);

            var pluginApplicationType = this.GetPluginType<TPlugin>();
            this.GuardPluginType(pluginApplicationType);

            return new Tuple<string, Type>(pluginVersion, pluginApplicationType);
        }

        private string GetVersionName<TPlugin>() where TPlugin : PluginBase
        {
            var pluginType = typeof (TPlugin);

            var versionAttribute = pluginType.GetCustomAttribute<PluginVersionAttribute>();

            return versionAttribute.Version;
        }

        private Type GetPluginType<TPlugin>() where TPlugin : PluginBase
        {
            var pluginType = typeof(TPlugin);

            var versionAttribute = pluginType.GetCustomAttribute<PluginVersionAttribute>();

            return versionAttribute.PluginType;
        }

        private void GuardVersion(string version)
        {
            if (string.IsNullOrEmpty(version))
            {
                throw new InvalidOperationException("The version could not be empty.");
            }
        }

        private void GuardPluginType(Type pluginType)
        {
            if (!typeof (IPlugin).IsAssignableFrom(pluginType))
            {
                throw new InvalidOperationException("The plugin type should implement IPlugin.");
            }
        }

        private void GuardPluginVersion(Type pluginType, string version)
        {
            IList<PluginTokenBase> pluginTokens;

            if (_plugins.TryGetValue(pluginType, out pluginTokens))
            {
                if (pluginTokens.Select(p => p.Version).Contains(version))
                {
                    throw new InvalidOperationException("The plugin with the same version has already been loaded.");
                }
            }
        }
    }
}
