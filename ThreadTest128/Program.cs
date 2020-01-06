using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest128
{
    /// <summary>
    /// 借力TaskCompletionSource
    /// 知道工作项何时完成。
    /// 取消未启动的工作项目。
    /// 优雅地处理工作项引发的任何异常。
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            var pcQ = new PCQueue(1);
            Task task = pcQ.EnqueueTask(() => Console.WriteLine("Easy!"));
            Thread.Sleep(Timeout.Infinite);
        }
    }

    public class PCQueue : IDisposable
    {
        private class WorkItem
        {
            public readonly TaskCompletionSource<object> TaskSource;
            public readonly Action Action;
            public readonly CancellationToken? CancelToken;

            public WorkItem(
                TaskCompletionSource<object> taskSource,
                Action action,
                CancellationToken? cancelToken)
            {
                TaskSource = taskSource;
                Action = action;
                CancelToken = cancelToken;
            }
        }

        private BlockingCollection<WorkItem> _taskQ = new BlockingCollection<WorkItem>();

        public PCQueue(int workerCount)
        {
            for (int i = 0; i < workerCount; i++)
            {
                Task.Factory.StartNew(Consume);
            }
        }

        public Task EnqueueTask(Action action)
        {
            return EnqueueTask(action, null);
        }

        public Task EnqueueTask(Action action, CancellationToken? cancelToken)
        {
            var tcs = new TaskCompletionSource<object>();
            _taskQ.Add(new WorkItem(tcs, action, cancelToken));
            return tcs.Task;
        }

        private void Consume()
        {
            foreach (WorkItem workItem in _taskQ.GetConsumingEnumerable())
            {
                if (workItem.CancelToken.HasValue && workItem.CancelToken.Value.IsCancellationRequested)
                {
                    workItem.TaskSource.SetCanceled();
                }
                else
                {
                    try
                    {
                        workItem.Action();
                        workItem.TaskSource.SetResult(null);   // 表示完成
                    }
                    catch (OperationCanceledException ex)
                    {
                        if (ex.CancellationToken == workItem.CancelToken)
                        {
                            workItem.TaskSource.SetCanceled();
                        }
                        else
                        {
                            workItem.TaskSource.SetException(ex);
                        }
                    }
                    catch (Exception ex)
                    {
                        workItem.TaskSource.SetException(ex);
                    }
                }
            }
        }

        public void Dispose()
        {
            _taskQ.CompleteAdding();
        }
    }
}