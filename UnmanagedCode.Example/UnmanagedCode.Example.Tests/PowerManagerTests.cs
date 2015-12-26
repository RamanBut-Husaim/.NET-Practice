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

        [Fact]
        public void GetSystemBatteryState_WhenStateIsRequested_ReturnsSuccessfully()
        {
            // the output for PC with UPS is different for each call. seems strange.
            SystemBatteryState batteryState = _powerManager.GetSystemBatteryState();

            _outputWriter.WriteLine("Max Capacity: {0}", batteryState.MaxCapacity);
            _outputWriter.WriteLine("Remaining Capacity: {0}", batteryState.RemainingCapacity);
            _outputWriter.WriteLine("Estimated time: {0} seconds", batteryState.EstimatedTime);
        }

        [Fact]
        public void GetSystemPowerInformation_WhenStateIsRequested_ReturnsSuccessfully()
        {
            SystemPowerInformation powerInformation = _powerManager.GetSystemPowerInformation();

            _outputWriter.WriteLine("Maximum Idleness Allowed: {0}", powerInformation.MaxIdlenessAllowed);
            _outputWriter.WriteLine("The current idle level: {0} %", powerInformation.Idleness);
            _outputWriter.WriteLine("The time remaining in the idle timer: {0} seconds", powerInformation.TimeRemaining);
        }

        [Fact]
        public void CommitHibernationFile_WhenOperationIsRequested_ReturnsSuccessfully()
        {
            bool reservationSucceeded = _powerManager.CommitHibernationFile();

            Assert.True(reservationSucceeded);
        }

        [Fact]
        public void UncommitHibernationFile_WhenOperationIsRequested_ReturnsSuccessfully()
        {
            bool reservationSucceeded = _powerManager.UncommitHibernationFile();

            Assert.True(reservationSucceeded);
        }
    }
}
