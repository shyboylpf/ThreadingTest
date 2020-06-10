using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest10
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Task.Run(async delegate
            {
                for (int i = 0; i < int.MaxValue; i++)
                {
                    await Task.Yield(); // fork the continuation into a separate work item.
                }
            });
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            //Task.Delay(TimeSpan.FromSeconds(1000));
        }
    }
}