namespace MessageQueues.Core.Operations.Synchronization
{
    public interface ISynchronizationOperation : IOperation
    {
        string Path { get; }
    }
}
