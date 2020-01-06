using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest120
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // TaskCreationOptions
            // LongRunning 建议独占线程
            // PreferFairness 建议按照调用顺序来调用任务
            // AttachedToParent 指定调用间的父子关系

            Task parent = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("I am a parent.");

                Task.Factory.StartNew(() =>     // Detached Task
                {
                    Console.WriteLine("I am detached.");
                });

                Task.Factory.StartNew(() =>     // Child task
                {
                    Console.WriteLine("I am a child.");
                }, TaskCreationOptions.AttachedToParent);
            });

            // 等待任务完成的方法
            // Wait
            // Task.Result

            // Task.WaitAny 等效于每个任务完成就发出一个 ManualResetEventSlim的signaled
            // Task.WaitAll 会等待所有Task返回 , 如果task抛出异常 , 那么他会把异常整合到一起 , 并抛出一个 AggregateException
        }
    }
}