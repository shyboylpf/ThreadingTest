using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest74
{
    class Program
    {
        static void Main(string[] args)
        {
            // 简陋版定时器 , 永久占据线程资源
            // 匿名函数
            new Thread(delegate ()
            {
                while (true)
                {
                    Console.WriteLine(DateTime.Now);
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
            }).Start();

            // 匿名函数
            new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine(DateTime.Now);
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
            }).Start();


            // .NET Framework提供了四个计时器。其中两个是通用多线程计时器：
            // System.Threading.Timer
            // System.Timers.Timer
            // 另外两个是专用的单线程计时器：
            // System.Windows.Forms.Timer (Windows Forms timer)
            // System.Windows.Threading.DispatcherTimer(WPF timer)
            // 多线程计时器功能更强大，更准确，更灵活。单线程计时器对于运行更新Windows Forms控件或WPF元素的简单任务更安全，更方便。


        }
    }
}
