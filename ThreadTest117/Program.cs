using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest117
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            // 并发编程,  使用本地结果 , 然后汇总
            Console.WriteLine("parallel");
            stopwatch.Start();
            object locker2 = new object();
            double grandTotal = 0;
            Parallel.For(1, 10000000,
                () => 0.0,
                (i, state, localTotal) =>
                    localTotal + Math.Sqrt(i),

                localTotal =>
                { lock (locker2) grandTotal += localTotal; }
                );
            stopwatch.Stop();
            Console.WriteLine("ElapsedMilliseconds: " + stopwatch.ElapsedMilliseconds);

            // 并发编程,  然后锁定结果项 , 还不如不并发 ,此处要维护10000000个锁
            Console.WriteLine("parallel with lock");
            stopwatch.Restart();
            object locker = new object();
            double total = 0;
            Parallel.For(1, 10000000, i =>
            {
                lock (locker)
                {
                    total += Math.Sqrt(i);
                }
            });
            stopwatch.Stop();
            Console.WriteLine("ElapsedMilliseconds: " + stopwatch.ElapsedMilliseconds);

            // for循环
            Console.WriteLine("No parallel just for");
            stopwatch.Restart();
            double grantTotal2 = 0;
            for (int i = 0; i < 10000000; i++)
            {
                grantTotal2 += Math.Sqrt(i);
            }
            stopwatch.Stop();
            Console.WriteLine("ElapsedMilliseconds: " + stopwatch.ElapsedMilliseconds);

            Console.WriteLine("PLINQ Sum");
            stopwatch.Restart();
            ParallelEnumerable.Range(1, 10000000)
                .Sum(i => Math.Sqrt(i));
            stopwatch.Stop();
            Console.WriteLine("ElapsedMilliseconds: " + stopwatch.ElapsedMilliseconds);
        }
    }
}