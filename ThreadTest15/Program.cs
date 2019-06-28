///Thread Pooling
///Whenever you start a thread, a few hundred microseconds are spent organizing such things as a fresh private local variable stack.Each thread also 
///consumes(by default) around 1 MB of memory.The thread pool cuts these overheads by sharing and recycling threads, allowing multithreading to be applied 
///at a very granular level without a performance penalty.This is useful when leveraging multicore processors to execute computationally intensive code in 
///parallel in “divide-and-conquer” style.
///
///The thread pool also keeps a lid on the total number of worker threads it will run simultaneously. Too many active threads throttle the operating 
///system with administrative burden and render CPU caches ineffective. Once a limit is reached, jobs queue up and start only when another finishes.
///This makes arbitrarily concurrent applications possible, such as a web server. (The asynchronous method pattern is an advanced technique that takes 
///this further by making highly efficient use of the pooled threads; we describe this in Chapter 23 of C# 4.0 in a Nutshell).

///There are a number of ways to enter the thread pool:
///Via the Task Parallel Library(from Framework 4.0)
///By calling ThreadPool.QueueUserWorkItem
///Via asynchronous delegates
///Via BackgroundWorker
///

///The following constructs use the thread pool indirectly:

//WCF, Remoting, ASP.NET, and ASMX Web Services application servers
//System.Timers.Timer and System.Threading.Timer
//Framework methods that end in Async, such as those on WebClient(the event-based asynchronous pattern), and most BeginXXX methods(the asynchronous programming model pattern)
//PLINQ

///The Task Parallel Library (TPL) and PLINQ are sufficiently powerful and high-level that you’ll want to use them to assist in multithreading even when thread pooling is unimportant. We discuss these in detail in Part 5; right now, we'll look briefly at how you can use the Task class as a simple means of running a delegate on a pooled thread.
///

///There are a few things to be wary of when using pooled threads:

//You cannot set the Name of a pooled thread, making debugging more difficult(although you can attach a description when debugging in Visual Studio’s Threads window).
//Pooled threads are always background threads(this is usually not a problem).
//Blocking a pooled thread may trigger additional latency in the early life of an application unless you call ThreadPool.SetMinThreads(see Optimizing the Thread Pool).
//You are free to change the priority of a pooled thread — it will be restored to normal when released back to the pool.

///You can query if you’re currently executing on a pooled thread via the property Thread.CurrentThread.IsThreadPoolThread.



using System;
using System.Threading;

namespace ThreadTest15
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
