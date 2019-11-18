using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            warningType warning = (warningType)10;
            Console.WriteLine(warning);
            // normal
        }
    }

    internal enum warningType
    {
        normal = 10,
        warning = 20,
    }
}