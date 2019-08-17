using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/// <summary>
/// Mutex 互斥锁
/// A Mutex is like a C# lock, but it can work across multiple processes. In other words, Mutex can be computer-wide as well as application-wide.
/// Acquiring and releasing an uncontended Mutex takes a few microseconds — about 50 times slower than a lock.Acquiring and releasing an uncontended Mutex takes a few microseconds — about 50 times slower than a lock.
/// 比较慢
/// With a Mutex class, you call the WaitOne method to lock and ReleaseMutex to unlock. Closing or disposing a Mutex automatically releases it. Just as with the lock statement, a Mutex can be released only from the same thread that obtained it.
/// 
/// </summary>
namespace ThreadTest29
{
    //A common use for a cross-process Mutex is to ensure that only one instance of a program can run at a time. Here’s how it’s done:
    class Program
    {
        static void Main(string[] args)
        {
            // Naming a Mutex makes it available computer-wide. Use a name that's
            // unique to your company and application (e.g., include your URL).

            using(var mutex = new Mutex(false, "oreilly.com OneAtATimeDemo"))
            {
                // Wait a few seconds if contended, in case another instance
                // of the program is still in the process of shutting down.
                if(!mutex.WaitOne(TimeSpan.FromSeconds(3), false))
                {
                    Console.WriteLine("Another app instance is running. Bye!");
                    return;
                }
                RunProgram();
            }
        }

        private static void RunProgram()
        {
            Console.WriteLine("Running. Press Enter to exit");
            Console.ReadLine();
        }
    }
    //If running under Terminal Services, a computer-wide Mutex is ordinarily visible only to applications in the same terminal server session. To make it visible to all terminal server sessions, prefix its name with Global\.
}
