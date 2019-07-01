
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreadTest21
{
    class Program
    {
        /// <summary>
        /// You can also specify a callback delegate when calling BeginInvoke — a method accepting an IAsyncResult object that’s automatically called upon 
        /// completion. This allows the instigating thread to “forget” about the asynchronous delegate, but it requires a bit of extra work at the callback end:
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Func<string, int> method = Work;
            method.BeginInvoke("test", Done, method);
        }

        static int Work(string s) { return s.Length; }

        static void Done(IAsyncResult cookie)
        {
            var target = (Func<string, int>)cookie.AsyncState;
            int result = target.EndInvoke(cookie);
            Console.WriteLine("String length is: " + result);
        }
        /// The final argument to BeginInvoke is a user state object that populates the AsyncState property of IAsyncResult. 
        /// It can contain anything you like; in this case, we’re using it to pass the method delegate to the completion callback, 
        /// so we can call EndInvoke on it.
    }
}
