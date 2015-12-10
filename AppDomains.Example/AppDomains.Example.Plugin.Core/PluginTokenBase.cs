using System;

namespace AppDomains.Example.Plugin.Core
{
    public abstract class PluginTokenBase
    {
        private readonly string _version;
        private readonly AppDomain _appDomain;

        protected PluginTokenBase(string version, AppDomain appDomain)
        {
            _version = version;
            _appDomain = appDomain;
        }

        public string Version
        {
            get { return _version; }
        }

        public AppDomain AppDomain
        {
            get { return _appDomain; }
        }
    }
}
