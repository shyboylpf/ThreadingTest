using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest78
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start");
            bool complete = false;
            var t = new Thread(delegate()
            {
                bool toggle = false;
                while (!complete)
                {
                    Thread.MemoryBarrier();
                    toggle = !toggle;
                }
            });
            t.Start();
            Thread.Sleep(1000);
            complete = true;
            t.Join(); // Block indefinitely.
        }
    }
}
