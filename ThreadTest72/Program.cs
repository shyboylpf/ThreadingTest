using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest72
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("主线程测试开始..");
            Thread th = new Thread(ThMethod);
            th.Start();
            Thread.Sleep(1000);
            Console.WriteLine("主线程测试结束..");
            Console.ReadLine();
        }

        private static void ThMethod()
        {
            Console.WriteLine("异步执行开始");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("异步执行" + i.ToString() + "..");
                Thread.Sleep(1000);
            }
            Console.WriteLine("异步执行完成");
        }
    }
}
