using System;
using System.Runtime.InteropServices;

namespace UnmanagedCode.Example
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("7A8DBB39-9A0A-42E9-BAB9-F87468709852")]
    public sealed class PowerManagerCom : IPowerManagerCom
    {
        private readonly IPowerManager _powerManager;

        public PowerManagerCom()
        {
            _powerManager = new PowerManager();
        }

        public DateTime GetLastSleepTime()
        {
            return _powerManager.GetLastSleepTime();
        }

        public DateTime GetLastWakeTime()
        {
            return _powerManager.GetLastWakeTime();
        }
    }
}
