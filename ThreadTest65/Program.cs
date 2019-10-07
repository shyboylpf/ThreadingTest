using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest65
{
    class Program
    {
        Lazy<Expensive> _expensive = new Lazy<Expensive>(()=>new Expensive(), true);
        public Expensive Expensive
        {
            get
            {
                return _expensive.Value;
            }
        }
        static void Main(string[] args)
        {
            var exp = new Program().Expensive;
        }
    }
    class Expensive
    {

    }
}
