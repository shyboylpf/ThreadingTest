using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest107
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            "abcdef".AsParallel().Select(c => char.ToUpper(c)).ForAll(Console.WriteLine);
        }
    }
}