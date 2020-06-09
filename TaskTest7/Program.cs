using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest7
{
    internal class Program
    {
        /*To do this, you can use a source with a dummy TResult (Boolean is a good default choice,
         * but if you're concerned about the user of the Task downcasting it to a Task<TResult>,
         * you can use a private TResult type instead).*/

        private static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);
            Delay(5000).Wait();
            Console.WriteLine(DateTime.Now);
        }

        public static Task<bool> Delay(int millisecondsTimeout)
        {
            TaskCompletionSource<bool> tcs = null;
            Timer timer = null;

            timer = new Timer(delegate
            {
                timer.Dispose();
                tcs.TrySetResult(true);
            }, null, Timeout.Infinite, Timeout.Infinite);

            tcs = new TaskCompletionSource<bool>(timer);
            timer.Change(millisecondsTimeout, Timeout.Infinite);
            return tcs.Task;
        }
    }
}