using System;

namespace Debugging.Example.KeyGenerator
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            var keyGenerator = new KeyGenerator();

            Console.WriteLine("Key: {0}", keyGenerator.Generate());
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
