// Passing Data to a Thread
// The easiest way to pass arguments to a thread’s target method is to execute 
// a lambda expression that calls the method with the desired arguments:
using System;
using System.Threading;

namespace ThreadTest8
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(() => Print("Hello from t."));
            t.Start();


            ///With this approach, you can pass in any number of arguments to the method. You can even wrap the entire implementation in a multi-statement lambda:
            ///
            new Thread(() =>
            {
                Console.WriteLine("From another t.");
                Console.WriteLine("Hello.");
            }).Start();

            ///You can do the same thing almost as easily in C# 2.0 with anonymous methods:
            ///
            new Thread(delegate ()
            {
                Console.WriteLine("Hello from delegate.");
            }).Start();

            ///Another technique is to pass an argument into Thread’s Start method:
            ///
            Thread t2 = new Thread(Print);
            t2.Start("Hello from object t");
            ///This works because Thread’s constructor is overloaded to accept either of two delegates:
            ///public delegate void ThreadStart();
            ///public delegate void ParameterizedThreadStart(object obj);
            ///





        }

        static void Print(string message)
        {
            Console.WriteLine(message);
        }

        static void Print(object message)
        {
            Console.WriteLine(message.ToString());
        }
    }
}
