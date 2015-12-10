using System;
using System.Collections.Generic;

namespace AppDomains.Example.Plugin.Core
{
    public sealed class PluginManager : IPluginManager
    {
        private readonly IDictionary<Type, IList<PluginTokenBase>> _plugins;

        public PluginManager()
        {
            _plugins = new Dictionary<Type, IList<PluginTokenBase>>();
        }


    }
}
