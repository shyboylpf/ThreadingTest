using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest14
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Task.FromResult 方法使用的情景为，数据可能已存在，且只需要提升到Task<TResult>的 任务返回方法返回：
            // 数据已经有了，只是用Task来包装一下
            Task.Run(async () => Console.WriteLine(await new Program().GetValueAsync("")));
            Console.ReadKey();
        }

        public Task<int> GetValueAsync(string key)
        {
            int cacheValue;
            return TryGetCacheValue(out cacheValue) ?
                Task.FromResult(cacheValue) : GetValueAsyncInternal();
        }

        private bool TryGetCacheValue(out int cacheValue)
        {
            cacheValue = 1;
            return true;
        }

        private Task<int> GetValueAsyncInternal()
        {
            throw new NotImplementedException();
        }
    }
}