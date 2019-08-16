using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Choosing the Synchronization Object
/// Any object visible to each of the partaking threads can be used as a synchronizing object, subject to one hard rule: it must be a reference type.The synchronizing object is typically private (because this helps to encapsulate the locking logic) and is typically an instance or static field.The synchronizing object can double as the object it’s protecting, as the _list field does in the following example:
/// </summary>

namespace ThreadTest26
{
    class Program
    {
        List<string> _list = new List<string>();
        static void Main(string[] args)
        {

        }

        void Test()
        {
            lock (_list)
            {
                _list.Add("Item 1");
            }

            // A field dedicated for the purpose of locking (such as _locker, in the example prior) allows precise control over the scope and granularity of the lock. The containing object (this) — or even its type — can also be used as a synchronization object:
            lock (this) { }

            //lock (typeof(_list)){ }
        }
    }
}
