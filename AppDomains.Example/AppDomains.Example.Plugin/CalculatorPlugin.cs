using System;
using AppDomains.Example.Plugin.Contracts;

namespace AppDomains.Example.Plugin
{
    public sealed class CalculatorPlugin : PluginBase, ICalculatorPlugin
    {
        public CalculatorPlugin(TimeSpan initialLifeTime, TimeSpan renewalOnCallTime) : base(initialLifeTime, renewalOnCallTime)
        {
        }

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
