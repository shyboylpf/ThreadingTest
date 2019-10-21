using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest90
{
    internal class Program
    {
        private readonly object _locker = new object();
        private bool _signal;

        private static void Main(string[] args)
        {
        }

        private void WaitOne()
        {
            lock (_locker)
            {
                while (!_signal) Monitor.Wait(_locker);
            }
        }

        private void Set()
        {
            lock (_locker)
            {
                _signal = true;
                Monitor.PulseAll(_locker);
            }
        }

        private void Reset()
        {
            lock (_locker)
            {
                _signal = false;
            }
        }
    }
}