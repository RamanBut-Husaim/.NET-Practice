using System;
using MessageQueues.Core;
using MessageQueues.Core.Messages;

namespace MessageQueues.HarvesterHost.Core.FileOperations
{
    public sealed class FileSender : IFileSender
    {
        private readonly ITransferManager _transferManager;
        private readonly string _harvester;

        public FileSender(ITransferManager transferManager, string harvester)
        {
            _transferManager = transferManager;
            _harvester = harvester;
        }

        public void Send(FileMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            message.Harvester = _harvester;

            _transferManager.Send(message);
        }
    }
}
