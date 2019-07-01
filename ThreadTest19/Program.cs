///Asynchronous delegates
///ThreadPool.QueueUserWorkItem doesn’t provide an easy mechanism for getting return values back from a thread after it has finished executing.Asynchronous
///delegate invocations (asynchronous delegates for short) solve this, allowing any number of typed arguments to be passed in both directions. Furthermore, 
///unhandled exceptions on asynchronous delegates are conveniently rethrown on the original thread (or more accurately, the thread that calls EndInvoke), 
///and so they don’t need explicit handling.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest19
{
    class Program
    {

        /// <summary>
        /// Here’s how you start a worker task via an asynchronous delegate:

        //  Instantiate a delegate targeting the method you want to run in parallel (typically one of the predefined Func delegates).
        //Call BeginInvoke on the delegate, saving its IAsyncResult return value.

        //BeginInvoke returns immediately to the caller. You can then perform other activities while the pooled thread is working.
        //When you need the results, call EndInvoke on the delegate, passing in the saved IAsyncResult object.
        //In the following example, we use an asynchronous delegate invocation to execute concurrently with the main thread, a simple method that returns a string’s length:
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Func<string, int> method = Work;
            //IAsyncResult cookie = method.BeginInvoke("test", null, null);
            ///// 
            ///// ... here's where we can do other work in parallel...
            /////
            //int result = method.EndInvoke(cookie);


            /// 由于.netCore BeginInvoke不支持 , 那我们就用Task来写吧. ThreadTest20有.netFramework版本的BeginInvoke.
            Task<int> task = new Task<int>(() => Work("test"));
            task.Start();
            task.Wait();
            Console.WriteLine("String length is : " + task.Result);
        }

        static int Work(string s)
        {
            return s.Length;
        }
    }
}
