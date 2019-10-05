using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;

namespace ThreadTest58
{
    class Program
    {
        static BackgroundWorker _bw = new BackgroundWorker();
        static void Main(string[] args)
        {
            _bw.DoWork += bw_DoWork;
            _bw.RunWorkerAsync("Message to worker.");
            Console.ReadLine();
        }

        private static void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            // THis is called on the worker thread
            Console.WriteLine(e.Argument);      // Writes "Message to worker."
            // Perform time-consuming task...
        }
    }
}
