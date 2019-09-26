using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/// <summary>
/// Memory Barriers and Volatility
/// </summary>
namespace ThreadTest45
{
    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            a.B();
        }
    }

    class Foo
    {
        int _answer;
        bool _complete;

        void A()
        {
            _answer = 123;
            _complete = true;
        }

        void B()
        {
            if (_complete)
            {
                Console.WriteLine(_answer);
            }
        }
    }

    // 如果A/B同事运行在不同的线程上,  那么B有没有可能打印出0?
    // 是可以的,原因如下: 
    // 1. the compiler , CLR, or CPU may reorder your program's instructions to imporove efficiency.
    // 2. the compiler, CLR, or CPU may introduce caching optimizations such that assignments to variables won't be visible to other threads right away.



    ///Full fences
    class Foo1
    {
        int _answer;
        bool _complete;
        void A()
        {
            _answer = 123;
            Thread.MemoryBarrier(); // Barrier 1
            _complete = true;
            Thread.MemoryBarrier(); // Barrier 2
        }

        void B()
        {
            Thread.MemoryBarrier();  // Barrier 3
            if (_complete)
            {
                Thread.MemoryBarrier();  // Barrier 4
                Console.WriteLine(_answer);
            }
        }
    }
    //Barriers 1 and 4 prevent this example from writing “0”. Barriers 2 and 3 provide a freshness guarantee: they ensure that if B ran after A, reading _complete would evaluate to true.


    // 以下方式会隐式的生成完整围栏
    // 1. c#'s lock statement(Monitor.Enter / Monitor.Exit)
    // 2. all methods on Interlocked class (we'll cover these soon)
    // 3. Asynchronous call backs that use the thread pool -- these include asynchronous delegatess, APMcallbacks , and Task continuations
    // 4. setting and waiting on a signaling construct
    // 5. Anything that relies on signaling, such as starting or waiting on a Task.
    // By virtue of that last point, the following is thread-safe:
    class A
    {
        public void B()
        {
            int x = 0;
            Task t = Task.Factory.StartNew(() => x++);
            t.Wait();
            Console.WriteLine(x);
        }
    }
}
