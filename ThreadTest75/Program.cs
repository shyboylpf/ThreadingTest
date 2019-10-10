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


            Thread.Sleep(10000);
            Console.WriteLine("Timer.Change函数动态改变起始时间和间隔时间");
            // 可以用change函数动态额改变起始时间和间隔时间
            tmr.Change(1000, 5000);

            Thread.Sleep(10000);
            Console.WriteLine("间隔时间改为Timeout.Infinite  使函数仅执行一次");
            // 如果只想调用一次
            tmr.Change(1000, Timeout.Infinite);

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
