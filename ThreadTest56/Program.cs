using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Contexts;

namespace ThreadTest56
{
    [Synchronization]
    public class Deadlock : ContextBoundObject
    {
        public Deadlock Other;
        public void Demo()
        {
            Thread.Sleep(1000);
            Other.Hello();
        }
        void Hello()
        {
            Console.WriteLine("Hello.");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Deadlock dead1 = new Deadlock();
            Deadlock dead2 = new Deadlock();
            dead1.Other = dead2;
            dead2.Other = dead1;
            new Thread(dead1.Demo).Start();
            dead2.Demo();
        }
    }
}
