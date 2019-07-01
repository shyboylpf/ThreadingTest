///Asynchronous delegates
///ThreadPool.QueueUserWorkItem doesn’t provide an easy mechanism for getting return values back from a thread after it has finished executing.Asynchronous
///delegate invocations (asynchronous delegates for short) solve this, allowing any number of typed arguments to be passed in both directions. Furthermore, 
///unhandled exceptions on asynchronous delegates are conveniently rethrown on the original thread (or more accurately, the thread that calls EndInvoke), 
///and so they don’t need explicit handling.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreadTest20
{
    class Program
    {
        /// <summary>
        /// 书接上回, ThreadTest19.netCore竟然不支持这样的操作
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Func<string, int> method = Work;
            IAsyncResult cookie = method.BeginInvoke("test", null, null);
            int result = method.EndInvoke(cookie);
            Console.WriteLine("String length is : " + result);
        }
        static int Work(string s)
        {
            //throw null;
            return s.Length;
        }
        ///EndInvoke does three things. First, it waits for the asynchronous delegate to finish executing, if it hasn’t already. Second,
        ///it receives the return value (as well as any ref or out parameters). 
        ///Third, it throws any unhandled worker exception back to the calling thread.
    }
}
