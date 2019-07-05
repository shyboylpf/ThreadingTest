﻿///PART 2: BASIC SYNCHRONIZATION
///Synchronization constructs can be divided into four categories:

//Simple blocking methods
//These wait for another thread to finish or for a period of time to elapse.Sleep, Join, and Task.Wait are simple blocking methods.
//Locking constructs
//These limit the number of threads that can perform some activity or execute a section of code at a time. Exclusive locking constructs are most common — these allow just one thread in at a time, and allow competing threads to access common data without interfering with each other.The standard exclusive locking constructs are lock (Monitor.Enter/Monitor.Exit), Mutex, and SpinLock.The nonexclusive locking constructs are Semaphore, SemaphoreSlim, and the reader/writer locks.
//Signaling constructs
//These allow a thread to pause until receiving a notification from another, avoiding the need for inefficient polling. There are two commonly used signaling devices: event wait handles and Monitor’s Wait/Pulse methods. Framework 4.0 introduces the CountdownEvent and Barrier classes.
//Nonblocking synchronization constructs
//These protect access to a common field by calling upon processor primitives.The CLR and C# provide the following nonblocking constructs: Thread.MemoryBarrier, Thread.VolatileRead, Thread.VolatileWrite, the volatile keyword, and the Interlocked class.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadTest23
{
    class Program
    {
        static void Main(string[] args)
        {
            ///Blocking
            //A thread is deemed blocked when itsexecution is paused for some reason, such as when Sleeping or waiting for another to end via Join or EndInvoke.A blocked thread immediately yields its processor time slice, and from then on consumes no processor time until its blocking condition is satisfied.You can test for a thread being blocked via its ThreadState property:
            bool blocked = (Thread.CurrentThread.ThreadState & ThreadState.WaitSleepJoin) != 0;
            Console.WriteLine(blocked);
        }
    }
}
