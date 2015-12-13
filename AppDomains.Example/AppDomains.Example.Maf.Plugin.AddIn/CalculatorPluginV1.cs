using System;
using System.AddIn;
using AppDomains.Example.Maf.Plugin.AddInView;

namespace AppDomains.Example.Maf.Plugin.AddIn
{
    [AddIn("Calculator AddIn", Version = "1.0.0.0")]
    public class CalculatorPluginV1 : ICalculatorPlugin
    {
        public bool IsPrime(int number)
        {
            if (number <= 0)
            {
                throw new ArgumentException("The number value could not be non-positive!");
            }

            if (number == 1)
            {
                return false;
            }

            bool result = true;

            for (int i = 2; i < Math.Sqrt(number); ++i)
            {
                if (number % i == 0)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}
