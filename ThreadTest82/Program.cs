using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest82
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Queue the task.
            ThreadPool.QueueUserWorkItem(ThreadProc, new object());
            Console.WriteLine("Main thread does some work, then sleeps.");
            //Thread.Sleep(1000);

            Console.WriteLine("Main thread exits.");
        }

        // This thread procedure performs the task.
        private static void ThreadProc(object state)
        {
            // No state object was passed to QueueUserWorkItem, so stateInfo is null.
            Console.WriteLine("Hello from the thread pool.");
        }
    }

    //Examples of operations that use thread pool threads include the following:
    // When you create a Task or Task<TResult> object to perform some task asynchronously, by default the task is scheduled to run on a thread pool thread.
    // Asynchronous timers use the thread pool.Thread pool threads execute callbacks from the System.Threading.Timer internal class and raise events from the System.Timers.Timer internal class.
    // When you use registered wait handles, a system thread monitors the status of the wait handles.When a wait operation completes, a worker thread from the thread pool executes the corresponding callback function.
    // When you call the QueueUserWorkItem method to queue a method for execution on a thread pool thread. You do this by passing the method a WaitCallback internal delegate. The internal delegate has the signature
}