using System.AddIn.Pipeline;
using AppDomains.Example.Maf.Plugin.Contract;
using AppDomains.Example.Maf.Plugin.HostViewAddIn;

namespace AppDomains.Example.Maf.Plugin.HostSideAdapter
{
    [HostAdapter]
    public class CalculatorPluginContractToViewHostSideAdapter : ICalculatorPlugin
    {
        private readonly ICalculatorPluginContract _calculatorPluginContract;
        private readonly ContractHandle _contractHandle;

        public CalculatorPluginContractToViewHostSideAdapter(ICalculatorPluginContract calculatorPluginContract)
        {
            _calculatorPluginContract = calculatorPluginContract;
            _contractHandle = new ContractHandle(calculatorPluginContract);
        }

        public bool IsPrime(int number)
        {
            return _calculatorPluginContract.IsPrime(number);
        }
    }
}
