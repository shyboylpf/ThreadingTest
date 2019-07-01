using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreadTest20
{
    class Program
    {
        /// <summary>
        /// 书接上回, ThreadTest19.netCore竟然不支持这样的操作
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Func<string, int> method = Work;
            IAsyncResult cookie = method.BeginInvoke("test", null, null);
            int result = method.EndInvoke(cookie);
            Console.WriteLine("String length is : " + result);
        }
        static int Work(string s)
        {
            return s.Length;
        }
    }
}
