using System;
using System.Threading.Tasks;
using NLog;

namespace WindowsServices.Core.FileOperations
{
    public sealed class PollingManager : IPollingManager
    {
        private readonly ILogger _logger;
        private readonly int _waitInternal;
        private readonly int _accessCount;

        public PollingManager(
            ILogger logger,
            int waitInternal,
            int accessCount)
        {
            _logger = logger;
            _waitInternal = waitInternal;
            _accessCount = accessCount;
        }

        public int WaitInterval
        {
            get { return _waitInternal; }
        }

        public int AccessCount
        {
            get { return _accessCount; }
        }

        public async Task Perform<TException>(Func<Task> func) where TException : Exception
        {
            for (int i = 0; i < _accessCount; ++i)
            {
                // OK, I know, C# 6.0 supports this, but the situation is different.
                bool operationCompletedSuccessfully = false;
                try
                {
                    await func();
                    operationCompletedSuccessfully = true;
                    return;
                }
                catch (TException ex)
                {
                    _logger.Error(ex);
                }

                if (!operationCompletedSuccessfully)
                {
                    await Task.Delay(_waitInternal);
                }
            }
        }
    }
}
