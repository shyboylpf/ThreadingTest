using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest121
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int x = 0;
            Task<int> calc = Task.Factory.StartNew(() => 7 / x);
            try
            {
                Console.WriteLine(calc.Result);
            }
            catch (AggregateException aex)
            {
                Console.WriteLine(aex.InnerException.Message);
            }

            TaskCreationOptions atp = TaskCreationOptions.AttachedToParent;
            var parent = Task.Factory.StartNew(() =>
            {
                Task.Factory.StartNew(() =>
                {
                    Task.Factory.StartNew(() => { throw null; }, atp);
                }, atp);
            });

            parent.Wait();
        }
    }
}