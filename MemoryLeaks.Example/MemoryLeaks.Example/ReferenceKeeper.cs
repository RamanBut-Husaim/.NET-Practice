using System;

namespace MemoryLeaks.Example
{
    public sealed class ReferenceKeeper
    {
        public event EventHandler Notifier = delegate { };

        public void Notify()
        {
            this.Notifier.Invoke(this, null);
        }
    }
}
