using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest91
{
    /// <summary>
    /// 用Monitor.Wait/PulseAll 来实现CountdownEvent
    /// </summary>
    public class Countdown
    {
        private object _locker = new object();
        private int _value;

        private static void Main(string[] args)
        {
        }

        public Countdown()
        {
        }

        public Countdown(int initalCount)
        {
            _value = initalCount;
        }

        public void Signal()
        {
            AddCount(-1);
        }

        public void AddCount(int amount)
        {
            lock (_locker)
            {
                _value += amount;
                if (_value <= 0)
                {
                    Monitor.PulseAll(_locker);
                }
            }
        }

        public void Wait()
        {
            lock (_locker)
            {
                while (_value > 0)
                {
                    Monitor.Wait(_locker);
                }
            }
        }
    }
}