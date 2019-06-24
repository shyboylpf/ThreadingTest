///Both of these examples illustrate another key concept: 
///that of thread safety (or rather, lack of it!) 
///The output is actually indeterminate: it’s possible (though unlikely) 
///that “Done” could be printed twice. If, however, we swap the order 
///of statements in the Go method, the odds of “Done” being printed 
///twice go up dramatically:
using System;
using System.Threading;

namespace ThreadTest5
{
    class Program
    {
        static bool done;
        static void Main(string[] args)
        {
            new Thread(Go).Start();
            Go();
        }
        static void Go()
        {
            if (!done)
            {
                Console.WriteLine("Done");
                done = true;
            }
        }
    }
}
///The problem is that one thread can be evaluating the if statement right 
///as the other thread is executing the WriteLine statement — before it’s 
///had a chance to set done to true.
