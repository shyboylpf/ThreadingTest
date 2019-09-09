using System;
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
            new Thread(AddItem).Start();
            new Thread(AddItem).Start();
            new Thread(AddItem).Start();
        }

        private static void AddItem()
        {
            lock (_list) _list.Add("Item " + _list.Count);
            string[] items;
            lock (_list) items = _list.ToArray();
            foreach (string s in items) Console.WriteLine(s);
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
