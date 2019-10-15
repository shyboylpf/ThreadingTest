using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest87
{
    internal class Program
    {
        private static readonly object _locker = new object();

        private static void Main(string[] args)
        {
            Console.WriteLine("Start.");
            new Thread(Work).Start();
            lock (_locker)
            {
                Monitor.Pulse(_locker);
            }
        }

        private static void Work()
        {
            lock (_locker)
            {
                Monitor.Wait(_locker);
            }
            Console.WriteLine("Woken!!!");
        }

        ///Pulse has no latching effect because you’re expected to write the latch yourself, using a “go” flag as we did before.
        ///This is what makes Wait and Pulse versatile: with a boolean flag, we can make it function as an AutoResetEvent; with
        ///an integer field, we can write a CountdownEvent or a Semaphore. With more complex data structures, we can go further
        ///and write such constructs as a producer/consumer queue.
    }
}