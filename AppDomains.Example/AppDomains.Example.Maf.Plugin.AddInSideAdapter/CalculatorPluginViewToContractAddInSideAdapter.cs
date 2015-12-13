using System.AddIn.Pipeline;
using AppDomains.Example.Maf.Plugin.AddInView;
using AppDomains.Example.Maf.Plugin.Contract;

namespace AppDomains.Example.Maf.Plugin.AddInSideAdapter
{
    [AddInAdapter]
    public class CalculatorPluginViewToContractAddInSideAdapter : ContractBase, ICalculatorPluginContract
    {
        private readonly ICalculatorPlugin _calculatorPlugin;

        public CalculatorPluginViewToContractAddInSideAdapter(ICalculatorPlugin calculatorPlugin)
        {
            _calculatorPlugin = calculatorPlugin;
        }

        public bool IsPrime(int number)
        {
            return _calculatorPlugin.IsPrime(number);
        }
    }
}