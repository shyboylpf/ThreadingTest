///The generic Task<TResult> class is a subclass of the nongeneric Task. It lets you get a return value back from the task after it finishes 
///executing. In the following example, we download a web page using Task<TResult>:

using System;
using System.Threading.Tasks;

namespace ThreadTest17
{
    class Program
    {
        static void Main(string[] args)
        {
            // start the task executing:
            Task<string> task = Task.Factory.StartNew<string>
                (() => DownloadString("https://www.baidu.com/"));
            //task.Wait();

            // We can do other work here and it will execute in parallel;
            // RunSomeOtherMethod();

            // When we need the task's return value, we query its Result property:
            // If it's still executing, the current thread will block(wait)
            // until the task finishes:
            string result = task.Result;
            //Console.WriteLine(result.Substring(0, 200));
        }

        static string DownloadString(string uri)
        {
            using (var wc = new System.Net.WebClient())
            {
                //throw null;
                return wc.DownloadString(uri);
            }
        }
    }
}
