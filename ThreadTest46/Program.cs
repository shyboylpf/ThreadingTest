using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest46
{
    class Program
    {
        static void Main1(string[] args)
        {
            // 有两种方式可以创建 AutoResetEvent
            // 第一种方式 , 如果直接传入true 的 , 相当于直接calling Set upon it.
            var auto = new AutoResetEvent (false);
            // 第二种方式
            var auto2 = new EventWaitHandle(false, EventResetMode.AutoReset);

            // 下面的示例是一个进程简单的等待 , 知道另外一个线程给与信号(signal).
        }
    }

    class BasicWaitHandle
    {
        static EventWaitHandle _wairHandle = new AutoResetEvent(false);
        static void Main()
        {
            new Thread(Waiter).Start();
            //new Thread(Waiter).Start();
            Thread.Sleep(1000); // Pause for a second...
            _wairHandle.Set();  // Wake up the Waiter.
        }

        private static void Waiter()
        {
            Console.WriteLine("Waiting...");
            if(_wairHandle.WaitOne())//;  // Wait for notification
            {

                Console.WriteLine("Notifield.");
            }
            //_wairHandle.Reset();
        }
    }
    ///If Set is called when no thread is waiting, the handle stays open for as long as it takes until some thread calls WaitOne. This behavior helps avoid a race between a thread heading for the turnstile, and a thread inserting a ticket (“Oops, inserted the ticket a microsecond too soon, bad luck, now you’ll have to wait indefinitely!”). However, calling Set repeatedly on a turnstile at which no one is waiting doesn’t allow a whole party through when they arrive: only the next single person is let through and the extra tickets are “wasted.”
    ///Calling Reset on an AutoResetEvent closes the turnstile (should it be open) without waiting or blocking.
    ///WaitOne accepts an optional timeout parameter, returning false if the wait ended because of a timeout rather than obtaining the signal.
    ///Calling WaitOne with a timeout of 0 tests whether a wait handle is “open,” without blocking the caller. Bear in mind, though, that doing this resets the AutoResetEvent if it’s open.
}
