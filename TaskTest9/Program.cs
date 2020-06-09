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
         *
         */

        private static void Main(string[] args)
        {
        }
    }
}