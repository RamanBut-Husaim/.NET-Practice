namespace MessageQueues.Core.Operations.Rename
{
    public interface IRenameOperation : IOperation
    {
        string OldPath { get; }
        string NewPath { get; }
    }
}
