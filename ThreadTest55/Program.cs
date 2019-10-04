using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Contexts;

namespace ThreadTest55
{
    [Synchronization]
    public class AutoLock : ContextBoundObject
    {
        public void Demo()
        {
            Console.Write("Start...");
            Thread.Sleep(1000);
            Console.WriteLine("end");
        }

        public void Test()
        {
            new Thread(Demo).Start();
            new Thread(Demo).Start();
            new Thread(Demo).Start();
            Console.ReadLine();
        }

        public static void Main()
        {
            new AutoLock().Test();
        }
    }

    [Synchronization(SynchronizationAttribute.REQUIRES_NEW)]
    public class SynchronizedB : ContextBoundObject
    {

    }
}
