using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;  // Timers namespace rather than Threading
using System.Threading.Tasks;

namespace ThreadTest76
{
    class Program
    {
        /// <summary>
        /// System.Timers是对System.Threading.Timer的封装, 提供更多功能
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Console.WriteLine(new Random(Guid.NewGuid().GetHashCode()).Next());
            //Console.WriteLine(new Random(Guid.NewGuid().GetHashCode()).Next());
            //Console.WriteLine(new Random(Guid.NewGuid().GetHashCode()).Next());
            Timer tmr = new Timer();        // 不需要任何参数
            tmr.Interval = 500;             // 间隔时间
            tmr.Elapsed += tmr_Elapsed;     // 用时间代替委托 , uses an event instead of a delegate.
            tmr.Start();
            Console.ReadLine();
            tmr.Stop();
            Console.ReadLine();
            tmr.Start();
            Console.ReadLine();
            tmr.Dispose();
        }

        private static void tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Tick");
        }
        // 多线程计时器使用线程池来允许几个线程为多个计时器提供服务。
        // 事件可能在不同的线程上触发
        // 即使上一个Elapsed函数未完成, 此函数还是会按时触发, 所以, Elapsed函数必须是thread-safe的.
        // 高精度(1ms)计时器请搜索关键词 dllimport winmm.dll timesetevent.
    }
}
