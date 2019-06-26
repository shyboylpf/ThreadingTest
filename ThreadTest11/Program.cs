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
        }
        ///没有参数的情况下 , worker是一个前台线程, 需要等待worker线程的ReadLine操作
        /// dotnet.exe .\ThreadTest11.dll a
        ///有参数的情况下 , worker是一个后台线程, 主线程结束的时候 , worker会直接挂掉, 不会等待ReadLine操作.
        /// dotnet.exe .\ThreadTest11.dll
    }
}
