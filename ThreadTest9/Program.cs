using System;
using System.Threading;

namespace ThreadTest9
{
    class Program
    {
        static void Main(string[] args)
        {
            ///Lambda expressions and captured variables
            ///As we saw, a lambda expression is the most powerful way to pass data to a thread. However, you must be careful about accidentally modifying captured 
            ///variables after starting the thread, because these variables are shared.For instance, consider the following:
            for (int i = 0; i < 10; i++)
            {
                //new Thread(() => { Console.WriteLine(i.ToString()); }).Start();
            }
            ///The problem is that the i variable refers to the same memory location throughout the loop’s lifetime. Therefore, each thread calls Console.Write on a 
            ///variable whose value may change as it is running!
            ///
            ///Note: This is analogous to the problem we describe in “Captured Variables” in Chapter 8 of C# 4.0 in a Nutshell. The problem is less about multithreading 
            ///and more about C#'s rules for capturing variables (which are somewhat undesirable in the case of for and foreach loops).

            ///The solution is to use a temporary variable as follows:
            /// 扑街 , 根本不好用!
            for (int i = 0; i < 10; i++)
            {
                int temp = i;
                //new Thread(() => Console.WriteLine(temp)).Start();
            }

            ///Variable temp is now local to each loop iteration. Therefore, each thread captures a different memory location and there’s no problem. 
            ///We can illustrate the problem in the earlier code more simply with the following example:
            string text = "t1";
            Thread t1 = new Thread(() => Console.WriteLine(text));

            text = "t2";
            Thread t2 = new Thread(() => Console.WriteLine(text));

            t1.Start();
            t2.Start();

        }
    }
}
