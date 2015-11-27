using System;

namespace Async.Example.Model
{
    public sealed class ProgressNotifier : IProgressNotifier
    {
        private readonly Action<double> _progressUpdated;
        private readonly int _elementCount;

        public ProgressNotifier(Action<double> progressUpdated, int elementCount)
        {
            _progressUpdated = progressUpdated;
            _elementCount = Math.Max(elementCount, 1);
        }

        public void NotifyProgress(int value)
        {
            double progressValue = ((double) value / _elementCount) * 100;
            _progressUpdated.Invoke(progressValue);
        }
    }
}
