///Naming Threads
///Each thread has a Name property that you can set for the benefit of debugging.This is particularly useful in Visual Studio, since the thread’s name 
///is displayed in the Threads Window and Debug Location toolbar.You can set a thread’s name just once; attempts to change it later will throw an exception.

///The static Thread.CurrentThread property gives you the currently executing thread.In the following example, we set the main thread’s name:

using System;
using System.Threading;

namespace ThreadTest10
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "main";

            Thread worker = new Thread(Go);
            worker.Name = "worker";
            worker.Start();

            Go();
        }

        static void Go()
        {
            Console.WriteLine("Hello from " + Thread.CurrentThread.Name);
        }
    }
}
