using System;

namespace MessageQueues.Core.Messages
{
    [Serializable]
    public sealed class FileMessage : TransferableModel
    {
        public FileMessage()
        {
            this.FileContent = new byte[0];
        }

        public string Harvester { get; set; }

        public string FileName { get; set; }

        public string NewName { get; set; }

        public OperationType OperationType { get; set; }

        public byte[] FileContent { get; set; }
    }
}
