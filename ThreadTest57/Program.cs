using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace ThreadTest57
{
    class Program
    {
        static void Main(string[] args)
        {
            var wc = new WebClient();
            wc.DownloadStringCompleted += (sender, arg) =>
            {
                if (arg.Cancelled)
                    Console.WriteLine("Canceled");
                else if (arg.Error != null)
                    Console.WriteLine("Exception: " + arg.Error.Message);
                else
                {
                    Console.WriteLine(arg.Result.Length + " chars were downloaded");
                    // We could update the UI from here...
                }
            };
            wc.DownloadStringAsync(new Uri("http://www.linqpad.net"));  // Start it
        }
    }
}
