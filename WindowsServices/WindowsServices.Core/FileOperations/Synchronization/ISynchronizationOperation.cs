namespace WindowsServices.Core.FileOperations.Synchronization
{
    public interface ISynchronizationOperation : IOperation
    {
        string SourcePath { get; }

        string DestinationPath { get; }
    }
}
