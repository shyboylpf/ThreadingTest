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
            Brodcast(new Program(), EventArgs.Empty);
            for (int i = 0; i < 5; i++)
                Brodcast?.Invoke(new object(), EventArgs.Empty);

            Console.WriteLine("event invoke");
            Foo foo = new Foo();
            Foo1 foo1 = new Foo1();
            Foo2 foo2 = new Foo2();
            foo.BrodcastEvent += foo1.MessageBrodcast;
            foo.BrodcastEvent += foo2.MailBrodcast;
            foo.StartBrodcast();
        }

        private static void func2(object sender, EventArgs e)
        {
            Console.WriteLine("func2");
        }

        private static void func1(object sender, EventArgs e)
        {
            Console.WriteLine("func1");
        }
    }

    public class Foo
    {
        public event EventHandler BrodcastEvent;

        public void StartBrodcast()
        {
            BrodcastEvent(this, new EventArgs());
            //BrodcastEvent.BeginInvoke(this, new EventArgs(), new AsyncCallback());
        }
    }

    public class Foo1
    {
        public void MessageBrodcast(object sender, EventArgs e)
        {
            try
            {
                Thread.Sleep(5000);
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
            Thread.Sleep(5000);
            Console.WriteLine("2. 短信报警成功");
        }
    }
}