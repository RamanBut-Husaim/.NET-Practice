using System;
using System.Management;
using System.Threading;

namespace MemoryLeaks.FinalizationBlockade
{
    public sealed class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var obj = new ManagementObject("win32_logicaldisk.deviceid=\"C:\"");
            // There is an issue related to COM object wrapper finalization.
            // They should be finalized on the same thread. At this point
            // we call a function that creates a COM object inside. And actually
            // it should be disposed as no longer used.
            obj.Get();

            while (true)
            {
                // These objects are finalizable as well but finalization thread
                // destroys objects one by one and COM object is the first candidate.
                new MemorySpam();

                Thread.Sleep(TimeSpan.FromMilliseconds(10));
            }
        }
    }
}
