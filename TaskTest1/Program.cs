﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CancellationTokenSource ts = new CancellationTokenSource();
            Thread thread = new Thread(CancelToken);
            thread.Start(ts);

            Task t = Task.Run(() =>
            {
                Task.Delay(5000).Wait();
                Console.WriteLine("Task ended delay...");
            });
            try
            {
                Console.WriteLine("About to wait completion of task {0}", t.Id);
                bool result = t.Wait(1510, ts.Token);
                Console.WriteLine("Wait completed normally: {0}", result);
                Console.WriteLine("The task status:  {0:G}", t.Status);
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine("{0}: The wait has been canceled. Task status: {1:G}",
                                  e.GetType().Name, t.Status);
                Thread.Sleep(4000);
                Console.WriteLine("After sleeping, the task status:  {0:G}", t.Status);
                ts.Dispose();
            }
        }

        private static void CancelToken(object obj)
        {
            Thread.Sleep(1500);
            Console.WriteLine("Canceling the cancellation token from thread {0}...", Thread.CurrentThread.ManagedThreadId);
            CancellationTokenSource source = obj as CancellationTokenSource;
            if (source != null)
            {
                source.Cancel();
            }
        }
    }
}