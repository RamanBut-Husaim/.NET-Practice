using System;
using System.AddIn.Hosting;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using AppDomains.Example.Maf.Plugin.HostViewAddIn;

namespace AppDomains.Example.Maf.Plugin.Host
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            var directoryInfo = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
            var addInRootPath = Path.Combine(directoryInfo.Parent.FullName, "Pipeline");

            string[] warnings = AddInStore.Update(addInRootPath);
            DisplayWarnings(warnings);

            Collection<AddInToken> tokens = AddInStore.FindAddIns(typeof (ICalculatorPlugin), addInRootPath);
            DisplayTokens(tokens);

            foreach (AddInToken token in tokens)
            {
                ICalculatorPlugin calculatorPlugin = token.Activate<ICalculatorPlugin>(AddInSecurityLevel.FullTrust);
            }

            Console.ReadLine();
        }

        private static void DisplayWarnings(string[] warnings)
        {
            foreach (string warning in warnings)
            {
                Console.WriteLine(warning);
            }
        }

        private static void DisplayTokens(Collection<AddInToken> tokens)
        {
            for (int i = 0; i < tokens.Count; ++i)
            {
                var token = tokens[i];
                Console.WriteLine("({0}) {1} - {2}\t {3}\t {4}\t {5}", i, token.Name, token.AddInFullName, token.AssemblyName, token.Description, token.Version);
            }
        }
    }
}
