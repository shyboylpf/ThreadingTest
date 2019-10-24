using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest95
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var rw = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            rw.EnterWriteLock();
            rw.EnterReadLock();
            Console.WriteLine(rw.IsReadLockHeld);
            Console.WriteLine(rw.IsWriteLockHeld);
            rw.ExitReadLock();
            rw.ExitWriteLock();
        }
    }
}