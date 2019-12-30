using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest108
{
    /// <summary>
    /// ParallelEnumerable.Range is not simply a shortcut for calling Enumerable.Range(…).AsParallel(). It changes the performance of the query by activating range partitioning.
    ///
    // Strategy Element allocation Relative performance
    // Chunk partitioning Dynamic Average
    // Range partitioning Static  Poor to excellent
    // Hash partitioning Static  Poor
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            // 强制使用range partitioning
            ParallelEnumerable.Range(0, 999).Select(x => x > 100);

            // 强制使用chunk partitioning  使用Partitioner.Create来创建输入
            int[] numbers = { 3, 4, 5, 6, 7, 8, 9 };
            var parallelQuery =
                Partitioner.Create(numbers, true).AsParallel()
                .Where(x => x < 5);

            double sum = ParallelEnumerable.Range(1, 1000000).Sum(i => Math.Sqrt(i));

            Console.WriteLine(sum);
        }
    }
}