using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest102
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int i = 0;
            var query = from n in Enumerable.Range(0, 8).AsParallel().AsOrdered() select n * i++;

            foreach (var item in query)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("===============");

            query = Enumerable.Range(0, 8).AsParallel().Select((n, j) => n * j);

            foreach (var item in query)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}