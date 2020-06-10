using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest11
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Start");
            Task.Run(async () => await task().ConfigureAwait(continueOnCapturedContext: false));
            Console.WriteLine("End");
            Thread.Sleep(Timeout.Infinite);
        }

        private static Task task()
        {
            return Task.Run(delegate
            {
                Console.WriteLine("1");
            });
        }
    }
}