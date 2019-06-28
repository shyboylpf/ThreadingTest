///Entering the Thread Pool via TPL
//You can enter the thread pool easily using the Task classes in the Task Parallel Library.The Task classes were introduced in Framework 4.0: 
//if you’re familiar with the older constructs, consider the nongeneric Task class a replacement for ThreadPool.QueueUserWorkItem, and the generic 
//Task<TResult> a replacement for asynchronous delegates. The newer constructs are faster, more convenient, and more flexible than the old.
//To use the nongeneric Task class, call Task.Factory.StartNew, passing in a delegate of the target method:

using System;
using System.Threading.Tasks;

namespace ThreadTest16
{
    class Program
    {
        static void Main(string[] args)  // The Task class is in System.Threading.Tasks
        {
            Task.Factory.StartNew(Go);
            ///线程池都是后台线程 , 主线程结束 , 后台线程也被结束 , 所以很难打印出信息.
            ///call Wait(), 
            ///Task.Factory.StartNew returns a Task object, which you can then use to monitor the task — for instance, you can wait for it to complete by calling its Wait method.
            Task.Factory.StartNew(Go).Wait();
        }

        static void Go()
        {
            Console.WriteLine("Hello from the thread pool.");
        }
    }
}
