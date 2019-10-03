using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Contexts;

namespace ThreadTest54
{

    [Synchronization]
    public class AutoLock : ContextBoundObject
    {
        public void Demo()
        {
            Console.Write("Start...");
            Thread.Sleep(1000);           // We can't be preempted here
            Console.WriteLine("end");     // thanks to automatic locking!
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            AutoLock safeInstance = new AutoLock();
            new Thread(safeInstance.Demo).Start();      // call the demo method
            new Thread(safeInstance.Demo).Start();      // three times
            safeInstance.Demo();                        // concurrently.
        }
    }
}
