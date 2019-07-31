using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreadTest25
{
    class Program
    {
        static int _val1 = 1, _val2 = 1;
        static void Main(string[] args)
        {

        }


        /// <summary>
        /// This class is not thread-safe: if Go was called by two threads simultaneously, it would be possible to get a division-by-zero error, because _val2 could be set to zero in one thread right as the other thread was in between executing the if statement and Console.WriteLine.
        /// </summary>
        static void Go()
        {
            if (_val2 != 0)
            {
                Console.WriteLine(_val1 / _val2);
                _val2 = 0;
            }
        }

        /// Here's how lock can fix the problem:
        static readonly object _locker = new object();

        static void GoSafe()
        {
            lock (_locker)
            {
                if (_val2 != 0)
                {
                    Console.WriteLine(_val1 / _val2);
                }
                _val2 = 0;
            }
        }
    }
}
