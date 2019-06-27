///Foreground and Background Threads
///By default, threads you create explicitly are foreground threads.Foreground threads keep the application alive for as long as any one of them is running, whereas background 
///threads do not.Once all foreground threads finish, the application ends, and any background threads still running abruptly terminate.
///
/// Note:A thread’s foreground/background status has no relation to its priority or allocation of execution time.
/// 
using System;
using System.Threading;

namespace ThreadTest11
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread worker = new Thread(() => Console.ReadLine());
            if (args.Length > 0) worker.IsBackground = true;
            worker.Start();

            ///When a process terminates in this manner, any finally blocks in the execution stack of background threads are circumvented. This is a problem if your program employs finally (or using) blocks to perform cleanup work such as releasing resources or deleting temporary files. To avoid this, you can explicitly wait out such background threads upon exiting an application. There are two ways to accomplish this:
            /// If you’ve created the thread yourself, call Join on the thread.
            /// If you’re on a pooled thread, use an event wait handle.
            worker.Join();
        }
        ///没有参数的情况下 , worker是一个前台线程, 需要等待worker线程的ReadLine操作
        /// dotnet.exe .\ThreadTest11.dll a
        ///有参数的情况下 , worker是一个后台线程, 主线程结束的时候 , worker会直接挂掉, 不会等待ReadLine操作.
        /// dotnet.exe .\ThreadTest11.dll
    }
}
