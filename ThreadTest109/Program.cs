using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest109
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // 使用Aggregate来并行计算
            // 聚合的优点恰恰是可以使用PLINQ声明性地并行化大型聚合或复杂聚合。
            int[] numbers = { 2, 3, 4 };
            int sum = numbers.Aggregate(0, (total, n) => total + n); // 0+2+3+4
            Console.WriteLine(sum);

            // 省略种子参数
            numbers = new int[] { 1, 2, 3 };
            sum = numbers.Aggregate((total, n) => total + n); // 1+2+3
            Console.WriteLine(sum);

            int x = numbers.Aggregate(0, (prod, n) => prod * n); // 0*1*2*3
            Console.WriteLine(x);
            int y = numbers.Aggregate((prod, n) => prod * n);  // 1*2*3
            Console.WriteLine(y);

            // 这个函数有坑, 不符合直觉
            numbers = new int[] { 2, 3, 4 };
            sum = numbers.Aggregate((total, n) => total + n * n); // 2 + 3*3 + 4*4
            Console.WriteLine(sum);

            // 我们可以这样修复
            numbers = new int[] { 0, 2, 3, 4 };
            sum = numbers.Aggregate((total, n) => total + n * n); // 0 + 2*2 + 3*3 + 4*4
            Console.WriteLine(sum);

            // 重组查询，以使聚合函数可交换和关联：
            numbers = new int[] { 2, 3, 4 };
            sum = numbers.Select(n => n * n).Aggregate((total, n) => total + n);
            Console.WriteLine(sum);

            // 这种简单场景应该用Sum函数, 而不是用Aggregate
            sum = numbers.Sum(n => n * n);
            Console.WriteLine("Sum: " + sum);

            numbers = new int[] { 8, 9, 10 };
            Console.WriteLine("Sqrt(average): " + Math.Sqrt(numbers.Average(n => n * n)));

            // 计算标准差  standard deviation:
            numbers = new int[] { 9, 9, 9 };
            double mean = numbers.Average();
            double sdev = Math.Sqrt(numbers.Average(n =>
            {
                double dif = n - mean;
                return dif * dif;
            }));
            Console.WriteLine("sdev: " + sdev);
        }
    }
}