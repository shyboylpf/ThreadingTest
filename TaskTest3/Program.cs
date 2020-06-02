using System;
using System.Threading.Tasks;

namespace TaskTest3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        /*Hybrid approach
        You may find it useful to implement the TAP pattern manually but to delegate the core logic for the implementation to the compiler.
        For example, you may want to use the hybrid approach when you want to verify arguments outside a compiler-generated asynchronous method
        so that exceptions can escape to the method’s direct caller rather than being exposed through the System.Threading.Tasks.Task object:
        Another case where such delegation is useful is when you're implementing fast-path optimization and want to return a cached task.*/

        /// <summary>
        /// 用嵌套的方式来实现捕获异常
        /// 或者也可以用在缓存层的路径优化，如果缓存有就直接return缓存的值，没有的话，开始Task费时操作
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<int> MethodAsync(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            return MethodAsyncInternal(input);
        }

        private async Task<int> MethodAsyncInternal(string input)
        {
            // code that uses await goes here
            await PrivateFunc();
            int value = 1;
            return value;
        }

        private Task PrivateFunc()
        {
            return null;
        }
    }
}