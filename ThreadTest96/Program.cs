using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest96
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Thread t = new Thread(delegate () { while (true) ; });
            t.Start();
            Thread.Sleep(1000);
            t.Abort();
        }
    }
}