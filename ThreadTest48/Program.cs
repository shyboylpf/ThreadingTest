using System;
using System.Collections.Generic;
using System.Threading;

namespace ThreadTest48
{
    /// <summary>
    /// Producer/consumer queue
    /// </summary>
    class ProduceConsumerQueue : IDisposable
    {

        EventWaitHandle _wh = new AutoResetEvent(false);
        Thread _worker;
        readonly object _locker = new object();
        Queue<string> _tasks = new Queue<string>();


        static void Main(string[] args)
        {
            using(ProduceConsumerQueue q = new ProduceConsumerQueue())
            {
                q.EnqueueTask("Hello");
                for(int i = 0; i < 10; i++)
                {
                    q.EnqueueTask("Say " + i);
                }
                q.EnqueueTask("GoodBye!");
            }

            // Exiting the using satement calls q's Dispose method, which
            // enqueues a null task and waits until the consumer finishes.
            // 当using结束的时候, 会自动调用q的dispose, 入队一个null任务,并且等待, 直到消费者退出.
        }

        public ProduceConsumerQueue()
        {
            _worker = new Thread(Work);
            _worker.Start();
        }

        public void EnqueueTask(string task)
        {
            lock (_locker) _tasks.Enqueue(task);
            _wh.Set();
        }

        

        public void Dispose()
        {
            EnqueueTask(null);
            // Join阻塞主线程, 直到本线程结束. 让别人等自己, 而且是自己要先执行完才行.
            _worker.Join();
            _wh.Close();
        }

        // 消费者
        private void Work()
        {
            while (true)
            {
                string task = null;
                lock (_locker)
                {
                    if (_tasks.Count > 0)
                    {
                        task = _tasks.Dequeue();
                        if (task == null) return;
                    }
                }
                if (task != null)
                {
                    Console.WriteLine("Performing task: " + task);
                    Thread.Sleep(1000);     // simulate work...
                }
                else
                {
                    _wh.WaitOne();          // No more tasks - wait for a signal
                }
            }
        }

        ///Framework 4.0 provides a new class called BlockingCollection<T> that implements the functionality of a producer/consumer queue.
    }
}
