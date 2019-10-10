using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest75
{
    class Program
    {
        static void Main(string[] args)
        {
            // First interval = 5000ms; subsequent intervals = 1000ms
            Timer tmr = new Timer(Tick, "tick...", 5000, 1000);
            Console.ReadLine();
            tmr.Dispose();  // this both stops the timer and cleans up.
        }

        private static void Tick(object state)
        {
            // this run on a pooled thread
            Console.WriteLine(state);  // Writes "tick..."
        }
    }
}
