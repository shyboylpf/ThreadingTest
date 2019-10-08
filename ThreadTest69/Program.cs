using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest69
{
    class Program
    {
        
        static void Main(string[] args)
        {
            for(int i = 0; i < 5; i++)
            {
                new Thread(localRand).Start();
            }
        }

        static void localRand()
        {
            ThreadLocal<Random> localRandom = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));
            Console.WriteLine("threadLocal: " + localRandom.Value.Next());
        }
    }
}
