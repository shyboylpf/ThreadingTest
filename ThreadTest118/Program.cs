using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest118
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // 使用 Task.Factory.StartNew 来创建和调用一个Task, 传入一个Action的委托delegate
            Task.Factory.StartNew(() => Console.WriteLine("Hello from a task."));

            // Task的子类 Task<TResult> , 可以使您从Task获取一个返回值.
            Task<string> task = Task.Factory.StartNew<string>(() =>
            {
                using (var wc = new System.Net.WebClient())
                {
                    return wc.DownloadString("https://www.baidu.com/");
                }
            });
            // 并行的做一些其他事情
            // RunSomeOtherMethod();

            string result = task.Result;  // 等待Task完成, 并取回结果
            Console.WriteLine(result.Substring(0, 20));

            // 创建Task  , 然后调用
            var task2 = new Task(() => Console.WriteLine("Create a Task."));
            task2.Start();

            // 对当前Task进行同步调用
            task2.RunSynchronously();
        }
    }
}