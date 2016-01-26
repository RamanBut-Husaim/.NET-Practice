namespace MessageQueues.HarvesterHost.Core.FileOperations.Synchronization
{
    public interface ISynchronizationOperation : IOperation
    {
        string SourcePath { get; }

        string DestinationPath { get; }
    }
}
