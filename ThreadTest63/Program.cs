using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest63
{
    class Program
    {
        static void Main(string[] args)
        {
            var cancelSource = new CancellationTokenSource();
            new Thread(() => Work(cancelSource.Token)).Start();
            Thread.Sleep(5000);
            cancelSource.Cancel();
        }

        private static void Work(CancellationToken cancelToken)
        {
            while (true)
            {
                cancelToken.ThrowIfCancellationRequested();
            }
        }
        // The CancellationToken struct provides two additional useful members. The first is WaitHandle, which returns a wait handle that’s signaled when the token is canceled. The second is Register, which lets you register a callback delegate that will be fired upon cancellation.
        // Most of these classes’ use of cancellation tokens is in their Wait methods. For example, if you Wait on a ManualResetEventSlim and specify a cancellation token, another thread can Cancel its wait. This is much tidier and safer than calling Interrupt on the blocked thread.
    }
}
