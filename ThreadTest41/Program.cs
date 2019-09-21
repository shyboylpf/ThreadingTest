using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest41
{
    class Program
    {
        /// The following program demonstrates ReaderWriterLockSlim. Three threads continually enumerate a list, while two further threads append a random number to the list every second. A read lock protects the list readers, and a write lock protects the list writers:
        static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();
        static List<int> _items = new List<int>();
        static Random _rand = new Random();

        /// <summary>
        /// Reader/Writer Locks.
        /// ReaderWriterLockSlim 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            new Thread(Read).Start("A");
            new Thread(Read).Start("B");
            new Thread(Read).Start("C");

            new Thread(Write).Start("A");
            new Thread(Write).Start("B");

        }

        private static void Read(object threadID)
        {
            while (true)
            {
                _rw.EnterReadLock();
                //foreach (int i in _items)
                //{
                //    //Console.WriteLine();
                //    Thread.Sleep(10);
                //}
                try
                {
                    Console.WriteLine("Thread " + threadID + " Read " + _items.Last());
                    Thread.Sleep(10);
                }
                catch
                {
                    Thread.SpinWait(10);
                }
                _rw.ExitReadLock();
            }
        }

        private static void Write(object threadID)
        {
            while (true)
            {
                int newNumber = GetRandNum(100);
                _rw.EnterWriteLock();
                _items.Add(newNumber);
                _rw.ExitWriteLock();
                //Console.WriteLine("Thread " + threadID + " added " + newNumber);
                Thread.Sleep(100);
            }
        }

        private static int GetRandNum(int max)
        {
            lock (_rand)
            {
                return _rand.Next(max);
            }
        }

        
    }
}
