///Optimizing the Thread Pool
//The thread pool starts out with one thread in its pool.As tasks are assigned, the pool manager “injects” new threads to cope with the extra concurrent workload, up to a maximum limit.After a sufficient period of inactivity, the pool manager may “retire” threads if it suspects that doing so will lead to better throughput.


//You can set the upper limit of threads that the pool will create by calling ThreadPool.SetMaxThreads; the defaults are:

//1023 in Framework 4.0 in a 32-bit environment
//32768 in Framework 4.0 in a 64-bit environment
//250 per core in Framework 3.5
//25 per core in Framework 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreadTest22
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
