using System;
using System.Runtime.Remoting.Lifetime;
using System.Threading;
using AppDomains.Example.Plugin.Contracts;

namespace AppDomains.Example.Plugin.Core.Tests
{
    public sealed class FakePluginLifetimeManager<TPlugin> : MarshalByRefObject, IPluginLifetimeManager<TPlugin> where TPlugin: PluginBase
    {
        private readonly IPluginManager _pluginManager;
        private readonly AutoResetEvent _autoResetEvent;
        private bool _unloaded;

        public FakePluginLifetimeManager(IPluginManager pluginManager, AutoResetEvent autoResetEvent)
        {
            _pluginManager = pluginManager;
            _autoResetEvent = autoResetEvent;
        }

        public IPluginManager PluginManager
        {
            get { return _pluginManager; }
        }

        public bool Unloaded
        {
            get { return _unloaded; }
        }

        public TimeSpan Renewal(ILease lease)
        {
            _unloaded = true;
            _autoResetEvent.Set();

            return TimeSpan.FromSeconds(0);
        }
    }
}
