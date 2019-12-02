using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest105
{
    internal class Program
    {
        /// <summary>
        /// Changing the degree of parallelism
        /// private You can private call WithDegreeOfParallelism private only once private within a PLINQ query.If you need to call it again, you must force merging and repartitioning of the query by calling AsParallel() again within the query:
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            "The Quick Brown Fox"
                .AsParallel().WithDegreeOfParallelism(2)
                .Where(c => !char.IsWhiteSpace(c))
                .AsParallel().WithDegreeOfParallelism(3)
                .Select(c => char.ToUpper(c));
        }
    }
}