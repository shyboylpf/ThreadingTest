using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTest22
{
    internal class Program
    {
        /// <summary>
        ///
        /// Task.Delay You can use the Task.Delay method to introduce pauses into an
        /// asynchronous method's execution. This is useful for many kinds of functionality,
        /// including building polling loops and delaying the handling of user input for a
        /// predetermined period of time. The Task.Delay method can also be useful in combination
        /// with Task.WhenAny for implementing time-outs on awaits.

        /// If a task that's part of a larger asynchronous operation (for example, an ASP.NET web
        /// service) takes too long to complete, the overall operation could suffer, especially if
        /// it fails to ever complete. For this reason, it's important to be able to time out when
        /// waiting on an asynchronous operation.The synchronous Task.Wait, Task.WaitAll, and
        /// Task.WaitAny methods accept time-out values, but the corresponding
        /// TaskFactory.ContinueWhenAll/Task.WhenAny and the previously mentioned
        /// Task.WhenAll/Task.WhenAny methods do not.Instead, you can use Task.Delay and
        /// Task.WhenAny in combination to implement a time-out.

        /// For example, in your UI application, let's say that you want to download an image and
        /// disable the UI while the image is downloading. However, if the download takes too long,
        /// you want to re-enable the UI and discard the download:
        ///
        /// 您可以使用Task.Delay方法将暂停引入异步方法的执行中。这对于许多功能很有用，包括构建轮询循环以及将用户输入的处理延迟预定时间段。
        /// Task.Delay方法也可以与Task.WhenAny结合使用，以实现等待的超时。
        ///
        /// 如果作为较大的异步操作一部分的任务（例如，ASP.NET Web服务）花费的时间太长，则整个操作可能会受到影响，尤其是在无法完成的情况下。
        /// 因此，在等待异步操作时超时是很重要的。同步Task.Wait，Task.WaitAll和Task.WaitAny方法接受超时值，但是相应的TaskFactory.ContinueWhenAll / Task.WhenAny
        /// 和前面提到的Task.WhenAll / Task.WhenAny方法不接受。而是可以结合使用Task.Delay和Task.WhenAny来实现超时。
        /// </summary>
        /// <param name="args"></param>

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// 单次任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void btnDownload_Click(object sender, EventArgs e)
        {
            string url = "";
            //btnDownload.Enabled = false;
            try
            {
                Task<Bitmap> download = GetBitmapAsync(url);
                if (download == await Task.WhenAny(download, Task.Delay(3000)))
                {
                    Bitmap bmp = await download;
                    //pictureBox.Image = bmp;
                    //status.Text = "Downloaded";
                }
                else
                {
                    //pictureBox.Image = null;
                    //status.Text = "Timed out";
                    var ignored = download.ContinueWith(
                        t => Trace("Task finally completed"));
                }
            }
            finally
            {
                //btnDownload.Enabled = true;
            }
        }

        /// <summary>
        /// 多次任务
        /// </summary>
        public async void btnDownload_Click2(object sender, EventArgs e)
        {
            object[] urls = new object[1] { null };
            //btnDownload.Enabled = false;
            try
            {
                Task<Bitmap[]> downloads =
                    Task.WhenAll(from url in urls select GetBitmapAsync(url));
                if (downloads == await Task.WhenAny(downloads, Task.Delay(3000)))
                {
                    foreach (var bmp in downloads.Result) panel.AddImage(bmp);
                    status.Text = "Downloaded";
                }
                else
                {
                    status.Text = "Timed out";
                    downloads.ContinueWith(t => Log(t));
                }
            }
            finally { btnDownload.Enabled = true; }
        }

        private void Trace(string v)
        {
            throw new NotImplementedException();
        }

        private Task<Bitmap> GetBitmapAsync(object url)
        {
            throw new NotImplementedException();
        }
    }
}