using System;
using System.IO;
using AppDomains.Example.Maf.Plugin.HostViewAddIn;
using Xunit;
using Xunit.Abstractions;

namespace AppDomains.Example.Maf.Plugin.Host.Core.Tests
{
    public sealed class PluginManagerTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly string _addInRootPath;

        public PluginManagerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            var directoryInfo = new DirectoryInfo(Environment.CurrentDirectory);
            _addInRootPath = Path.Combine(directoryInfo.Parent.Parent.Parent.FullName, "AppDomains.Example.Maf.Plugin.Host", "App", "Pipeline");
        }

        [Fact]
        public void Load_WhenThePluginExists_ReturnPluginInstance()
        {
            var pluginManager = new PluginManager(_addInRootPath);

            ICalculatorPlugin calculatorPlugin = pluginManager.Load<ICalculatorPlugin>();

            Assert.NotNull(calculatorPlugin);
        }

        [Fact]
        public void IsLoaded_WhenPluginIsLoaded_ReturnsTrue()
        {
            var pluginManager = new PluginManager(_addInRootPath);

            ICalculatorPlugin calculatorPlugin = pluginManager.Load<ICalculatorPlugin>();

            Assert.True(pluginManager.IsLoaded<ICalculatorPlugin>());
        }

        [Fact]
        public void IsLoaded_WhenPluginIsUnloaded_ReturnsFalse()
        {
            var pluginManager = new PluginManager(_addInRootPath);

            ICalculatorPlugin calculatorPlugin = pluginManager.Load<ICalculatorPlugin>();
            pluginManager.Unload<ICalculatorPlugin>();

            Assert.False(pluginManager.IsLoaded<ICalculatorPlugin>());
        }
    }
}
