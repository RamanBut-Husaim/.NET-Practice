using System;

namespace MemoryLeaks.FinalizationBlockade
{
    public sealed class MemorySpam : IDisposable
    {
        private readonly byte[] _bigSpam;

        public MemorySpam()
        {
            _bigSpam = new byte[1024 * 1024];
        }

        ~MemorySpam()
        {
        }

        public void Dispose()
        {
        }
    }
}
