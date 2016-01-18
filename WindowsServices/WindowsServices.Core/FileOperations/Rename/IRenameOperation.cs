namespace WindowsServices.Core.FileOperations.Rename
{
    public interface IRenameOperation : IOperation
    {
        string OldPath { get; }
        string NewPath { get; }
    }
}
