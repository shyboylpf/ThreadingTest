using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;

namespace TaskTest20
{
    internal class Program
    {
        /// <summary>
        /// Throttling Consider the interleaving example, except that the user is downloading so
        /// many images that the downloads have to be throttled; for example, you want only a
        /// specific number of downloads to happen concurrently.To achieve this, you can start a
        /// subset of the asynchronous operations.As operations complete, you can start additional
        /// operations to take their place:
        /// 考虑交错的例子，除了用户下载的图像太多以至于必须限制下载。例如，您只希望同时进行特定数量的下载。
        /// 为此，您可以启动异步操作的子集。操作完成后，您可以开始其他操作以代替它们：
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            Func();
            Console.WriteLine("Hello World!");
        }

        private static async void Func()
        {
            const int CONCURRENCY_LEVEL = 4;
            Uri[] urls = new Uri[6] {
                new Uri("1"),
                new Uri("2"),
                new Uri("3"),
                new Uri("4"),
                new Uri("5"),
                new Uri("6"),
            };
            int nextIndex = 0;
            var imageTasks = new List<Task<Bitmap>>();
            while (nextIndex < CONCURRENCY_LEVEL && nextIndex < urls.Length)
            {
                imageTasks.Add(GetBitmapAsync(urls[nextIndex]));
                nextIndex++;
            }

            while (imageTasks.Count > 0)
            {
                try
                {
                    Task<Bitmap> imageTask = await Task.WhenAny(imageTasks);
                    imageTasks.Remove(imageTask);

                    Bitmap image = await imageTask;
                    //panel.AddImage(image);
                }
                catch (Exception exc)
                {
                    //Log(exc);
                }

                if (nextIndex < urls.Length)
                {
                    imageTasks.Add(GetBitmapAsync(urls[nextIndex]));
                    nextIndex++;
                }
            }
        }

        private static Task<Bitmap> GetBitmapAsync(Uri uri)
        {
            throw null;
        }
    }
}