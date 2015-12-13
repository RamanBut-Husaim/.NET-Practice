using System.AddIn.Pipeline;

namespace AppDomains.Example.Maf.Plugin.AddInView
{
    [AddInBase]
    public interface ICalculatorPlugin
    {
        bool IsPrime(int number);
    }
}
