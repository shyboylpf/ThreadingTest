using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest99
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IEnumerable<int> numbers = Enumerable.Range(3, 100 - 3);
            var parallelQuery = from n in numbers.AsParallel()
                                where Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0)
                                select n;
            int[] primes = parallelQuery.ToArray();

            foreach (var item in primes)
            {
                Console.WriteLine($"sqrt: {item}");
            }

            foreach (var item in "abcdef".AsParallel().Select(c => char.ToUpper(c)).ToArray())
            {
                Console.Write(item);
            }

            Parallel.For(0, 5, i => { Console.WriteLine("Step : " + i); });

            var numberss = numbers.AsParallel().Where(n => n > 90).Select(n => n * n).ToArray();
            foreach (var item in numberss)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("========AsOrdered=======");

            numberss = numbers.AsParallel().AsOrdered().Where(n => n > 90).Select(n => n * n).ToArray();
            foreach (var item in numberss)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("========AsUnOrdered=======");

            numberss = numbers.AsParallel().AsOrdered().Where(n => n > 90).AsUnordered().Select(n => n * n).ToArray();
            foreach (var item in numberss)
            {
                Console.WriteLine(item);
            }

            // 强制并行
            numbers.AsParallel().WithExecutionMode(ParallelExecutionMode.ForceParallelism);
        }
    }
}