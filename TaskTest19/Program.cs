using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTest19
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Task.WhenAny 的 interleaving

            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// Consider a case where you're downloading images from the web and processing each image
        /// (for example, adding the image to a UI control). You have to do the processing
        /// sequentially on the UI thread, but you want to download the images as concurrently as
        /// possible. Also, you don’t want to hold up adding the images to the UI until they’re all
        /// downloaded—you want to add them as they complete:
        ///
        /// 考虑一种情况，您要从Web下载图像并处理每个图像（例如，将图像添加到UI控件）。您必须按顺序在UI线程上进行处理，
        /// 但是您希望尽可能同时下载图像。另外，您也不想在所有图像都下载完后就将它们添加到UI中了，而是想在完成时添加它们：
        /// </summary>
        private static async void Func()
        {
            string[] urls = new string[3] { "1", "1", "1" };
            List<Task<Bitmap>> imageTasks =
                (from imageUrl in urls select GetBitmapAsync(imageUrl)).ToList();
            while (imageTasks.Count > 0)
            {
                try
                {
                    Task<Bitmap> imageTask = await Task.WhenAny(imageTasks);
                    imageTasks.Remove(imageTask);

                    Bitmap image = await imageTask;
                    //panel.AddImage(image);
                }
                catch { }
            }
        }

        /// <summary>
        /// You can also apply interleaving to a scenario that involves computationally intensive
        /// processing on the ThreadPool of the downloaded images; for example:
        ///
        /// 您还可以将交织应用于涉及对下载的映像的ThreadPool进行计算密集型处理的方案。例如：
        /// </summary>
        private static async void FuncCompute()
        {
            string[] urls = new string[3] { "1", "1", "1" };
            List<Task<Bitmap>> imageTasks =
            (from imageUrl in urls
             select GetBitmapAsync(imageUrl)
                .ContinueWith(t => ConvertImage(t.Result))).ToList();
            while (imageTasks.Count > 0)
            {
                try
                {
                    Task<Bitmap> imageTask = await Task.WhenAny(imageTasks);
                    imageTasks.Remove(imageTask);

                    Bitmap image = await imageTask;
                    //panel.AddImage(image);
                }
                catch { }
            }
        }

        private static Bitmap ConvertImage(object result)
        {
            throw new NotImplementedException();
        }

        private static Task<Bitmap> GetBitmapAsync(string imageUrl)
        {
            throw new NotImplementedException();
        }
    }
}