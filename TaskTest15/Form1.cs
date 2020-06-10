using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskTest15
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = await Task.Run(() =>
            {
                // ... do compute-bound work here
                long answer = 0;
                for (int i = 0; i < int.MaxValue; i++)
                {
                    answer += i;
                }
                return answer.ToString();
            });
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = await Task.Run(async () =>
                {
                    return await DownloadFirstImageAsync();
                });
            //     await Task.Run(() =>
            //{
            //    return Image.FromFile("111.jpg");
            //});
        }

        private Task<Bitmap> DownloadSecondImageAsync()
        {
            throw new NotImplementedException();
        }

        private async Task<Image> DownloadFirstImageAsync()
        {
            using (WebClient client = new WebClient())
            {
                byte[] data = await client.DownloadDataTaskAsync("http://img.mingxing.com/upload/attach/2017/12-01/307135-jjYqz5.jpg");
                using (MemoryStream mem = new MemoryStream(data))
                {
                    return Image.FromStream(mem);
                }
            }
            //return await Task.Run(() =>
            //{
            //    return Image.FromFile("111.jpg");
            //});
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}