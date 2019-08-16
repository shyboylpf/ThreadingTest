using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// When to Lock
/// As a basic rule, you need to lock around accessing any writable shared field. Even in the simplest case — an assignment operation on a single field — you must consider synchronization. In the following class, neither the Increment nor the Assign method is thread-safe:
/// </summary>
namespace ThreadTest27
{
    class Program
    {
        static int _x;
        static void Main(string[] args)
        {
        }

        // 这些都是线程不安全的
        static void Increment() { _x++; }
        static void Assign() { _x = 123; }

        // 以下是Thread-safe版本
        static readonly object _locker = new object();
        static int _x1;
        static void Increment1()
        {
            lock (_locker)
            {
                _x1++;
            }
        }
        static void Assign1()
        {
            lock (_locker)
            {
                _x1 = 123;
            }
        }

    }
}
