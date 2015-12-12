using System;

namespace AppDomains.Example.Plugin.Contracts
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PluginVersionAttribute : Attribute
    {
        public PluginVersionAttribute(string version, Type pluginType)
        {
            this.Version = version;
            this.PluginType = pluginType;
        }

        public string Version { get; private set; }

        public Type PluginType { get; private set; }
    }
}
