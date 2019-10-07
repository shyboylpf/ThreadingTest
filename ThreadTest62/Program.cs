using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest62
{
    class Program
    {
        static void Main(string[] args)
        {
            var canceler = new RulyCanceler();
            new Thread(()=> {
                try
                {
                    Work(canceler);
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Canceled!");
                }
            }).Start();
            Thread.Sleep(5000);
            canceler.Cancel();  // cancel掉操作.
        }

        private static void Work(RulyCanceler canceler)
        {
            while (true)
            {
                canceler.ThrowIfCancellationRequested(); // 检查CancellationRequest是否为true
                try
                {
                    OtherMethod(canceler);
                }
                finally
                {
                    // any required cleanup.
                }
            }
        }

        private static void OtherMethod(RulyCanceler canceler)
        {
            // Do stuff...
            canceler.ThrowIfCancellationRequested();
        }
    }

    class RulyCanceler
    {
        object _cancelLokcer = new object();
        bool _cancelRequest;
        public bool IsCancellationRequested
        {
            get
            {
                lock (_cancelLokcer)
                {
                    return _cancelRequest;
                }
            }
        }

        public void Cancel()
        {
            lock (_cancelLokcer)
            {
                _cancelRequest = true;
            }
        }
        public void ThrowIfCancellationRequested()
        {
            if (IsCancellationRequested)
            {
                throw new OperationCanceledException();
                // OperationCanceledException is a Framework type intended for just this purpose. Any exception class will work just as well, though.
            }
        }
    }
}
