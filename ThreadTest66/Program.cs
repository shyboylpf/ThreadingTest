using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest66
{
    class Program
    {
        // To use LazyInitializer, call EnsureInitialized before accessing the field, passing a reference to the field and the factory delegate:
        // 要使用LazyInitializer，请在访问该字段之前调用确保初始化，传递对该字段和工厂委托的引用
        Expensive _expensive;
        public Expensive Expensive
        {
            get
            {
                LazyInitializer.EnsureInitialized(ref _expensive, () => new Expensive());
                return _expensive;
            }
        }

        // For reference, here’s how double-checked locking is implemented:
        volatile Expensive _expensive2;
        public Expensive Expensive2
        {
            get
            {
                if (_expensive2 == null)                // First check (outside lock)
                {
                    lock (_expensive2)
                    {
                        if (_expensive2 == null)        // Second check (inside lock)
                        {
                            _expensive2 = new Expensive();
                        }
                    }
                }
                return _expensive2;
            }
        }

        // And here’s how the race-to-initialize pattern is implemented:
        volatile Expensive _expensive3;
        public Expensive Expensive3
        {
            get
            {
                if (_expensive3 == null)
                {
                    var instance = new Expensive();
                    // 用第一个和第三个作比较, 如果相等, 就用第二个参数替换第一个参数;
                    Interlocked.CompareExchange(ref _expensive3, instance, null);
                }
                return _expensive3;
            }
        }


        //
        static void Main(string[] args)
        {
        }
    }

    class Expensive { }
}
