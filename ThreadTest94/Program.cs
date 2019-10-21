using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest94
{
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