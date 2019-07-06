///Blocking Versus Spinning

using System.Threading;

namespace ThreadTest24
{
    class Program
    {
        ///Sometimes a thread must pause until a certain condition is met. Signaling and locking constructs achieve this efficiently by blocking until a condition is satisfied. However, there is a simpler alternative: a thread can await a condition by spinning in a polling loop. For example:
        ///while (!proceed);
        // or:

        static void Main(string[] args)
        {
            bool proceed = true;
            while (!proceed) { }
            //while (DateTime.Now<nextStartTime);
            ///In general, this is very wasteful on processor time: as far as the CLR and operating system are concerned, the thread is performing an important calculation, and so gets allocated resources accordingly!
            ///

            //Sometimes a hybrid between blocking and spinning is used:

            while (!proceed) Thread.Sleep(10);
            //Although inelegant, this is (in general) far more efficient than outright spinning.Problems can arise, though, due to concurrency issues on the proceed flag. Proper use of locking and signaling avoids this.
        }

    }
}
