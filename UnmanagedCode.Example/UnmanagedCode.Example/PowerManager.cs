using System;
using System.Runtime.InteropServices;

namespace UnmanagedCode.Example
{
    public sealed class PowerManager : IPowerManager
    {
        private const uint SuccessfullCompletion = 0;

        [DllImport("kernel32.dll")]
        private static extern ulong GetTickCount64();

        [DllImport("powrprof.dll", SetLastError = true)]
        private static extern uint CallNtPowerInformation(
            int InformationLevel,
            IntPtr lpInputBuffer,
            uint nInputBufferSize,
            [Out] IntPtr lpOutputBuffer,
            uint nOutputBufferSize
            );

        public DateTime GetLastSleepTime()
        {
            return this.GetLastTime(PowerInformationLevel.LastSleepTime);
        }

        public DateTime GetLastWakeTime()
        {
            return this.GetLastTime(PowerInformationLevel.LastWakeTime);
        }

        public SystemBatteryState GetSystemBatteryState()
        {
            return this.GetSystemInformation<SystemBatteryState>(PowerInformationLevel.SystemBatteryState);
        }

        public SystemPowerInformation GetSystemPowerInformation()
        {
            return this.GetSystemInformation<SystemPowerInformation>(PowerInformationLevel.SystemPowerInformation);
        }

        public bool CommitHibernationFile()
        {
            return this.ManageHibernationFile(true);
        }

        public bool UncommitHibernationFile()
        {
            return this.ManageHibernationFile(false);
        }

        private bool ManageHibernationFile(bool reserve)
        {
            IntPtr ptr = IntPtr.Zero;

            try
            {
                int inputSize = Marshal.SizeOf(typeof(int));
                ptr = Marshal.AllocCoTaskMem(inputSize);
                Marshal.WriteInt32(ptr, reserve ? 1 : 0);

                uint operationCompletionCode = CallNtPowerInformation((int)PowerInformationLevel.SystemReserveHiberFile, ptr, (uint)inputSize, IntPtr.Zero, 0);
                if (operationCompletionCode != SuccessfullCompletion)
                {
                    Marshal.ThrowExceptionForHR((int)operationCompletionCode);
                }

                return true;
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(ptr);
                }
            }
        }

        private T GetSystemInformation<T>(PowerInformationLevel level)
        {
            IntPtr ptr = IntPtr.Zero;

            try
            {
                int outputSize = Marshal.SizeOf(typeof(T));
                ptr = Marshal.AllocCoTaskMem(outputSize);
                uint operationCompletionCode = CallNtPowerInformation((int)level, IntPtr.Zero, 0, ptr, (uint)outputSize);
                if (operationCompletionCode != SuccessfullCompletion)
                {
                    Marshal.ThrowExceptionForHR((int)operationCompletionCode);
                }

                var systemBatteryState = Marshal.PtrToStructure<T>(ptr);

                return systemBatteryState;
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(ptr);
                }
            }

        }

        private DateTime GetLastTime(PowerInformationLevel level)
        {
            IntPtr ptr = IntPtr.Zero;

            try
            {
                int outputSize = Marshal.SizeOf(typeof(ulong));
                ptr = Marshal.AllocCoTaskMem(outputSize);
                uint operationCompletionCode = CallNtPowerInformation((int)level, IntPtr.Zero, 0, ptr, (uint)outputSize);
                if (operationCompletionCode != SuccessfullCompletion)
                {
                    Marshal.ThrowExceptionForHR((int)operationCompletionCode);
                }

                long ticksCount = Marshal.ReadInt64(ptr);
                ulong systemStartupElapsedTime = this.GetSystemStartupElapsedTime();

                ulong timeElapsedSinceLastSleep = systemStartupElapsedTime - (ulong)ticksCount;

                return DateTime.UtcNow - TimeSpan.FromTicks((long)timeElapsedSinceLastSleep);
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(ptr);
                }
            }
        }

        private ulong GetSystemStartupElapsedTime()
        {
            const int microsecondMultiplier = 1000;
            const int tickCounterResolution = 100;
            const int nanosecondMultiplier = 1000 / tickCounterResolution;


            return GetTickCount64() * microsecondMultiplier * nanosecondMultiplier;
        }
    }
}
