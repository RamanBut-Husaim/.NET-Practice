using System;
using System.Runtime.InteropServices;

namespace UnmanagedCode.Example
{
    [ComVisible(true)]
    [Guid("1AFB1C4C-6C7F-45C6-8B62-63A2243BA9C3")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IPowerManagerCom
    {
        DateTime GetLastSleepTime();
        DateTime GetLastWakeTime();
    }
}
