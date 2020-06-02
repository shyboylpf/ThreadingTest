using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest6
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("开始" + DateTime.Now);
            DateTimeOffset a = Delay(5000).Result;

            Console.WriteLine("offset" + a.Millisecond);
            Console.WriteLine("结束" + DateTime.Now);

            Poll(new Uri("http://w.b"), new CancellationToken(), new Progress());
            Thread.Sleep(Timeout.Infinite);
        }

        /*假设您要创建一个任务，该任务将在指定的时间段后完成。*/

        internal static Task<DateTimeOffset> Delay(int millisecondsTimeout)
        {
            TaskCompletionSource<DateTimeOffset> tcs = null;
            Timer timer = null;
            timer = new Timer(delegate
            {
                timer.Dispose();
                tcs.TrySetResult(DateTimeOffset.UtcNow);
            }, null, Timeout.Infinite, Timeout.Infinite);

            tcs = new TaskCompletionSource<DateTimeOffset>(timer);
            timer.Change(millisecondsTimeout, Timeout.Infinite);
            return tcs.Task;
        }

        /*Starting with the .NET Framework 4.5, the Task.Delay method is provided for this purpose,
         * and you can use it inside another asynchronous method, for example, to implement an asynchronous polling loop:
         * 出于延时的目的，Framework4.5出了Task.Delay()函数
         */

        public static async Task Poll(Uri url, CancellationToken cancellationToken,
            IProgress<bool> progress)
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
                bool success = false;
                try
                {
                    await DownloadStringAsync(url);
                    success = true;
                }
                catch { /*ignore errors*/}
                progress.Report(success);
            }
        }

        private static Task DownloadStringAsync(Uri url)
        {
            return Task.Run(() => { Thread.Sleep(1000); });
        }

        private class Progress : IProgress<bool>
        {
            public void Report(bool value)
            {
                Console.WriteLine(value.ToString());
            }
        }
    }
}