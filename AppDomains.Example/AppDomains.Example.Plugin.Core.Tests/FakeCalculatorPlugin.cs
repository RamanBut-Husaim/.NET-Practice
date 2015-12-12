using System;
using AppDomains.Example.Plugin.Contracts;

namespace AppDomains.Example.Plugin.Core.Tests
{
    public sealed class FakeCalculatorPlugin : PluginBase, ICalculatorPlugin
    {
        private readonly string _appDomainName;

        public FakeCalculatorPlugin()
        {
            _appDomainName = AppDomain.CurrentDomain.FriendlyName;
        }

        public string AppDomainName
        {
            get { return _appDomainName; }
        }

        public bool IsPrime(int number)
        {
            return true;
        }
    }
}
