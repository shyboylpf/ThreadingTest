using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.Reflection;

namespace ThreadTest93
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string start = "22:00";
            string end = "11:00";

            DateTime start1 = DateTime.ParseExact(start, "HH:mm", CultureInfo.InvariantCulture);
            DateTime end2 = DateTime.ParseExact(end, "HH:mm", CultureInfo.InvariantCulture);

            int timeout = 30;

            DateTime offBedTime = DateTime.Now.AddHours(-1);

            if (DateTime.Now.Subtract(offBedTime).TotalMinutes > timeout)
            {
                if (DateTime.Now > start1 || DateTime.Now < end2)
                {
                    Console.WriteLine("报警");
                }
            }
            else
            {
                Console.WriteLine("正常");
            }

            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
            keyValuePairs[1] = 1;
            keyValuePairs[2] = 2;
            keyValuePairs[1] = 3;
            keyValuePairs.Remove(1);
            keyValuePairs.Remove(3);
            keyValuePairs.Add(1, 2);
            foreach (var item in keyValuePairs.Keys)
            {
                Console.WriteLine(item);
            }
            if (keyValuePairs.ContainsKey(1))
            {
                Console.WriteLine("key有1");
            }
            int result;
            if (keyValuePairs.TryGetValue(3, out result))
            {
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("此key不存在");
            }

            Dictionary<int, Foo> keyValuePairs1 = new Dictionary<int, Foo>();
            Foo foo = new Foo()
            {
                a = 10
            };
            keyValuePairs1[1] = foo;
            keyValuePairs1[1].a = 12;
            Foo foo1 = keyValuePairs1[1];
            func(ref foo1);
            Console.WriteLine(keyValuePairs1[1].a);

            int a = 10;
            func1(ref a);
            Console.WriteLine(a);

            int b = 10;
            func2(b);
            Console.WriteLine(b);
            //try
            //{
            //    new Thread(func3).Start();
            //}
            //catch
            //{
            //    Console.WriteLine("func3线程崩溃了");
            //}
            Console.WriteLine(types.a);
            func4(test.normal);
            HbWarningType hbWarningType = new HbWarningType();
            Console.WriteLine(hbWarningType.warning == test.normal);

            //WarningType warning = new WarningType();\

            // === 线程安全的字典

            ConcurrentDictionary<int, int> keyValuePairs2 = new ConcurrentDictionary<int, int>();
            keyValuePairs2[1] = 1;
            keyValuePairs2[2] = 2;
            keyValuePairs2.TryRemove(1, out int value2);
            keyValuePairs2.TryAdd(1, 3);
            Console.WriteLine(keyValuePairs2[1]);
            Console.WriteLine(keyValuePairs2.Count);
            Console.WriteLine(keyValuePairs2.Skip(0).Count());
            Console.WriteLine("reflection...");
            ClassForReflction cs = new ClassForReflction();
            Type type = cs.GetType();
            MemberInfo[] memberInfos = type.GetMembers();
            foreach (MemberInfo item in memberInfos)
            {
                Console.WriteLine(item.Name);
            }

            FieldInfo[] fieldInfos = type.GetFields();
            List<FieldInfo> warnTypeInfos = new List<FieldInfo>();
            foreach (var item in fieldInfos)
            {
                Console.WriteLine($"GetFields: {item.Name}:{item.GetValue(cs)}:{item.FieldType}:{item.FieldType.Name}");
                if (item.FieldType.BaseType == typeof(WarningType))
                    warnTypeInfos.Add(item);
            }

            Console.WriteLine("warnTypeInfos");
            foreach (var item in warnTypeInfos)
            {
                Console.WriteLine($"test: {item.Name}:{item.GetValue(cs)}:{item.FieldType}:{item.FieldType.Name}");
            }
            Console.WriteLine("warnTypeInfosEnd");

            FieldInfo fieldInfo = type.GetField("hbWarn");
            object obj = fieldInfo.GetValue(cs);
            PropertyInfo propertyInfo = type.GetProperty("hbWarn");
            PropertyInfo[] propertyInfos = type.GetProperties();
            foreach (var item in propertyInfos)
            {
                Console.WriteLine(item.Name);
            }

            Thread.Sleep(Timeout.Infinite);
        }

        public static void func4(object obj)
        {
            if (obj.ToString().Contains("normal"))
            {
                Console.WriteLine("enum转换 object正常");
            }
        }

        public static void func3()
        {
            throw new Exception();
        }

        public static void func2(int a)
        {
            a = 17;
        }

        public static void func1(ref int a)
        {
            a = 15;
        }

        public static void func(ref Foo foo)
        {
            foo.a = 14;
        }
    }

    public class Foo
    {
        public int a;
    }

    public enum types
    {
        a = 1,
    }

    public enum test
    {
        normal,
        earlyWarning,
        Warning,
    }

    public abstract class WarningType
    {
        public long ID;
        public test warning;

        public WarningType()
        {
            ID = 10;
            warning = test.Warning;
        }
    }

    public class HbWarningType : WarningType
    {
    }

    public class ClassForReflction
    {
        public HbWarningType hbWarn = new HbWarningType();
        public HbWarningType hbaarn = new HbWarningType();
        public long long1 = 0;
    }
}