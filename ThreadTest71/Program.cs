using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest71
{
    class Program
    {
        static void Main(string[] args)
        {
            ///使用Func<T,TResult>和Action<T>,Action而不使用Delegate其实都是为了简化代码,使用更少的代码达到相同的效果，不需要我们显示的声明一个委托。
            /// Func<T,TResult>的最后一个参数始终是返回类型，而Action<T>是没有返回类型的，而Action是没有返回类型和参数输入的.

            // func : int是参数 , bool是返回值
            Func<int, bool> myFunc = null;
            // //给委托封装方法的地方 使用了Lambda表达式
            myFunc = x => CheckIsInt32(x);
            // //调用委托
            bool ok = myFunc(5);
            Console.WriteLine("Func<int, bool>: " + ok);

            // Action: 没有返回值
            Action action = null;
            action += CheckIsVoid;
            action += CheckIsVoid2;
            action();

            // Action 带一个参数
            Action<int> action2 = null;
            action2 += Print;
            action2(2);

            // Task
            test();
            log("Main: 调用test之后");
            Thread.Sleep(Timeout.Infinite);
        }

        private static void log(string v)
        {
            Console.WriteLine("线程{0}: {1}", Thread.CurrentThread.ManagedThreadId, v);
        }

        private static async void test()
        {
            log("test: awit之前");
            log("doo的Task的结果:" + await doo());
            log("test: awit 之后");
        }

        // 返回Task的async方法, 一个标准的带返回值的异步task任务方法
        private static async Task<int> doo()
        {
            // async中使用await就是异步中以同步方式执行Task任务的方法, task任务一个接一个执行.
            var res1 = await Task.Run(() =>
            {
                Thread.Sleep(1000);
                log("Awaited Task1 执行");
                return 1;
            });
            var res2 = await Task.Run(() =>
            {
                Thread.Sleep(1000);
                log("Awaited Task2 执行");
                return 2;
            });
            var res3 = await Task.Run(() =>
            {
                Thread.Sleep(1000);
                log("Awaited Task3 执行");
                return 3;
            });

            // 不适用await: task多线程, 当前task不会等这个执行完, 因为不是await, 只是又开启了一个任务
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                log("Task.Run: Task多线程执行.");
            });

            return res1 + res2 + res3;

        }

        private static void Print(int obj)
        {
            Console.WriteLine($"Action带一个参数, 参数为: {obj}");
        }

        private static void CheckIsVoid()
        {
            Console.WriteLine("Action: 没有传入参数, 也没有返回值");
        }
        private static void CheckIsVoid2()
        {
            Console.WriteLine("Action2: 没有传入参数, 也没有返回值");
        }

        /// <summary>
        /// //被封装的方法
        /// </summary>
        /// <param name="pars"></param>
        /// <returns></returns>
        private static bool CheckIsInt32(int pars) 
        {
            return pars == 5;
        }
    }
}
