using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest52
{
    /// <summary>
    /// Wait Handles and the Thread Pool
    /// </summary>
    class Program
    {
        static ManualResetEvent _starter = new ManualResetEvent(false);

        static WaitHandle _wh1 = new AutoResetEvent(false);
        static WaitHandle _wh2 = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            Console.WriteLine("ThreadPool waiting a set.");
            //RegisteredWaitHandle reg = ThreadPool.RegisterWaitForSingleObject(_starter, Go, "Some Data", -1, true);
            //Thread.Sleep(5000);
            //Console.WriteLine("Signaling worker...");
            //_starter.Set();
            //reg.Unregister(_starter);       // Clean up when we're done.

            for(int i = 0; i < 100; i++)
            {
                Task.Run(AppServerMethod2);
                //Thread.Sleep(1000);
            }
            _starter.Set();
            ///SignalAndWait calls Set on one WaitHandle, and then calls WaitOne on another WaitHandle.
            ///After signaling the first handle, it will jump to the head of the queue in waiting on the second handle; 
            ///this helps it succeed (although the operation is not truly atomic). You can think of this method as “swapping” 
            ///one signal for another, and use it on a pair of EventWaitHandles to set up two threads to rendezvous or “meet” at the 
            ///same point in time. Either AutoResetEvent or ManualResetEvent will do the trick. The first thread executes the following:
            WaitHandle.SignalAndWait(_wh1, _wh2);
            WaitHandle.SignalAndWait(_wh2, _wh1);
            Console.ReadLine();

        }

        private static void Go(object state, bool timedOut)
        {
            Console.WriteLine("Started - " + state);
            // Perform task...
        }
        // When the wait handle is signaled(or a timeout elapses), the delegate runs on a pooled thread.
        // 当等待handle被singal, 委托(delegate)会运行在一个线程池上.


        // RegisterWaitForSingleObject在必须处理许多并发请求的应用程序服务器中特别有价值。
        static void AppServerMethod()
        {
            _starter.WaitOne();
            Console.WriteLine("AppServerMethod");
        }
        // 如果一百个客户端同时调用上面的方法, 将在阻塞期间会占用100个线程.
        // 用RegisterWaitForSingleObject替换_wh.WaitOne允许该方法立即返回，不浪费任何线程：
        static void AppServerMethod2()
        {
            RegisteredWaitHandle reg = ThreadPool.RegisterWaitForSingleObject(_starter, Resume, "paramater", -1, true);
        }

        static private void Resume(object state, bool timedOut)
        {
            // ... continue execution
            Console.WriteLine("Resume : " + state + Thread.CurrentThread.Name);
        }
        // 传递给Resume的数据对象允许继续任何瞬态数据。
    }
}
