using System;
using System.Threading;

namespace ThreadTest14
{
    class Program
    {
        static void Main(string[] args)
        {
            ///The remedy is to move the exception handler into the Go method:
            ///
            new Thread(Go).Start();
            
        }

        static void Go()
        {
            try
            {
                // ...
                throw null;    // The NullReferenceException will get caught below
                               // ...
            }
            catch (Exception ex)
            {
                // Typically log the exception, and/or signal another thread
                // that we've come unstuck
                // ...
            }
        }
        ///You need an exception handler on all thread entry methods in production applications — just as you do (usually at a higher level, 
        ///in the execution stack) on your main thread. An unhandled exception causes the whole application to shut down. With an ugly dialog!
    }
    ///In writing such exception handling blocks, rarely would you ignore the error: typically, you’d log the details of the exception, and then 
    ///perhaps display a dialog allowing the user to automatically submit those details to your web server. You then might shut down the application 
    ///— because it’s possible that the error corrupted the program’s state. However, the cost of doing so is that the user will lose his recent work 
    ///— open documents, for instance.
    ///

    ///The “global” exception handling events for WPF and Windows Forms applications (Application.DispatcherUnhandledException and 
    ///Application.ThreadException) fire only for exceptions thrown on the main UI thread. You still must handle exceptions on worker threads manually.
    ///AppDomain.CurrentDomain.UnhandledException fires on any unhandled exception, but provides no means of preventing the application from shutting down afterward.
    ///

    ///There are, however, some cases where you don’t need to handle exceptions on a worker thread, because the .NET Framework does it for you. These are covered in upcoming sections, and are:
    ///Asynchronous delegates
    ///BackgroundWorker
    ///The Task Parallel Library(conditions apply)
}
