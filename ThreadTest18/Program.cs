///Entering the Thread Pool Without TPL
//You can't use the Task Parallel Library if you're targeting an earlier version of the.NET Framework(prior to 4.0). Instead, you must use one of the 
//older constructs for entering the thread pool: ThreadPool.QueueUserWorkItem and asynchronous delegates.The difference between the two is that asynchronous
//delegates let you return data from the thread. Asynchronous delegates also marshal any exception back to the caller.

//QueueUserWorkItem
//To use QueueUserWorkItem, simply call this method with a delegate that you want to run on a pooled thread:

using System;
using System.Threading;

namespace ThreadTest18
{
    class Program
    {
        /// <summary>
        /// QueueUserWorkItem
        /// To use QueueUserWorkItem, simply call this method with a delegate that you want to run on a pooled thread:
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(Go);
            ThreadPool.QueueUserWorkItem(Go, 123);
            //Console.ReadLine();
        }

        static void Go(object data)
        {
            Console.WriteLine("Hello from the thread pool! " + data);
        }
    }
}
