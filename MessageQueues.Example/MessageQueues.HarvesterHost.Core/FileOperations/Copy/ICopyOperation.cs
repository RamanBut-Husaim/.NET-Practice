namespace MessageQueues.HarvesterHost.Core.FileOperations.Copy
{
    public interface ICopyOperation : IOperation
    {
        string SourcePath { get; }
    }
}
