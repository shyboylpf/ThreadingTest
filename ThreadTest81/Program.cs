using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadTest81
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("start");
            new Thread(() => go(5000)).Start();
            Console.WriteLine("end");
        }

        private static void go(int i)
        {
            Thread.Sleep(i);
        }
    }
}