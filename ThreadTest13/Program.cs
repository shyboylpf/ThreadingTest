///Exception Handling
///Any try/catch/finally blocks in scope when a thread is created are of no relevance to the thread when it starts executing.
///Consider the following program:

using System;
using System.Threading;

namespace ThreadTest13
{
    class Program
    {
        static void Main(string[] args)
        {
            ///The try/catch statement in this example is ineffective, and the newly created thread will be encumbered with an unhandled 
            ///NullReferenceException. This behavior makes sense when you consider that each thread has an independent execution path.
            try
            {
                new Thread(Go).Start();
            }
            catch (Exception ex)
            {
                // We'll never get here!
                Console.WriteLine("Exception!");
            }
        }

        static void Go() { throw null; }   // Throws a NullReferenceException
    }
}
