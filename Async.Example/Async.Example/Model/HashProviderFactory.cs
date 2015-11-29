namespace Async.Example.Model
{
    public sealed class HashProviderFactory
    {
        public HashProvider Create()
        {
            return new HashProvider();
        }
    }
}
