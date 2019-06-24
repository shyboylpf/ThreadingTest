using System;
using System.Threading;

namespace ThreadTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(Go).Start();     // Call Go() on a new thread
            Go();                       // Call Go() on the main thread
        }

        static void Go()
        {
            for (int cycles = 0; cycles < 5; cycles++) Console.Write('?');
        }
    }
}
