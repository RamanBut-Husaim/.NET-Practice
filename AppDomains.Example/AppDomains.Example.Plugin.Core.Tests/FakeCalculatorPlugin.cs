using System;
using AppDomains.Example.Plugin.Contracts;

namespace AppDomains.Example.Plugin.Core.Tests
{
    [PluginVersion("1.0.0.0", typeof(ICalculatorPlugin))]
    public sealed class FakeCalculatorPlugin : PluginBase, ICalculatorPlugin
    {
        private readonly string _appDomainName;

        public FakeCalculatorPlugin(TimeSpan initialLifeTime, TimeSpan renewalOnCallTime) : base(initialLifeTime, renewalOnCallTime)
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

    [PluginVersion("1.0.0.1", typeof(ICalculatorPlugin))]
    public sealed class FakeAnotherCalculatorPlugin : PluginBase, ICalculatorPlugin
    {
        public FakeAnotherCalculatorPlugin(TimeSpan initialLifeTime, TimeSpan renewalOnCallTime) : base(initialLifeTime, renewalOnCallTime)
        {
        }

        public bool IsPrime(int number)
        {
            return true;
        }
    }
}
