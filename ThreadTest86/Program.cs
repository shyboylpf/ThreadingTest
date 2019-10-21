using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest86
{
    internal class Program
    {
        // 定义一个字段作为同步对象
        private static readonly object _locker = new object();

        // 定义字段, 用于自定义blocking条件
        private static bool _go;

        private static void Main(string[] args)
        {                                           // 新的线程会被block
            new Thread(Work).Start();               // 因为 _go==false

            Console.ReadLine();                     // 等待用户敲击回车

            lock (_locker)                          //现在让我们设置_go=true
            {                                       // 来唤醒线程
                _go = true;
                Monitor.Pulse(_locker);
            }
        }

        private static void Work()
        {
            lock (_locker)
            {
                while (!_go)
                {
                    Monitor.Wait(_locker);          // 当我们等待的时候, Lock会被释放
                }
            }
            Console.WriteLine("Woken!!!");
        }

        // Work方法中我们会阻塞直到_go编程true
        // Monitor.Wait方法会做以下事情:
        // 1. 释放_locker上的锁
        // 2. 阻塞自己, 直到_locker被pulse
        // 3. 重新获取_locker上的锁 , 如果发生锁竞争 , 就阻塞自己, 直到锁可用
    }
}