using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest16
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            /*Use the WhenAll method to asynchronously wait on multiple asynchronous operations that are represented as tasks.
             * The method has multiple overloads that support a set of non-generic tasks or a non-uniform set of generic tasks
             * (for example, asynchronously waiting for multiple void-returning operations, or asynchronously waiting for multiple
             * value-returning methods where each value may have a different type) and to support a uniform set of generic tasks
             * (such as asynchronously waiting for multiple TResult-returning methods).
             * Let's say you want to send email messages to several customers. You can overlap sending the messages so you're not
             * waiting for one message to complete before sending the next. You can also find out when the send operations have
             * completed and whether any errors have occurred:
             * WhenAll方法 可用于异步等待多个表示为任务的异步操作。
             */

            // 使用Enumerable来生成int[]
            IEnumerable<int> numbers = Enumerable.Range(3, 1000 - 3);

            var parallelQuery =
              from n in numbers.AsParallel()
              where Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0)
              select n;

            int[] addrs = parallelQuery.ToArray();

            // whenAll
            IEnumerable<Task> asyncOps = from addr in addrs select SendMailAsync(addr.ToString());
            Task.Run(async () => await Task.WhenAll(asyncOps)).Wait();
            //Thread.Sleep(Timeout.Infinite);
            //Console.ReadKey();
        }

        /// <summary>
        /// 上面的代码没有显示处理可能发生的异常，而是通过对WhenAll生成的任务执行await传播异常，若要处理该异常，可使用下面的代码
        /// </summary>
        /// <param name="addrs"></param>
        /// <returns></returns>
        private static async Task SendMailsAsync(string[] addrs)
        {
            IEnumerable<Task> asyncOps = from addr in addrs select SendMailAsync(addr.ToString());
            try
            {
                await Task.WhenAll(asyncOps);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.InnerException.Message);
            }
        }

        /// <summary>
        /// 在这种情况下，如果任意异步操作失败，所有异常都会合并到AggregateException异常中，
        /// 此异常存储在whenAll方法返回的Task中，但是，仅通过await关键字传播其中的一个异常
        /// 如果想要检查所有异常，可以使用以下代码
        /// </summary>
        /// <param name="addrs"></param>
        /// <returns></returns>
        private static async Task SendMainsAsync2(string[] addrs)
        {
            Task[] asyncOps = (from addr in addrs select SendMailAsync(addr)).ToArray();
            try
            {
                await Task.WhenAll(asyncOps);
            }
            catch (Exception exc)
            {
                foreach (Task faulted in asyncOps.Where(t => t.IsFaulted))
                {
                    // .. work with faulted and faulted.Exception.
                    //处理失败任务
                }
            }
        }

        private static Task SendMailAsync(string addr)
        {
            return Task.Run(() => Console.WriteLine("发送 邮件给：" + addr));
        }
    }
}