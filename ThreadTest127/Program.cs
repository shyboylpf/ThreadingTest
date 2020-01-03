using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace ThreadTest127
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PCQueue pCQueue = new PCQueue(4);
            foreach (var item in Enumerable.Range(0, 100))
                pCQueue.EnqueueTask(() => Console.Write(item + ":" + Thread.CurrentThread.ManagedThreadId + " "));
            pCQueue.Dispose();
            Thread.Sleep(Timeout.Infinite);
        }
    }

    public class PCQueue : IDisposable
    {
        private BlockingCollection<Action> _taskQ = new BlockingCollection<Action>();

        public PCQueue(int workerCount)
        {
            // 为每个消费者创建并开始隔离的任务
            for (int i = 0; i < workerCount; i++)
            {
                Task.Factory.StartNew(Consume);
            }
        }

        public void EnqueueTask(Action action)
        {
            _taskQ.Add(action);
        }

        private void Consume()
        {
            // 这个sequeue会在没有元素的时候blocking , 会在CompleteAdding的时候结束
            foreach (Action action in _taskQ.GetConsumingEnumerable())
            {
                action(); // 执行任务
            }
        }

        public void Dispose()
        {
            _taskQ.CompleteAdding();
        }
    }
}