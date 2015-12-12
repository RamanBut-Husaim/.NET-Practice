using System;
using Xunit;
using Xunit.Abstractions;

namespace AppDomains.Example.Plugin.Core.Tests
{
    public sealed class PluginManagerTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public PluginManagerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Load_WhenTheTypeIsSpecified_ItIsLoaded()
        {
            var pluginManager = new PluginManager(AppDomain.CurrentDomain.BaseDirectory);

            FakeCalculatorPlugin fakePlugin = pluginManager.Load<FakeCalculatorPlugin>();

            Assert.NotNull(fakePlugin);
        }

        [Fact]
        public void Load_WhenThePluginIsSpecified_IsIsLoadedIntoDifferentApplicationDomain()
        {
            var pluginManager = new PluginManager(AppDomain.CurrentDomain.BaseDirectory);

            FakeCalculatorPlugin fakePlugin = pluginManager.Load<FakeCalculatorPlugin>();

            string currentDomainName = AppDomain.CurrentDomain.FriendlyName;
            _testOutputHelper.WriteLine("Current Domain: {0}", currentDomainName);
            _testOutputHelper.WriteLine("Plugin Domain: {0}", fakePlugin.AppDomainName);
            Assert.NotEqual(currentDomainName, fakePlugin.AppDomainName);
        }

        [Fact]
        public void Load_WhenTheSamePluginIsLoadedTwice_TheSameInstanceIsReturned()
        {
            var pluginManager = new PluginManager(AppDomain.CurrentDomain.BaseDirectory);

            FakeCalculatorPlugin firstLoad = pluginManager.Load<FakeCalculatorPlugin>();
            FakeCalculatorPlugin secondLoad = pluginManager.Load<FakeCalculatorPlugin>();

            Assert.Equal(firstLoad, secondLoad);
        }

        [Fact]
        public void Unload_WhenThePluginHasBeenLoaded_TheUnloadWillBeSuccessfull()
        {
            var pluginManager = new PluginManager(AppDomain.CurrentDomain.BaseDirectory);

            var plugin = pluginManager.Load<FakeCalculatorPlugin>();
            pluginManager.Unload<FakeCalculatorPlugin>();

            Assert.Throws<AppDomainUnloadedException>(() => plugin.IsPrime(3));
        }

        [Fact]
        public void Unload_WhenThePluginHasNotBeenLoaded_TheExceptionOccurs()
        {
            var pluginManager = new PluginManager(AppDomain.CurrentDomain.BaseDirectory);

            Assert.Throws<InvalidOperationException>(() => pluginManager.Unload<FakeCalculatorPlugin>());
        }

        [Fact]
        public void Unload_WhenThePluginUnloadedSecondTime_TheExceptionOccurs()
        {
            var pluginManager = new PluginManager(AppDomain.CurrentDomain.BaseDirectory);

            pluginManager.Load<FakeCalculatorPlugin>();
            pluginManager.Unload<FakeCalculatorPlugin>();

            Assert.Throws<InvalidOperationException>(() => pluginManager.Unload<FakeCalculatorPlugin>());
        }
    }
}
