using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest18
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            /*
             * Task.WhenAny
             *
             * You can use the WhenAny method to asynchronously wait for just one of multiple asynchronous operations represented as tasks to complete.
             * This method serves four primary use cases:
             * Redundancy: Performing an operation multiple times and selecting the one that completes first (for example, contacting multiple stock quote web services that will produce a single result and selecting the one that completes the fastest).
             * Interleaving: Launching multiple operations and waiting for all of them to complete, but processing them as they complete.
             * Throttling: Allowing additional operations to begin as others complete. This is an extension of the interleaving scenario.
             * Early bailout: For example, an operation represented by task t1 can be grouped in a WhenAny task with another task t2, and you can wait on the WhenAny task. Task t2 could represent a time-out, or cancellation, or some other signal that causes the WhenAny task to complete before t1 completes.
             *
             * Task.WhenAny 可以用来表示任务的多个异步操作之一完成。
             * 主要有下面四种用法：
             * 冗余：执行同一个操作多次，并选择最先完成的一个操作（比如联系多个股票报价服务器，并选取完成最快的一项）
             * 交织：启动多个操作并等待所有操作完成，但是在他们完成时就处理他们的返回结果
             * 节流：允许其他完成时进行附加操作
             * 早期救援：例如，可以将任务t1表示的操作与另一个任务t2分组在WhenAny任务中，并且您可以等待WhileAny任务。任务t2可能表示超时，取消或其他某种信号，这些信号导致WhenAny任务在t1完成之前完成。
             *
             */
        }

        /// <summary>
        /// Redundancy
        // Consider a case where you want to make a decision about whether to buy a stock. There are
        // several stock recommendation web services that you trust, but depending on daily load,
        // each service can end up being slow at different times. You can use the WhenAny method to
        // receive a notification when any operation completes:
        /// 冗余
        /// </summary>
        /// <param name="symbol"></param>
        private static async void Redundancy(object symbol)
        {
            var recommendations = new List<Task<bool>>()
            {
                GetBuyRecommendation1Async(symbol),
                GetBuyRecommendation2Async(symbol),
                GetBuyRecommendation3Async(symbol)
            };
            Task<bool> recommendation = await Task.WhenAny(recommendations);
            if (await recommendation)
            {
                BuyStock(symbol);
            }
        }

        /// <summary>
        /// As with WhenAll, you have to be able to accommodate exceptions. Because you receive the
        /// completed task back, you can await the returned task to have errors propagated, and
        /// try/catch them appropriately; for example:
        /// 与WhenAll一样，您必须能够容纳异常。因为您已收到完成的任务，所以您可以等待返回的任务传播错误，并尝试/捕获它们。例如：
        /// </summary>
        /// <param name="symbol"></param>
        private static async void RedundancyExp(object symbol)
        {
            var recommendations = new List<Task<bool>>()
            {
                GetBuyRecommendation1Async(symbol),
                GetBuyRecommendation2Async(symbol),
                GetBuyRecommendation3Async(symbol)
            };
            while (recommendations.Count() > 0)
            {
                Task<bool> recommendation = await Task.WhenAny(recommendations);
                try
                {
                    if (await recommendation)
                    {
                        BuyStock(symbol);
                    }
                    break;
                }
                catch (WebException exc)
                {
                    recommendations.Remove(recommendation);
                }
            }
        }

        /// <summary>
        /// Additionally, even if a first task completes successfully, subsequent tasks may fail. At
        /// this point, you have several options for dealing with exceptions: You can wait until all
        /// the launched tasks have completed, in which case you can use the WhenAll method, or you
        /// can decide that all exceptions are important and must be logged. For this, you can use
        /// continuations to receive a notification when tasks have completed asynchronously:
        /// 此外，即使第一个任务成功完成，后续任务也可能失败。此时，您可以使用几种方法来处理异常：您可以等待所有启动的任务完成，在这种情况下，可以使用WhenAll方法，或者可以确定所有异常都很重要并且必须将其记录下来。为此，可以在任务异步完成时使用延续来接收通知：
        /// </summary>
        /// <param name="symbol"></param>
        private static async void RedundancyExp2(object symbol)
        {
            var recommendations = new List<Task<bool>>()
            {
                GetBuyRecommendation1Async(symbol),
                GetBuyRecommendation2Async(symbol),
                GetBuyRecommendation3Async(symbol)
            };

            // #1 为每个Task追加一个任务来处理异常
            foreach (Task<bool> item in recommendations)
            {
                var ignored = item.ContinueWith(
                    t =>
                    {
                        if (t.IsFaulted)
                        {
                            Console.WriteLine(t.Exception);
                        }
                    });
            }

            // #2 为每个Task追加一个任务来处理异常
            foreach (var item in recommendations)
            {
                var ignored = item.ContinueWith(
                    t => Console.WriteLine(t.Exception), TaskContinuationOptions.OnlyOnFaulted);
            }

            // #3 为每个Task追加一个任务来处理异常
            LogCompletionIfFailed(recommendations);

            Task<bool> recommendation = await Task.WhenAny(recommendations);
            if (await recommendation)
            {
                BuyStock(symbol);
            }
        }

        private static async void LogCompletionIfFailed(List<Task<bool>> recommendations)
        {
            foreach (var task in recommendations)
            {
                try { await task; }
                catch (Exception exc) { Console.WriteLine(exc); }
            }
        }

        /// <summary>
        /// Finally, you may want to cancel all the remaining operations:
        /// 最后，你可能想取消所有未完成的任务
        /// </summary>
        /// <param name="symbol"></param>
        private static async void RedundancyExp3(object symbol)
        {
            var cts = new CancellationTokenSource();
            var recommendations = new List<Task<bool>>()
            {
                GetBuyRecommendation1Async(symbol, cts.Token),
                GetBuyRecommendation2Async(symbol, cts.Token),
                GetBuyRecommendation3Async(symbol, cts.Token)
            };

            Task<bool> recommendation = await Task.WhenAny(recommendations);
            cts.Cancel();
            if (await recommendation) BuyStock(symbol);
        }

        private static Task<bool> GetBuyRecommendation3Async(object symbol, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        private static Task<bool> GetBuyRecommendation2Async(object symbol, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        private static Task<bool> GetBuyRecommendation1Async(object symbol, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        private static void BuyStock(object symbol)
        {
            throw new NotImplementedException();
        }

        private static Task<bool> GetBuyRecommendation3Async(object symbol)
        {
            throw new NotImplementedException();
        }

        private static Task<bool> GetBuyRecommendation2Async(object symbol)
        {
            throw new NotImplementedException();
        }

        private static Task<bool> GetBuyRecommendation1Async(object symbol)
        {
            throw new NotImplementedException();
        }
    }
}