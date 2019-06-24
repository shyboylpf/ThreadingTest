///Static fields offer another way to share data between threads. 
///Here’s the same example with done as a static field:

using System;
using System.Threading;

namespace ThreadTest4
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
                done = true;
                Console.WriteLine("Done");
            }
        }
    }
}
