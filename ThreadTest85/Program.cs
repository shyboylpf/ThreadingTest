using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadTest85
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new Foo();
            Console.ReadLine();
        }
    }

    internal class Foo
    {
        private Timer tmr = null;

        public Foo()
        {
            tmr = new Timer(test, null, 0, 1000);
        }

        private void test(object state)
        {
            Console.WriteLine($"start from timer.{Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(2000);
            throw (new Exception());
            Console.WriteLine($"end from timer.{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}