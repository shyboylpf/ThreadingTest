using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest49
{
    /// <summary>
    /// ManualResetEvent
    /// A ManualResetEvent functions like an ordinary gate. Calling Set opens the gate, allowing any number of threads calling WaitOne to be let through. Calling Reset closes the gate. Threads that call WaitOne on a closed gate will block; when the gate is next opened, they will be released all at once. Apart from these differences, a ManualResetEvent functions like an AutoResetEvent.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var manual1 = new ManualResetEvent(false);
            var manual2 = new EventWaitHandle(false, EventResetMode.ManualReset);
            // .net4.0 之后加了这个优化版本. 针对短时间等待进行了优化, 能够选择旋转一定次数的迭代。
            // 更有效的托管实施 , 可以通过 CancellationToken来取消一个wait
            // 不可以进行进程间通信,  
            var manual3 = new ManualResetEventSlim(false);
        }
        // A ManualResetEvent is useful in allowing one thread to unblock many other threads. 
    }
}
