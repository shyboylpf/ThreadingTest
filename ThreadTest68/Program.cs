using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest68
{
    class Program
    {
        // The easiest approach to thread-local storage is to mark a static field with the ThreadStatic attribute:
        [ThreadStatic] static int _x;

        // ThreadLocal<T> is new to Framework 4.0. It provides thread-local storage for both static and instance fields — and allows you to specify default values.
        static ThreadLocal<int> _x1 = new ThreadLocal<int>(() => 3);

        static Random _rd = new Random();
        static void Main(string[] args)
        {
            int x1 = _x1.Value;

            for(int i = 0; i < 2; i++)
            {
                new Thread(localRand).Start();
                Thread.SpinWait(1000);
            }

            Foo foo = new Foo();
            Console.WriteLine("threadLocal in sephore class: " + foo.localRandom.Value.Next());
            Console.WriteLine("threadLocal in sephore class: " + foo.localRandom.Value.Next());

            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("new class:" + new Random().Next());
                Thread.Sleep(1000);
            }

            for(int i = 0; i < 2; i++)
            {
                int tmp = 0;
                lock (_rd)
                {
                    tmp = _rd.Next();
                }
                Console.WriteLine("static random: " + tmp);
            }

            Console.WriteLine("===random thread-unsafe start===");
            for(int i = 0; i < 5; i++)
            {
                new Thread(new Foo2().print).Start();
                Thread.Sleep(1000);
            }
        }

        static void localRand()
        {
            var localRandom = new ThreadLocal<Random>(() => new Random());
            Console.WriteLine("threadLocal: " + localRandom.Value.Next());
        }
    }

    class Foo
    {
        public ThreadLocal<Random> localRandom = new ThreadLocal<Random>(() => new Random());
    }

    class Foo2
    {
        public static readonly Random random = new Random();
        static Barrier _barrier = new Barrier(5, barrier => Console.WriteLine("人齐了, 开始干"));
        public void print()
        {
            //unsafe
            //{
            //    TypedReference tr = __makeref(random);
            //    IntPtr ptr = **(IntPtr**)(&tr);
            //}
            _barrier.SignalAndWait();
            Console.WriteLine("thread-unsafe random: " + random.Next());
        }
    }
}
