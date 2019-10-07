using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest64
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    class Foo
    {
        // 实例化Foo会直接实例化Expensive , 不管expensive是否被访问
        public readonly Expensive expensive = new Expensive();

        // 懒实例化 . lazily instantiate Expensive , 
        // 并且要考虑线程安全问题
        Expensive _expensive;
        readonly object _expenseLock = new object();
        public Expensive Expensive
        {
            get
            {
                if (_expensive == null)
                {
                    lock (_expenseLock)  // 在同一个时刻加了锁的那部分程序只有一个线程可以进入 .
                    {
                        if (_expensive == null)  // 双重锁定
                        {
                            _expensive = new Expensive();
                        }
                    }
                }
                return _expensive;
            }
        }
    }

    class Expensive
    {
        /* Suppose this is expensive to construct */
    }
}
