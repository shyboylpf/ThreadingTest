using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest17
{
    internal class Program
    {
        private static void Main(string[] args)
        {
        }

        /// <summary>
        /// 以异步方式从Web下载多个文件的示例。
        /// 在此示例中，所有异步操作具有相同的结果类型，并很容易对结果进行访问
        /// </summary>
        /// <param name="urls"></param>
        private static async void FuncAsync(string[] urls)
        {
            string[] pages = await Task.WhenAll(
                from url in urls select DownloadStringAsync(url));
        }

        private static async void FuncExcAsync(string[] urls)
        {
            Task<string>[] asyncOps =
                (from url in urls select DownloadStringAsync(url)).ToArray();
            try
            {
                string[] pages = await Task.WhenAll(asyncOps);
            }
            catch (Exception exc)
            {
                foreach (Task<string> faulted in asyncOps.Where(t => t.IsFaulted))
                {
                    // work with faulted and faulted.Exception
                }
            }
        }

        private static Task<string> DownloadStringAsync(string url)
        {
            throw new NotImplementedException();
        }
    }
}