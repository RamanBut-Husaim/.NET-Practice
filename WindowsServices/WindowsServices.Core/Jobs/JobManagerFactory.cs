using System;

namespace WindowsServices.Core.Jobs
{
    public sealed class JobManagerFactory
    {
        private readonly Func<string, IJobManager> _factory;

        public JobManagerFactory(Func<string, IJobManager> factory)
        {
            _factory = factory;
        }

        public IJobManager Create(string destinationPath)
        {
            return _factory.Invoke(destinationPath);
        }
    }
}
