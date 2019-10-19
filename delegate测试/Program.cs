using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace delegate测试
{
    public class Program
    {
        public static event EventHandler Brodcast;

        private static void Main(string[] args)
        {
            Brodcast += func1;
            Brodcast += func2;
            Brodcast(new object(), new EventArgs());
        }

        private static void func2(object sender, EventArgs e)
        {
            Console.WriteLine
                ("func2");
        }

        private static void func1(object sender, EventArgs e)
        {
            Console.WriteLine
                ("func1");
        }
    }

    public class Foo
    {
        public event EventHandler BrodcastEvent;

        public void StartBrodcast()
        {
            BrodcastEvent(this, new EventArgs());
        }
    }

    public class Foo1
    {
        public void MessageBrodcast(object sender, EventArgs e)
        {
            try
            {
                Thread.Sleep(2000);
                Console.WriteLine("1. 短信报警成功");
                throw new Exception();
            }
            catch (Exception ex)
            {
            }
        }
    }

    public class Foo2
    {
        public void MailBrodcast(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Console.WriteLine("2. 短信报警成功");
        }
    }
}