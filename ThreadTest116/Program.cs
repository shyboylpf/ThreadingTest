using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest116
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // sortedset 底层用的红黑树
            SortedSet<int> vs = new SortedSet<int>();

            vs.Add(1);
            vs.Add(2);
            vs.Add(3);

            vs.Remove(2);

            vs.Add(2);

            Console.WriteLine(vs.First(x => x == 2));
        }
    }
}