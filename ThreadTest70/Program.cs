using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest70
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class test
    {
        // The same LocalDataStoreSlot object can be used across all threads
        LocalDataStoreSlot _secSlot = Thread.GetNamedDataSlot("securityLevel");
        //  Alternatively, you can control a slot’s scope yourself with an unnamed slot, obtained by calling Thread.AllocateDataSlot:
        LocalDataStoreSlot _secSlot2 = Thread.AllocateDataSlot();

        // This property has a separate value on each thread.
        int SecurityLevel
        {
            get
            {
                object data = Thread.GetData(_secSlot);
                return data == null ? 0 : (int)data;        // null == uninitialized
            }
            set
            {
                Thread.SetData(_secSlot, value);
            }
        }
    }
}
