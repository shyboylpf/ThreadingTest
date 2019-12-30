using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest110
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int[] numbers = { 1, 2, 3, 4 };

            int sum = numbers.AsParallel().Aggregate(
                () => 0,                                            // seedFatcory
                (localTotal, n) => localTotal + n,                  // updateAccumulatorFunc
                (mainTot, localTotal) => mainTot + localTotal,      // combineAccumulatorFunc
                finalResult => finalResult);                        // resultSelector

            Console.WriteLine(sum);

            // 统计26个字母出现的次数
            string text = "Let’s suppose this is a really long string";
            var letterFrequencies = new int[26];
            foreach (char c in text)
            {
                int index = char.ToUpper(c) - 'A';
                if (index >= 0 && index <= 26)
                {
                    letterFrequencies[index]++;
                }
            }
            foreach (int item in letterFrequencies)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine("\nAggregate:");
            int[] result = text.Aggregate(
                new int[26],        // 创建累加器
                (letterFrequencie, c) => // 将字母汇总去累加器
                {
                    int index = char.ToUpper(c) - 'A';
                    if (index >= 0 && index <= 26)
                    {
                        letterFrequencie[index]++;
                    }
                    return letterFrequencie;
                });
            foreach (int item in result)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine("\nAsParallel().Aggregate");
            int[] result2 =
                text.AsParallel().Aggregate(
                    () => new int[26],          // 创建新的本地累加器
                    (localFrequencies, c) =>        // 汇总到本地累加器
                    {
                        int index = char.ToUpper(c) - 'A';
                        if (index >= 0 && index <= 26)
                        {
                            localFrequencies[index]++;
                        }
                        return localFrequencies;
                    },
                    // Aggregate local->main 累加器
                    (mainFreq, localFreq) =>
                    mainFreq.Zip(localFreq, (f1, f2) => f1 + f2).ToArray(),

                    finalResult => finalResult
                    );
            foreach (int item in result2)
            {
                Console.Write(item + " ");
            }
        }
    }
}