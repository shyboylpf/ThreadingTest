using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest12
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // 取消异步操作
            // 取消一个异步操作
            string url = "baidu.com";
            var cts1 = new CancellationTokenSource();
            string result = await DownloadStringAsync(url, cts1.Token);
            cts1.Cancel();

            // 取消多个异步操作
            // 若要取消多个异步调用，可以将相同令牌传递给所有调用
            var cts2 = new CancellationTokenSource();
            string[] urls = new string[] { "baidu.com", "bing.com" };
            IList<string> resultBatch = await Task.WhenAll(from url1 in urls select DownloadStringAsync(url1, cts2.Token));
            // at some point later, potentially on another thread
            // ...
            cts2.Cancel();

            // 使用CancellationToken.None表示绝不会请求取消操作，这会让CancellationToken.CanBeCanceled属性返回false
            // 或者，将相同令牌传递给操作的选择性子集
            var cts3 = new CancellationTokenSource();
            string data = await DownloadStringAsync(url, cts3.Token);
            string outputPath = "D:\\tmp";
            await SaveToDiskAsync(outputPath, data, CancellationToken.None);
            cts3.Cancel();
        }

        private static Task SaveToDiskAsync(string outputPath, string data, CancellationToken none)
        {
            throw new NotImplementedException();
        }

        private static Task<string> DownloadStringAsync(string url, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                throw new TaskCanceledException();
            }
            throw new NotImplementedException();
        }
    }
}