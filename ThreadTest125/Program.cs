using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest125
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // 重写 Task.Factory, 来重写一些创建属性 和 后续属性
            var factory = new TaskFactory(TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent,
                TaskContinuationOptions.None);
            Task task1 = factory.StartNew(() => Console.WriteLine("1"));
            Task task2 = factory.StartNew(() => Console.WriteLine("2"));
            task2.Wait();

            // TaskCompletionSource
            var source = new TaskCompletionSource<int>();
            new Thread(() => { Thread.Sleep(5000); source.SetResult(123); })
                .Start();

            Task<int> task = source.Task; // 我们的Slave任务
            Console.WriteLine(task.Result);// 123
        }
    }
}