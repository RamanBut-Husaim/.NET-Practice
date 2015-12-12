namespace AppDomains.Example.Plugin.Contracts
{
    public interface ICalculatorPlugin : IPlugin
    {
        bool IsPrime(int number);
    }
}
