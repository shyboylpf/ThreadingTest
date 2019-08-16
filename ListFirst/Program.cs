using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            List<tmp> tmps = new List<tmp>();
            Console.WriteLine(tmps.First(x=>x.sn.Equals("1")));
        }
    }

    class tmp
    {
        public string sn { get; set; }
    }
}
