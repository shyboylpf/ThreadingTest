///Thread Priority
///A thread’s Priority property determines how much execution time it gets relative to other active threads in the operating system, on the following scale:

using System;
using System.Diagnostics;
using System.Threading;

namespace ThreadTest12
{
    class Program
    {
        enum ThreadPriority { Lowest, BelowNormal, Normal, AboveNormal, Highest }
        /// <summary>
        /// Elevating a thread’s priority doesn’t make it capable of performing real-time work, because it’s still throttled by the 
        /// application’s process priority. To perform real-time work, you must also elevate the process priority using the Process 
        /// class in System.Diagnostics (we didn’t tell you how to do this):
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            using (Process p = Process.GetCurrentProcess())
                p.PriorityClass = ProcessPriorityClass.RealTime;
            while (true) { Console.Write('1'); };
        }
        ///ProcessPriorityClass.High is actually one notch short of the highest priority: Realtime. Setting a process priority to Realtime 
        ///instructs the OS that you never want the process to yield CPU time to another process. If your program enters an accidental infinite 
        ///loop, you might find even the operating system locked out, with nothing short of the power button left to rescue you! For this reason, 
        ///High is usually the best choice for real-time applications.
        ///

        ///If your real-time application has a user interface, elevating the process priority gives screen updates excessive CPU time, slowing down the 
        ///entire computer (particularly if the UI is complex). Lowering the main thread’s priority in conjunction with raising the process’s priority 
        ///ensures that the real-time thread doesn’t get preempted by screen redraws, but doesn’t solve the problem of starving other applications of 
        ///CPU time, because the operating system will still allocate disproportionate resources to the process as a whole. An ideal solution is to 
        ///have the real-time worker and user interface run as separate applications with different process priorities, communicating via Remoting or 
        ///memory-mapped files. Memory-mapped files are ideally suited to this task; we explain how they work in Chapters 14 and 25 of C# 4.0 in a Nutshell.
    }
}
