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
    }
}
