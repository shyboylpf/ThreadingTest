﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest31
{
    class Program
    {
        static List<string> _list = new List<string>();
        static void Main(string[] args)
        {
            var thread1 = new Thread(AddItem);
            thread1.Name = "thread1";
            thread1.Start();

            var thread2 = new Thread(AddItem);
            thread2.Name = "thread2";
            thread2.Start();
            //new Thread(AddItem).Start();
            //new Thread(AddItem).Start();
            //new Thread(AddItem).Start();
        }

        private static void AddItem()
        {
            lock (_list)
            {
                _list.Add("Item " + _list.Count);
            }
            string[] items;
            lock (_list)
            {
                items = _list.ToArray();
            }
            foreach (string s in items)
            {
                Console.WriteLine($"name: {Thread.CurrentThread.Name} , {s}");
            }
        }
    }

    static class UserCache
    {
        static Dictionary<int, User> _users = new Dictionary<int, User>();
        internal static User GetUser(int id)
        {
            User u = null;

            lock (_users)
            {
                if(_users.TryGetValue(id, out u))
                {
                    return u;
                }
            }

            u = RetrieveUser(id);
            lock(_users)
            {
                _users[id] = u;
            }
            return u;
        }

        private static User RetrieveUser(int id)
        {
            throw new NotImplementedException();
        }
    }

    internal class User { }
}
