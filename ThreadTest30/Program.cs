using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/// <summary>
/// 信号 Semaphore
/// A semaphore is like a nightclub: it has a certain capacity, enforced by a bouncer. Once it’s full, no more people can enter, and a queue builds up outside. Then, for each person that leaves, one person enters from the head of the queue. The constructor requires a minimum of two arguments: the number of places currently available in the nightclub and the club’s total capacity.
/// 用于限制并发
/// </summary>
namespace ThreadTest30
{
    class Program
    {
        /// <summary>
        /// Semaphores can be useful in limiting concurrency — preventing too many threads from executing a particular piece of code at once. In the following example, five threads try to enter a nightclub that allows only three threads in at once:
        /// </summary>
        /// <param name="args"></param>
        /// 
        static SemaphoreSlim _sem = new SemaphoreSlim(3);
        static void Main(string[] args)
        {
            for(int i = 1; i <= 5; i++)
            {
                new Thread(Enter).Start(i);
            }
        }

        private static void Enter(object id)
        {
            Console.WriteLine(id + " wants to enter.");
            _sem.Wait();
            Console.WriteLine(id + " is in!");
            Thread.Sleep(1000 * (int)id);
            Console.WriteLine(id + " is leaving.");
            _sem.Release();
        }
    }
}
