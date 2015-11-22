using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Multithreading.Example.Core
{
    public sealed class FileProcessor : IFileProcessor
    {
        private readonly string _content;

        public FileProcessor(string content)
        {
            _content = content;
        }

        public string Process()
        {
            IEnumerable<int> factorialNumbers = this.GetFactorialNumbers();

            var result = new StringBuilder();
            foreach (var factorialNumber in factorialNumbers)
            {
                BigInteger factorial = this.CalculateFactorial(factorialNumber);
                result.Append(factorial.ToString()).Append(' ');
            }

            return result.ToString();
        }

        private IEnumerable<int> GetFactorialNumbers()
        {
            string[] stringFactorialNumbers = _content.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            var factorialNumbers = new List<int>();
            for (int i = 0; i < stringFactorialNumbers.Length; ++i)
            {
                int factorialNumber;
                if (int.TryParse(stringFactorialNumbers[i], out factorialNumber))
                {
                    factorialNumbers.Add(factorialNumber);
                }
            }

            return factorialNumbers;
        }

        private BigInteger CalculateFactorial(int factorialNumber)
        {
            var result = new BigInteger(1);

            for (int i = factorialNumber; i > 1; --i)
            {
                result *= factorialNumber;
            }

            return result;
        }
    }
}
