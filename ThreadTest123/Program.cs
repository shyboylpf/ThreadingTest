using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest123
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Continuations with multiple antecedents
            // 前面两个任务都结束后 , 才会调用第三个
            Task task1 = Task.Factory.StartNew(() => Console.WriteLine("x"));
            Task task2 = Task.Factory.StartNew(() => Console.WriteLine("y"));

            var continuation = Task.Factory.ContinueWhenAll(
                new[] { task1, task2 }, tasks => Console.WriteLine("Done"));
            continuation.Wait();

            // ContinueWhenAll使用lambda表达式的真实应用
            Task<int> task3 = Task.Factory.StartNew(() => 123);
            Task<int> task4 = Task.Factory.StartNew(() => 456);

            Task<int> task5 = Task<int>.Factory.ContinueWhenAll(
                new[] { task3, task4 }, tasks => tasks.Sum(t => t.Result));

            Console.WriteLine(task5.Result); // 579

            // Multiple continuations on a single antecedent
            // 单个前提 多个延续
            var t1 = Task.Factory.StartNew(() => Thread.Sleep(1000));
            t1.ContinueWith(ant => Console.Write("x"));
            t1.ContinueWith(ant => Console.Write("y")).Wait();
        }
    }
}