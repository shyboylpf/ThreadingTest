using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest21
{
    internal class Program
    {
        /// <summary>
        /// Early Bailout Consider that you're waiting asynchronously for an operation to complete
        /// while simultaneously responding to a user's cancellation request (for example, the user
        /// clicked a cancel button). The following code illustrates this scenario:
        ///
        /// 在执行等待的同时，响应用户的取消请求。
        /// </summary>
        /// <param name="args"></param>

        private CancellationTokenSource m_cts;

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            if (m_cts != null)
            {
                m_cts.Cancel();
            }
        }

        public async void btnRun_Click(object sender, EventArgs e)
        {
            m_cts = new CancellationTokenSource();
            //btnRun.Enabled = false;
            try
            {
                Task<Bitmap> imageDownload = GetBitmapAsync("http://a.com/a.jpg");
                await UntilCompletionOrCancellation(imageDownload, m_cts.Token);
                if (imageDownload.IsCompleted)
                {
                    Bitmap bitmap = await imageDownload;
                }
                else
                {
                    imageDownload.ContinueWith(t => Console.WriteLine(t));
                }
            }
            finally
            {
                //btnRun_Click.Enabled = true;
            }
        }

        private static async Task<Task> UntilCompletionOrCancellation(Task asyncOp, CancellationToken ct)
        {
            var tcs = new TaskCompletionSource<bool>();
            using (ct.Register(() => tcs.TrySetResult(true)))
                await Task.WhenAny(asyncOp, tcs.Task);
            return asyncOp;
        }

        private Task<Bitmap> GetBitmapAsync(string v, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This implementation re-enables the user interface as soon as you decide to bail out, but
        /// doesn't cancel the underlying asynchronous operations. Another alternative would be to
        /// cancel the pending operations when you decide to bail out, but not reestablish the user
        /// interface until the operations complete, potentially due to ending early due to the
        /// cancellation request:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void btnRun_Click2(object sender, EventArgs e)
        {
            m_cts = new CancellationTokenSource();

            //btnRun.Enabled = false;
            try
            {
                Task<Bitmap> imageDownload = GetBitmapAsync("", m_cts.Token);
                await UntilCompletionOrCancellation(imageDownload, m_cts.Token);
                Bitmap image = await imageDownload;
                //panel.AddImage(image);
            }
            catch (OperationCanceledException) { }
            finally
            {
                //btnRun.Enabled = true;
            }
        }
    }
}