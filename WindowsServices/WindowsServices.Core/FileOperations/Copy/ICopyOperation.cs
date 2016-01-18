namespace WindowsServices.Core.FileOperations.Copy
{
    public interface ICopyOperation : IOperation
    {
        string SourcePath { get; }

        string DestinationPath { get; }
    }
}
