using System;

namespace MessageQueues.HarvesterHost.Core.Services
{
    public sealed class FileServiceFactory
    {
        private readonly Func<string, IFileService> _factory;

        public FileServiceFactory(Func<string, IFileService> factory)
        {
            _factory = factory;
        }

        public IFileService Create(string destinationPath)
        {
            return _factory.Invoke(destinationPath);
        }
    }
}
