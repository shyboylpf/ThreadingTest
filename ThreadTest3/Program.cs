///Threads share data if they have a common reference to the same 
///object instance. For example:

using System;
using System.Threading;

namespace ThreadTest3
{
    class Program
    {
        bool done;
        static void Main(string[] args)
        {
            Program tt = new Program();     // Create a common instance
            new Thread(tt.Go).Start();
            tt.Go();
        }

        void Go()
        {
            if (!done) {
                done = true;
                Console.WriteLine("Done");
            }
        }
    }
}
///Because both threads call Go() on the same ThreadTest instance, they share the done field. 
///This results in "Done" being printed once instead of twice:
