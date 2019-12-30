using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest119
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // 使用Task.Factory.StartNew来调用Task的时候,  可以传递一个状态对象"Hello"给目标函数Greet
            var task = Task.Factory.StartNew(Greet, "Hello");
            task.Wait();

            // 因为C#里有lambda表达式 , 我们可以用State object用来标记次Task , 并且我们可以用 AsyncState来取出他的名字
            // 我们可以在vs的Parallel windows windows里更好的调试, 因为task拥有一个有意义的名字
            var task2 = Task.Factory.StartNew(state => Greet("Hello"), "Greeting");
            Console.WriteLine(task2.AsyncState); // Greeting
            task2.Wait();
        }

        private static void Greet(object state)
        {
            Console.WriteLine(state.ToString());
        }
    }
}