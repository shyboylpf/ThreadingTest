using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest79
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class IfYouThinkYouUnderstandVolatitle
    {
        volatile int x, y;
        void Test1()        // Executed on one thread
        {
            x = 1;          // Volatile write(release-fence)
            int a = y;      // Volatile read(acuire-fence)
        }

        void Test2()        // Executed on another thread
        {
            y = 1;          // Volatile write 释放围栏
            int b = x;      // Volatile read 获得围栏
        }

        // a,b 可能最后都是0
    }
}
