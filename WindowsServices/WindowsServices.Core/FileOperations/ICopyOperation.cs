namespace WindowsServices.Core.FileOperations
{
    public interface ICopyOperation : IOperation
    {
        string SourcePath { get; }

        string DestinationPath { get; }
    }
}
