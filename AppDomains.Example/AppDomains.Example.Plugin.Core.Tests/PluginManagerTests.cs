using System;
using System.Threading;
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

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        public void Load_WhenTheLifetimeIsExpired_ThePluginIsUnloaded(int waitMilliseconds)
        {
            using (var autoResetEvent = new AutoResetEvent(false))
            {
                var fakePluginLifetimeManagerFactory = new FakePluginLifetimeManagerFactory(autoResetEvent);

                var pluginManager = new PluginManager(
                    AppDomain.CurrentDomain.BaseDirectory,
                    TimeSpan.FromMilliseconds(waitMilliseconds),
                    TimeSpan.FromMilliseconds(waitMilliseconds),
                    fakePluginLifetimeManagerFactory);

                var plugin = pluginManager.Load<FakeCalculatorPlugin>();

                autoResetEvent.WaitOne(TimeSpan.FromMinutes(1));

                var pluginLifetimeManager = fakePluginLifetimeManagerFactory.LastPluginLifetimeManager as FakePluginLifetimeManager<FakeCalculatorPlugin>;

                Assert.True(pluginLifetimeManager.Unloaded);
            }
        }

        [Fact]
        public void Load_WhenTwoVersionsOfThePluginAreLoaded_OperationIsSuccessfull()
        {
            var pluginManager = new PluginManager(AppDomain.CurrentDomain.BaseDirectory);

            FakeCalculatorPlugin plugin = pluginManager.Load<FakeCalculatorPlugin>();
            FakeAnotherCalculatorPlugin anotherPlugin = pluginManager.Load<FakeAnotherCalculatorPlugin>();

            Assert.NotNull(plugin);
            Assert.NotNull(anotherPlugin);
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

        [Fact]
        public void Unload_WhenOneVersionOfThePluginIsUnloaded_TheSecondExists()
        {
            var pluginManager = new PluginManager(AppDomain.CurrentDomain.BaseDirectory);

            pluginManager.Load<FakeCalculatorPlugin>();
            pluginManager.Load<FakeAnotherCalculatorPlugin>();

            pluginManager.Unload<FakeCalculatorPlugin>();

            Assert.True(pluginManager.IsLoaded<FakeAnotherCalculatorPlugin>());
        }

        [Fact]
        public void IsLoaded_WhenTheTypeWasLoaded_ReturnsTrue()
        {
            var pluginManager = new PluginManager(AppDomain.CurrentDomain.BaseDirectory);

            pluginManager.Load<FakeCalculatorPlugin>();

            Assert.True(pluginManager.IsLoaded<FakeCalculatorPlugin>());
        }

        [Fact]
        public void IsLoaded_WhenTheTypeWasUnloaded_ReturnsFalse()
        {
            var pluginManager = new PluginManager(AppDomain.CurrentDomain.BaseDirectory);

            pluginManager.Load<FakeCalculatorPlugin>();
            pluginManager.Unload<FakeCalculatorPlugin>();

            Assert.False(pluginManager.IsLoaded<FakeCalculatorPlugin>());
        }
    }
}
