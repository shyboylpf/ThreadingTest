using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest5
{
    internal class Program
    {
        private static void Main(string[] args)
        {
        }

        internal Task<Bitmap> RenderAsync(ImageData data, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var bmp = new Bitmap(10, 10);
                for (int y = 0; y < 10; y++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    for (int x = 0; x < 10; x++)
                    {
                        // render pixel [x,y] into bmp
                    }
                }
                return bmp;
            }, cancellationToken);
        }
    }

    //internal Task<Bitmap> RenderAsync(ImageData data, CancellationToken cancellationToken)
    //{
    //    return Task.Run(() =>
    //    {
    //        var bmp = new Bitmap(10, 10);
    //        for (int y = 0; y < 10; y++)
    //        {
    //            cancellationToken.ThrowIfCancellationRequested();
    //            for (int x = 0; x < 10; x++)
    //            {
    //                // render pixel[x,y]into bmp
    //            }
    //        }
    //        return bmp;
    //    }, cancellationToken);
    //}
}