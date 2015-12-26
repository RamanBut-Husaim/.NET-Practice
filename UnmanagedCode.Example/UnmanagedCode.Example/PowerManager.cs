using System;
using System.Runtime.InteropServices;

namespace UnmanagedCode.Example
{
    public sealed class PowerManager
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
