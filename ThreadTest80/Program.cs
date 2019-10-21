using System;
using System.Threading;

namespace ThreadTest80
{
    internal class Program
    {
        private static long _sum;

        private static void Main(string[] args)
        {
            // 简单的加减操作 Simple increment/decrement operations.
            Interlocked.Increment(ref _sum);        // 1
            Interlocked.Decrement(ref _sum);        // 0

            // 加减一个值
            Interlocked.Add(ref _sum, 3);           // 3

            // 读取一个64位的属性
            Console.WriteLine(Interlocked.Read(ref _sum));  //3

            // 写入64位field的同时写入读取前一个值
            // (读取并打印3 , 并将_sum更新为10)
            Console.WriteLine(Interlocked.Exchange(ref _sum, 10));

            // 仅当字段匹配特定值（10）时才更新：
            Console.WriteLine(Interlocked.CompareExchange(ref _sum, 123, 10));

            for (int i = 0; i < 100; i++)
            {
                new Thread(ThreadSafe.Go).Start();
                new Thread(ThreadUnsafe.Go).Start();
            }
            Console.ReadLine();
        }
    }

    /// <summary>
    /// 在32位环境下, 读写64位字段是非原子性的, 因为它需要两条单独的指令：每个32位存储器位置一条指令。
    /// </summary>
    internal class Atomicity
    {
        private static int _x, _y;
        private static long _z;

        private static void Test()
        {
            long myLocal;
            _x = 3;         // Atomic
            _z = 3;         // Nonatomic on 32-bit environs(_z is 64 bits)
            myLocal = _z;   // 在32位环境下非原子性操作(_z是64位的.
            _y += _x;       // 非原子操作(读和写操作)
            _x++;           // 非原子操作(读和写操作)
        }
    }

    /// <summary>
    /// 当10个线程同时执行时, 最终_x并不会一定为0, 因为在读取_x和_x--写会的之间 , 可能别的线程读取_x , 变成了脏读.
    /// </summary>
    internal class ThreadUnsafe
    {
        private static int _x = 1000;

        public static void Go()
        {
            for (int i = 0; i < 10; i++)
            {
                _x--;
                if (_x == 0)
                {
                    Console.WriteLine($"[Thread Unsafe] ThreadID : {Thread.CurrentThread.ManagedThreadId} , Complete. ");
                }
            }
        }
    }

    internal class ThreadSafe
    {
        private static long _x = 1000;

        public static void Go()
        {
            for (int i = 0; i < 10; i++)
            {
                Interlocked.Add(ref _x, -1);
                if (Interlocked.Read(ref _x) == 0)
                {
                    Console.WriteLine($"[Thread safe] ThreadID : {Thread.CurrentThread.ManagedThreadId} , Complete. ");
                }
            }
        }
    }
}