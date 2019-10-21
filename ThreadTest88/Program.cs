using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest88
{
    internal class PCQueue
    {
        private readonly object _locker = new object();
        private Thread[] _workers;
        private Queue<Action> _itemQ = new Queue<Action>();

        private static void Main(string[] args)
        {
            PCQueue q = new PCQueue(2);

            Console.WriteLine("Enqueuing 10 items...");

            for (int i = 0; i < 10; i++)
            {
                int itemNumber = i;     // 来避免变量捕获陷阱
                q.EnqueueItem(() =>
                {
                    Thread.Sleep(1000); // 模拟费时操作
                    Console.WriteLine(" Task" + itemNumber + "ID: " + Thread.CurrentThread.ManagedThreadId);
                });
            }

            q.ShutDown(true);
            Console.WriteLine();
            Console.WriteLine("Workers complete!");
        }

        /// <summary>
        /// 构造函数, 创建并启动多个worker线程
        /// </summary>
        /// <param name="workerCount"></param>
        public PCQueue(int workerCount)
        {
            _workers = new Thread[workerCount];

            // 为每个worker创建并启动单独的线程
            for (int i = 0; i < workerCount; i++)
            {
                (_workers[i] = new Thread(Consume)).Start();
            }
        }

        /// <summary>
        /// 结束worker
        /// </summary>
        /// <param name="waitForWorkers">是否等待Worker工作结束</param>
        public void ShutDown(bool waitForWorkers)
        {
            // 给每个woker入队一个null, 使其退出
            foreach (Thread worker in _workers)
            {
                EnqueueItem(null);
            }

            // 等待Worker们完成
            if (waitForWorkers)
            {
                foreach (Thread worker in _workers)
                {
                    worker.Join();
                }
            }
        }

        /// <summary>
        /// 生产者
        /// </summary>
        /// <param name="item">任务</param>
        private void EnqueueItem(Action item)
        {
            lock (_locker)
            {
                _itemQ.Enqueue(item);       // 我们必须pulse, 因为我们正在改变
                Monitor.Pulse(_locker);     // 阻塞条件
            }
        }

        /// <summary>
        /// 消费者
        /// </summary>
        private void Consume()
        {
            while (true)                // 持续消费
            {
                Action item;
                lock (_locker)
                {
                    while (_itemQ.Count == 0)
                    {
                        Monitor.Wait(_locker);
                    }
                    item = _itemQ.Dequeue();
                }
                if (item == null)       // 这标志着我们退出
                {
                    return;
                }
                item();                 // 执行item
            }
        }
    }
}