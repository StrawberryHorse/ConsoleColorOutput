using System;
using ConsoleColorOutput;

namespace TestCCO
{
    class Program
    {
        static void Main(string[] args)
        {
            var ou = new ConsoleOutputHelper();
            ou.WriteLine("{yellow}Hello World!{}");
        }
    }
}
