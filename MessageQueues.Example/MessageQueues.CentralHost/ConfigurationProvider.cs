using System;

namespace MessageQueues.CentralHost
{
    public sealed class ConfigurationProvider
    {

        private static readonly Lazy<ConfigurationProvider> _instance = new Lazy<ConfigurationProvider>(() => new ConfigurationProvider());

        private ConfigurationProvider()
        {
        }

        public CentralHostServiceConfiguration Configuration { get; set; }

        public static ConfigurationProvider Instance
        {
            get { return _instance.Value; }
        }
    }
}
