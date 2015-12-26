using System;

namespace UnmanagedCode.Example
{
    public interface IPowerManager
    {
        DateTime GetLastSleepTime();

        DateTime GetLastWakeTime();

        SystemBatteryState GetSystemBatteryState();

        bool CommitHibernationFile();

        bool UncommitHibernationFile();

        void SendToSleep();
    }
}
