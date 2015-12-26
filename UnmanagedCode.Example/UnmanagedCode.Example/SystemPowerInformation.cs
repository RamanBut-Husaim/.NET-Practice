using System.Runtime.InteropServices;

namespace UnmanagedCode.Example
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SystemPowerInformation
    {
        public uint MaxIdlenessAllowed;
        public uint Idleness;
        public uint TimeRemaining;
        public sbyte CoolingMode;
    }
}
