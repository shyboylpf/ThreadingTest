using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Reflection;
using System.Security.Cryptography;

namespace ThreadTest112
{
    internal class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static void Main(string[] args)
        {
            log.Info("test");
            Parallel.For(1, 10, i => Foo(i));
            Console.WriteLine("====");
            Parallel.For(1, 10, Foo);

            Parallel.ForEach("Hello , World", Foo);

            var keyPairs = new string[6];
            Parallel.For(0, keyPairs.Length, i => keyPairs[i] = RSA.Create().ToXmlString(true));
            Parallel.ForEach(keyPairs, i => Console.WriteLine(i));

            string[] keyPairs2 =
                ParallelEnumerable.Range(0, 6)
                .Select(i => RSA.Create().ToXmlString(true))
                .ToArray();
            Parallel.ForEach(keyPairs2, i => Console.WriteLine(i));
        }

        private static void Foo(int i)
        {
            log.Info(i);
        }

        private static void Foo(char c)
        {
            log.Info(c);
        }
    }
}