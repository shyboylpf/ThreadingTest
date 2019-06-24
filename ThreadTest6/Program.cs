///You can wait for another thread to end by calling its Join method. 
///For example:
using System;
using System.Threading;

namespace ThreadTest6
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(Go);
            t.Start();
            t.Join();
            Console.WriteLine("Thread t has ended!");
        }
        static void Go()
        {
            for (int i = 0; i < 1000; i++) Console.Write("y");
            //Thread.Sleep(0);
            Thread.Yield();
            //Thread.Sleep(TimeSpan.FromHours(1));        // sleep for 1 hour
            //Thread.Sleep(500);                          // sleep for 500 milliseconds.
        }
    }
}
///While waiting on a Sleep or Join, a thread is blocked and so does not consume CPU resources.
///Sleep(0) or Yield is occasionally useful in production code for advanced performance tweaks. 
///It’s also an excellent diagnostic tool for helping to uncover thread safety issues: if inserting Thread.Yield() anywhere 
///in your code makes or breaks the program, you almost certainly have a bug.