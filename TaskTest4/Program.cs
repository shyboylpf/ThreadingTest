using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest4
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            /*
             * Workloads
            * If a method is purely compute-bound, it should be exposed only as a synchronous implementation.
            * 如果一个方法是纯粹的计算密集型方法，则此方法应该公开为同步方法
            *  The code that consumes it may then choose whether to wrap an invocation of that synchronous method into a task to offload the work to another thread or to achieve parallelism.
            *  使用它的方法可以选择是否要包装到一个任务中，一次实现另外的流程和并发性
            *  And if a method is I/O-bound, it should be exposed only as an asynchronous implementation.
            *  而且，如果方法是I / O绑定的，则应仅将其公开为异步实现。
             */
        }

        //internal Task<Bitmap> RenderAsync(
        //    ImageData data, CancellationToken cancellationToken)
    }
}