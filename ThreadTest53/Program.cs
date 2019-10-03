using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest53
{
    class Program
    {
        static Barrier _barrier = new Barrier(3, barrier => Console.WriteLine());
        static void Main(string[] args)
        {
            new Thread(Speak).Start();
            new Thread(Speak).Start();
            new Thread(Speak).Start();
        }

        private static void Speak()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Write(i + " ");
                _barrier.SignalAndWait();
            }
        }
    }
}
