using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest94
{
    /// <summary>
    /// 升级锁与读锁差不多 , 但是系统中可以有多个读锁, 却同时只能取出一个升级锁, 通过序列化升级为写锁来避免死锁的发生
    /// 就像SQL server中升级锁那样.
    /// </summary>
    internal class Program
    {
        private static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();
        private static List<int> _items = new List<int>();
        private static Random _rand = new Random();

        private static void Main()
        {
            new Thread(Read).Start();
            new Thread(Read).Start();
            new Thread(Read).Start();

            new Thread(Write).Start("A");
            new Thread(Write).Start("B");
        }

        private static void Read()
        {
            while (true)
            {
                _rw.EnterReadLock();
                foreach (int i in _items) Thread.Sleep(10);
                _rw.ExitReadLock();
            }
        }

        private static void Write(object threadID)
        {
            while (true)
            {
                Console.WriteLine(_rw.CurrentReadCount + " concurrent readers");
                int newNumber = GetRandNum(100);
                _rw.EnterUpgradeableReadLock();
                if (!_items.Contains(newNumber))
                {
                    _rw.EnterWriteLock();
                    _items.Add(newNumber);
                    _rw.ExitWriteLock();
                }
                _rw.ExitUpgradeableReadLock();
                Console.WriteLine("Thread " + threadID + " added " + newNumber);
                Thread.Sleep(100);
            }
        }

        private static int GetRandNum(int max)
        { lock (_rand) return _rand.Next(max); }
    }
}