using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest98
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Thread t = new Thread(Work);
            t.Start();
            Thread.Sleep(1000); t.Abort();
            Thread.Sleep(1000); t.Abort();
            Thread.Sleep(1000); t.Abort();
        }

        private static void Work()
        {
            while (true)
            {
                try { while (true) ; }
                catch (ThreadAbortException)
                {
                    Thread.ResetAbort();
                }
                Console.WriteLine("I will not die! ");
            }
        }
    }
}