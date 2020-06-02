using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TaskTest2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    // Generating TAP methods manually
    /*You may implement the TAP pattern manually for better control over implementation. The compiler relies on the public surface
     * area exposed from the System.Threading.Tasks namespace and supporting types in the System.Runtime.CompilerServices namespace.
     * To implement the TAP yourself, you create a TaskCompletionSource<TResult> object, perform the asynchronous operation, and when it completes,
     * call the SetResult, SetException, or SetCanceled method, or the Try version of one of these methods. When you implement a TAP method manually,
     * you must complete the resulting task when the represented asynchronous operation completes. For example:
     */

    public static class StaticClassName
    {
        public static Task<int> ReadTask(this Stream stream, byte[] buffer, int offset, int count, object state)
        {
            var tcs = new TaskCompletionSource<int>();
            stream.BeginRead(buffer, offset, count, ar =>
            {
                try
                {
                    tcs.SetResult(stream.EndRead(ar));
                }
                catch (Exception exc)
                {
                    tcs.SetException(exc);
                }
            }, state);
            return tcs.Task;
        }
    }
}