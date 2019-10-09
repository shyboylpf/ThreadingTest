using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest73
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("主线程测试开始..");
            AsyncMethod();
            Thread.Sleep(1000);
            Console.WriteLine("主线程测试结束..");
            Console.ReadLine();
        }

        private static async void AsyncMethod()
        {
            Console.WriteLine("开始异步代码");
            var result = await MyMethod();
            Console.WriteLine("异步代码执行完毕" + result.ToString());
        }

        private static async Task<int> MyMethod()
        {
            for(int i = 0; i < 5; i++)
            {
                Console.WriteLine($"异步执行{i.ToString()}..");
                await Task.Delay(1000); // 模拟耗时操作
            }
            return 1000;
        }
        // 只有拥有async才能在其内部使用await关键字。异步方法可以具有Task、Task<>或void的返回类型；
        // await关键字则是用于返回值是“可等待”类型(awaitable)的方法
    }
}
