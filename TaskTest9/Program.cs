using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest9
{
    internal class Program
    {
        /*you can use the await keyword in C# and the Await Operator in Visual Basic to asynchronously await Task and Task<TResult> objects.
         * When you're awaiting a Task, the await expression is of type void. When you're awaiting a Task<TResult>, the await expression is of type TResult
         * 可是使用await关键字来一部等待任务
         * await Task返回void类型， await Task<TResult>则返回TResult类型
         * An await expression must occur inside the body of an asynchronous method.
         * await关键字必须出现在async方法中
         * Under the covers, the await functionality installs a callback on the task by using a continuation. This callback resumes the asynchronous method at the point of suspension. When the asynchronous method is resumed, if the awaited operation completed successfully and was a Task<TResult>, its TResult is returned.
         * await使用continuation来注册了一个回调，此回调在挂起时恢复异步方法，如果等待的方法执行结束，则返回TResult
         * Typically, this is the default task scheduler (TaskScheduler.Default), which targets the thread pool.
         * When an asynchronous method is called, it synchronously executes the body of the function up until the first await expression on an awaitable instance
         * that has not yet completed, at which point the invocation returns to the caller. If the asynchronous method does not return void, a Task or Task<TResult>
         * object is returned to represent the ongoing computation. In a non-void asynchronous method, if a return statement is encountered or the end of the method
         * body is reached, the task is completed in the RanToCompletion final state. If an unhandled exception causes control to leave the body of the asynchronous
         * method, the task ends in the Faulted state. If that exception is an OperationCanceledException, the task instead ends in the Canceled state. In this
         * manner, the result or exception is eventually published.
         * There are several important variations of this behavior. For performance reasons, if a task has already completed by the time the task is awaited,
         * control is not yielded, and the function continues to execute. Additionally, returning to the original context isn't always the desired behavior
         * and can be changed; this is described in more detail in the next section.
         */

        private static void Main(string[] args)
        {
        }
    }
}