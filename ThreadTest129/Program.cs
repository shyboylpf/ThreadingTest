using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest129
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // SpinLock
            var spinLock = new SpinLock(true); // 启动所有者跟踪

            bool lockTaken = false;
            try
            {
                spinLock.Enter(ref lockTaken);
                // 其他代码
            }
            finally
            {
                if (lockTaken)
                {
                    spinLock.Exit();
                }
            }

            // SpinWait
            bool _proceed = false;
            void Test()
            {
                SpinWait.SpinUntil(() => { Thread.MemoryBarrier(); return _proceed; });
            }

            // SpinWait 2
            bool _proceed2;
            void Test2()
            {
                var spinWait = new SpinWait();
                while (!_proceed2)
                {
                    Thread.MemoryBarrier();
                    spinWait.SpinOnce();
                }
            }
        }
    }
}