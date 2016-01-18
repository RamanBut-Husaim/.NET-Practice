namespace WindowsServices.Core.FileOperations
{
    public interface IRenameOperation : IOperation
    {
        string OldPath { get; }
        string NewPath { get; }
    }
}
