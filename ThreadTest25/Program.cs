using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
        ///Only one thread can lock the synchronizing object (in this case, _locker) at a time, and any contending threads are blocked until the lock is released. If more than one thread contends the lock, they are queued on a “ready queue” and granted the lock on a first-come, first-served basis (a caveat is that nuances in the behavior of Windows and the CLR mean that the fairness of the queue can sometimes be violated). Exclusive locks are sometimes said to enforce serialized access to whatever’s protected by the lock, because one thread’s access cannot overlap with that of another. In this case, we’re protecting the logic inside the Go method, as well as the fields _val1 and _val2.
        ///


        ///C#’s lock statement is in fact a syntactic shortcut for a call to the methods Monitor.Enter and Monitor.Exit, with a try/finally block. Here’s (a simplified version of) what’s actually happening within the Go method of the preceding example:
        static void GoSafeMonitor()
        {
            Monitor.Enter(_locker);
            try
            {
                if (_val2 != 0) Console.WriteLine(_val1/_val2);
                _val2 = 0;
            }
            finally
            {
                Monitor.Exit(_locker);
            }
            ///Calling Monitor.Exit without first calling Monitor.Enter on the same object throws an exception.
            ///
        }

        /// <summary>
        /// 有时候会在Monitor.Exit之前发生异常 , 这样 , 这个锁就永远都不能释放了, 所以CLR4.0 增加了Moniter.Enter的如下重写
        /// </summary>
        static void GoSafeLockTaken()
        {
            bool lockTaken = false;
            try
            {
                Monitor.Enter(_locker, ref lockTaken);
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(_locker);
                }
            }
        }

        /// <summary>
        /// 尝试获取锁 , 拿到锁就返回true , 拿不到就返回false;
        /// </summary>
        static void GoSafeTryEnter()
        {
            Monitor.TryEnter(_locker);
        }
        
    }
}
