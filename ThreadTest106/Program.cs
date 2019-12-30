using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest106
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IEnumerable<int> million = Enumerable.Range(3, 1000000);

            var cacelSource = new CancellationTokenSource();
            var primeNumberQuery =
                from n in million.AsParallel().WithCancellation(cacelSource.Token)
                where Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0)
                select n;

            new Thread(() =>
            {
                Thread.Sleep(100);
                cacelSource.Cancel();
            }).Start();

            try
            {
                int[] primes = primeNumberQuery.ToArray();
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Query canceled");
            }
        }
    }
}