using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest61
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(delegate()
            {
                //Thread.Sleep(Timeout.Infinite);
                try
                {
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadInterruptedException)
                {
                    Console.Write("Forcibly ");
                }
                Console.WriteLine("Woken!");
            });
            t.Start();
            t.Interrupt();
        }
        // 除非未处理ThreadInterruptedException，否则中断线程不会导致线程结束。
        // Interrupting a thread does not cause the thread to end, unless the ThreadInterruptedException is unhandled.
    }
}
