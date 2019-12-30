using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest115
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Parallel.ForEach("Hello, world", (c, loopState, index) =>
            {
                if (c == ',')
                {
                    //loopState.Break();
                    //loopState.Stop();
                    throw new Exception();
                }
                else
                {
                    Console.Write(c);
                    Console.WriteLine(loopState.IsExceptional);
                }
            });
        }
    }
}