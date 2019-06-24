
// Creating and Starting Threads
using System;
using System.Threading;

namespace ThreadTest7
{
    class Program
    {
        static void Main(string[] args)
        {
            // 用ThreadStart
            //Thread t = new Thread(new ThreadStart(Go));

            // A thread can be created more conveniently by specifying just a method group — and allowing C# to infer the ThreadStart delegate:
            // ThreadStart 可以省略
            //Thread t = new Thread(Go);

            // Another shortcut is to use a lambda expression or anonymous method:
            // 另一种缩写方式, 使用lambda表达式或者匿名函数
            Thread t = new Thread(()=> { Console.WriteLine("Hello!"); });

            t.Start();
            Go();
        }

        static void Go()
        {
            Console.WriteLine("hello");
        }
    }
}
