using System.AddIn.Contract;
using System.AddIn.Pipeline;

namespace AppDomains.Example.Maf.Plugin.Contract
{
    [AddInContract]
    public interface ICalculatorPluginContract : IContract
    {
        bool IsPrime(int number);
    }
}