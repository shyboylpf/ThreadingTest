using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest67
{
    class Program
    {
        static void Main(string[] args)
        {
            int _lastRread = 0;
            int index = 10;
            while(Interlocked.CompareExchange(ref _lastRread, index+1, index) < index)
            {
                index--;
                Console.WriteLine(_lastRread);
            }
            Console.WriteLine("result: " + _lastRread);
        }
    }
}
