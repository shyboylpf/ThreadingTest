using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/// <summary>
/// 锁和原子性
/// 
/// </summary>
namespace ThreadTest28
{
    class Program
    {
        static void Main1(string[] args)
        {
        }
        static readonly object _locker = new object();
        decimal _savingsBalance, _checkBalance;
        void Transfer(decimal amount)
        {
            lock (_locker)
            {
                _savingsBalance += amount;
                _checkBalance -= amount + GetBankFee();
            }
        }
        //If an exception was thrown by GetBankFee(), the bank would lose money. In this case, we could avoid the problem by calling GetBankFee earlier. A solution for more complex cases is to implement “rollback” logic within a catch or finally block.

        private decimal GetBankFee()
        {
            throw new NotImplementedException();
        }


        //多重lock(Nested Locking)
        void lock1()
        {
            lock (_locker)
            {
                lock (_locker)
                {
                    lock (_locker)
                    {
                        // Do something.
                    }
                }
            }
        }

        void lock2()
        {
            Monitor.Enter(_locker);
            Monitor.Enter(_locker);
            Monitor.Enter(_locker);
            // do something.
            Monitor.Exit(_locker);
            Monitor.Exit(_locker);
            Monitor.Exit(_locker);
        }

        // Nested locking is useful when one method calls another within a lock:
        static void Main(string[] args)
        {
            lock (_locker)
            {
                AnotherMethod();
            }
        }

        private static void AnotherMethod()
        {
            lock (_locker) { Console.WriteLine("Another method"); }
        }
        // A thread can block on only the first (outermost) lock.


        /// 死锁
        object locker1 = new object();
        object locker2 = new object();

        void lock3()
        {
            new Thread(() =>
            {
                lock (locker1)
                {
                    Thread.Sleep(1000);
                    lock (locker2) ;      // Deadlock
                }
            }).Start();
            lock (locker2)
            {
                Thread.Sleep(1000);
                lock (locker1) ;                          // Deadlock
            }
        }

        // 死锁是多线程编程最难的问题之一 , 特别是有多个相关object的时候 . 
    }
}
