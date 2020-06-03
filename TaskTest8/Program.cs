using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest8
{
    internal class Program
    {
        /*
         * ***************
         * Asynchronous methods are not limited to just compute-bound or I/O-bound operations but may represent a mixture of the two.
         * In fact, multiple asynchronous operations are often combined into larger mixed operations. For example, the RenderAsync method
         * in a previous example performed a computationally intensive operation to render an image based on some input imageData. This imageData
         * could come from a web service that you asynchronously access:
         ****************
         */

        private static void Main(string[] args)
        {
        }

        public async Task<Bitmap> DownloadDataAndRenderImageAsync(CancellationToken cancellationToken)
        {
            var imageData = await DownloadImageDataAsync(cancellationToken);
            return await RenderAsync(imageData, cancellationToken);
        }

        private Task<Bitmap> RenderAsync(object imageData, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private Task<string> DownloadImageDataAsync(CancellationToken cancellationToken)
        {
            return (Task<string>)Task.Run(delegate { });
        }
    }
}