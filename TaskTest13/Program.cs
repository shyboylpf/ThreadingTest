using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest13
{
    internal class Program
    {
        private object pbDwonloadProgress;

        private static void Main(string[] args)
        {
        }

        // 监视进度
        // 某些异步方法通过传入异步方法的进度接口来公开进度
        // WPF里会用
        private async void btnDownload_Click(object sender, RouteEventArgs e)
        {
            btnDownload.IsEnabled = false;
            try
            {
                txtResult.Text = await DownloadStringAsync(TxtUrl.Text,
                    new Progress<int>(p => pbDwonloadProgress.Value = p));
            }
            finally
            {
                btnDownload.IsEnabled = true;
            }
        }

        private Task DownloadStringAsync(object text, Progress<int> progress)
        {
            throw new NotImplementedException();
        }
    }

    internal class RouteEventArgs
    {
    }
}