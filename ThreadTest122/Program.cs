using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest122
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var cancelSource = new CancellationTokenSource();
            CancellationToken token = cancelSource.Token;

            Task task = Task.Factory.StartNew(() =>
            {
                // do some stuff
                Console.WriteLine("Before cancel.");
                token.ThrowIfCancellationRequested(); // 检查取消请求
                Console.WriteLine("After cancel.");
                // do some stuff
            }, token);
            //Thread.Sleep(100);
            cancelSource.Cancel();

            // 要捕获取消的任务 , 需要catch AggregateException 然后检查他的内部异常;
            try
            {
                task.Wait();
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is OperationCanceledException)
                {
                    Console.WriteLine("Task canceled!");
                }
            }

            // Parallel带着cacel Token
            Task task0 = Task.Factory.StartNew(() =>
            {
                var query = Enumerable.Range(1, 10).AsParallel().WithCancellation(token).Select(i => i);
            });

            // 当Task1执行完毕, 或者异常退出 , Task2会立马执行
            Task task1 = Task.Factory.StartNew(() =>
            {
                Console.Write("antecedant..");
            });
            Task task2 = task1.ContinueWith(ant => Console.Write("..continuation"));
            task1.Wait();
            task2.Wait();

            // Continuations and Task<TResult>
            Task.Factory.StartNew<int>(() => 8)
                .ContinueWith(ant => ant.Result * 2)
                .ContinueWith(ant => Math.Sqrt(ant.Result))
                .ContinueWith(ant => Console.WriteLine(ant.Result)).Wait();

            // Continuations and exceptions
            Task task3 = Task.Factory.StartNew(() =>
            {
                throw null;
            });
            Task task4 = task3.ContinueWith(ant => Console.WriteLine(ant.Exception));
            task4.Wait();

            // 异常捕获后 , 重新抛出
            Task continuation = Task.Factory.StartNew(() =>
            {
                throw null;
            }).ContinueWith(ant =>
            {
                if (ant.Exception != null)
                {
                    throw ant.Exception;
                }
            });
            continuation.Wait(); // 异常现在重新抛出到调用者

            // 通过使用TaskContinuationOptions的选项 , 来指定后续使用的Task
            Task task5 = Task.Factory.StartNew(() => { throw null; });
            Task error = task5.ContinueWith(ant => Console.Write(ant.Exception),
                TaskContinuationOptions.OnlyOnFaulted);
            Task ok = task5.ContinueWith(ant => Console.Write("Success."),
                TaskContinuationOptions.NotOnFaulted);

            task5.Wait();

            // 以下方法吞噬了所有异常
            Task.Factory.StartNew(() => { throw null; }).IgnoreExceptions();

            // Continuations and child tasks 继续和子任务
            Console.WriteLine("Continuations and child tasks 继续和子任务");
            TaskCreationOptions atp = TaskCreationOptions.AttachedToParent;
            Task.Factory.StartNew(() =>
            {
                Task.Factory.StartNew(() => { throw null; }, atp);
                Task.Factory.StartNew(() => { throw null; }, atp);
                Task.Factory.StartNew(() => { throw null; }, atp);
            })
                .ContinueWith(p => Console.WriteLine(p.Exception),
                TaskContinuationOptions.OnlyOnFaulted);

            // 这种情况下 , 无论task6有没有抛出异常 , task7都会执行, 因为task7并没有做限制
            Task task6 = Task.Factory.StartNew(() => { });
            Task fault = task6.ContinueWith(ant => { Console.WriteLine("fault"); },
                TaskContinuationOptions.OnlyOnFaulted);
            Task task7 = fault.ContinueWith(ant => Console.WriteLine("task7"));
        }
    }

    public static class ExtendTask
    {
        public static void IgnoreExceptions(this Task task)
        {
            task.ContinueWith(t => { var ignore = t.Exception; },
              TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}