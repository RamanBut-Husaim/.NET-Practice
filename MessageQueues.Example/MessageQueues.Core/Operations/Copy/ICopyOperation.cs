namespace MessageQueues.Core.Operations.Copy
{
    public interface ICopyOperation : IOperation
    {
        string Path { get; }
    }
}
