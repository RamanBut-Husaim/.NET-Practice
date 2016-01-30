using MessageQueues.Core.Messages;

namespace MessageQueues.HarvesterHost.Core.FileOperations
{
    public interface IFileSender
    {
        void Send(FileMessage message);
    }
}
