using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TreadTest51
{
    /// <summary>
    /// Creating a Cross-Process EventWaitHandle
    /// </summary>
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ///EventWaitHandle’s constructor allows a “named” EventWaitHandle to be created, capable of operating
            ///across multiple processes. The name is simply a string, and it can be any value that doesn’t unintentionally
            ///conflict with someone else’s! If the name is already in use on the computer, you get a reference to the same
            ///underlying EventWaitHandle; otherwise, the operating system creates a new one. Here’s an example:
            EventWaitHandle wh = new EventWaitHandle(false,EventResetMode.AutoReset, "MyCompany.MyApp.SomeName");
            ///If two applications each ran this code, they would be able to signal each other: the wait handle would work across all threads in both processes.
        }
    }
}
