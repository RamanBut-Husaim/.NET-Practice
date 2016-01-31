using System;

namespace MessageQueues.HarvesterHost
{
    public sealed class ConfigurationProvider
    {
        private static readonly Lazy<ConfigurationProvider> _instance = new Lazy<ConfigurationProvider>(() => new ConfigurationProvider());

        private ConfigurationProvider()
        {
        }

        public ServiceConfiguration Configuration { get; set; }

        public static ConfigurationProvider Instance
        {
            get { return _instance.Value; }
        }
    }
}
