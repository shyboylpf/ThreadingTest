using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest47
{
    class TwoWaySignaling
    {
        static EventWaitHandle _ready = new AutoResetEvent(false);
        static EventWaitHandle _go = new AutoResetEvent(false);
        static readonly object _locker = new object();
        static string _message;

        static void Main(string[] args)
        {
            new Thread(Work).Start();

            _ready.WaitOne();       // First wait until worker is ready.
            lock (_locker)
            {
                _message = "ooo";
            }
            _go.Set();              // Tell worker to go.

            _ready.WaitOne();
            lock (_locker)
            {
                _message = "ahhh";  // Give the worker another message.
            }
            _go.Set();
            _ready.WaitOne();
            lock (_locker)
            {
                _message = null;
            }
            _go.Set();
        }

        private static void Work()
        {
            while (true)
            {
                _ready.Set();       // Indicate that we're ready.
                _go.WaitOne();      // Wait to be kicked off...
                lock (_locker)
                {
                    if (_message == null)
                    {
                        return;     // Gracefully Exit.
                    }
                    Console.WriteLine(_message);
                }
            }
        }
    }
    //Here, we’re using a null message to indicate that the worker should end. With threads that run indefinitely, it’s important to have an exit strategy!
}
