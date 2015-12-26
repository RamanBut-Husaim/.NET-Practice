using System;
using Xunit;
using Xunit.Abstractions;

namespace UnmanagedCode.Example.Tests
{
    public sealed class PowerManagerTests
    {
        private readonly ITestOutputHelper _outputWriter;

        private readonly PowerManager _powerManager;

        public PowerManagerTests(ITestOutputHelper outputWriter)
        {
            _outputWriter = outputWriter;

            _powerManager = new PowerManager();
        }

        [Fact]
        public void GetLastSleepTime_WhenTimeIsRequested_ReturnsSuccessfully()
        {
            DateTime lastSleepTime = _powerManager.GetLastSleepTime();

            _outputWriter.WriteLine("Last sleep time: {0}", lastSleepTime.ToLocalTime());
        }

        [Fact]
        public void GetLastWakeTime_WhenTimeIsRequested_ReturnsSuccessfully()
        {
            DateTime lastWakeTime = _powerManager.GetLastWakeTime();

            _outputWriter.WriteLine("Last wake time: {0}", lastWakeTime.ToLocalTime());
        }
    }
}
