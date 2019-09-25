using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest42
{
    class Program
    {
        public static ConcurrentBag<int> vs = new ConcurrentBag<int>();

        public static List<int> ls = new List<int>();
        static void Main(string[] args)
        {
            //vs.Add(3);
            //vs.Add(4);
            //vs.Add(5);
            //Console.WriteLine(vs.Count());
            //var vs1 = vs.Where(x => x > 3);
            //foreach (var item in vs1)
            //{
            //    Console.WriteLine(item);
            //}

            //var vs2 = vs.First(x => x > 3);
            ////Console.WriteLine(vs2);
            //vs.TryTake(out var vs3);
            ////foreach(var item in vs3)
            ////{
            ////    Console.WriteLine($"take:{item}");
            ////}
            //Console.WriteLine($"Count:{vs3}");
            //Console.WriteLine($"Count:{vs.Count}");

            //vs.Take<int>(1);
            //vs.TryPeek(out int i);

            //Console.WriteLine("second test.");
            //ls.Add(3);
            //ls.Add(4);
            //ls.Add(5);
            //var vs4 = ls.Where(x => x > 3);
            //foreach (var item in vs4)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine("Thrid test.");
            //lock (ls)
            //{
            //    ls.Add(7);
            //    ls.Add(8);
            //    ls.Add(9);
            //}
            //lock (ls)
            //{
            //    ls.RemoveAll(x => x > 3);
            //}
            //Console.WriteLine(ls.Count);
            Task.Factory.StartNew(Producer);
            Task.Factory.StartNew(Consumer);
            Console.ReadKey();
        }

        private static void Consumer()
        {
            Thread.SpinWait(10000);
            lock (vs)
            {
                Console.WriteLine(vs.Count);
            }
        }

        private static void Producer()
        {
            lock (vs)
            {
                Thread.Sleep(2000);
            }
            checked
            {
                
            }
        }
    }
}
