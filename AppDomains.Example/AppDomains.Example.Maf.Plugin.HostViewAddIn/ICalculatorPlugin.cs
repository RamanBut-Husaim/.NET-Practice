namespace AppDomains.Example.Maf.Plugin.HostViewAddIn
{
    public interface ICalculatorPlugin : IHostViewAddIn
    {
        bool IsPrime(int number);
    }
}