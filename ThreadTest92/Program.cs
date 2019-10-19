using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest92
{
    internal class Rendezvous
    {
        private static object _locker = new object();

        private static Countdown _countdown = new Countdown(2);

        private static void Main(string[] args)
        {
            Random r = new Random();
            new Thread(Mate).Start(r.Next(10000));
            Thread.Sleep(r.Next(10000));

            _countdown.Signal();
            _countdown.Wait();

            Console.Write("Mate! ");
        }

        private static void Mate(object delay)
        {
            Thread.Sleep((int)delay);

            _countdown.Signal();
            _countdown.Wait();

            Console.Write("Mate! ");
        }
    }

    public class Countdown
    {
        private object _locker = new object();
        private int _value;

        public Countdown()
        {
        }

        public Countdown(int initialCount)
        {
            _value = initialCount;
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
                if (_value <= 0) Monitor.PulseAll(_locker);
            }
        }

        public void Wait()
        {
            lock (_locker)
                while (_value > 0)
                    Monitor.Wait(_locker);
        }
    }
}