using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest111
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Parallel.Invoke(
                () => new WebClient().DownloadFile("http://www.linqpad.net", "lp.html"),
                () => new WebClient().DownloadFile("http://www.jaoo.dk", "jaoo.html"));

            // 并行操作List会线程不安全 , 最好用ConcurrentBag
            var data = new List<string>();
            Parallel.Invoke(
                () => data.Add(new WebClient().DownloadString("http://www.linqpad.net")),
                () => data.Add(new WebClient().DownloadString("http://www.jaoo.dk"))
                );

            ParallelOptions parallelOptions = new ParallelOptions();
            CancellationToken cancellation = new CancellationToken();
            parallelOptions.MaxDegreeOfParallelism = 3;
            parallelOptions.CancellationToken = cancellation;
        }
    }
}