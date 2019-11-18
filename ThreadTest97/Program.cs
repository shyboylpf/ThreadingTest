using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest97
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Thread t = new Thread(delegate () { while (true) ; });

            Console.WriteLine(t.ThreadState); // Unstarted

            t.Start();
            Thread.Sleep(1000);
            Console.WriteLine(t.ThreadState);  // running

            t.Abort();
            Console.WriteLine(t.ThreadState);   // aborted

            t.Join();
            Console.WriteLine(t.ThreadState);   // aborted
        }
    }
}