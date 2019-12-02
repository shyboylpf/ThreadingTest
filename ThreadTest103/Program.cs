using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest103
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var items = (from site in new[]
              {
              "www.baidu.com",
              "t.cn",
              "t.tt",
            }
              .AsParallel().WithDegreeOfParallelism(3)
                         let p = new Ping().Send(site)
                         select new
                         {
                             site,
                             Result = p.Status,
                             Time = p.RoundtripTime
                         });

            foreach (var item in items)
            {
                Console.WriteLine($"{item.site}|{item.Result}|{item.Time}");
            }
        }
    }
}