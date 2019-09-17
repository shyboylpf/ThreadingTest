using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest33
{
    /// <summary>
    /// This first example shows how to add and take items so that the operations will block if the collection is either temporarily empty (when taking) or at maximum capacity (when adding), or if a specified timeout period has elapsed. Note that blocking on maximum capacity is only enabled when the BlockingCollection has been created with a maximum capacity specified in the constructor.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Increase or decrease this value as desired.
            int itemsToAdd = 500;

            // Preserve all the display output for Adds and Takes
            //Console.SetBufferSize(80, (itemsToAdd * 2) + 3);

            // A bounded collection. Increase, decrease, or remove the
            // maximum capacity argument to see how it impacts behavior.
            BlockingCollection<int> numbers = new BlockingCollection<int>(50);

            // A simple blocking consumer with no cancellation.
            Task.Run(() =>
            {
                int i = -1;
                while (!numbers.IsCompleted)
                {
                    try
                    {
                        i = numbers.Take();
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Adding was completed!");
                        break;
                    }
                    Console.WriteLine("Take:{0} ", i);

                    // Simulate a slow consumer. This will cause
                    // collection to fill up fast and thus Adds wil block.
                    //Thread.SpinWait(100000);
                }

                Console.WriteLine("\r\nNo more items to take. Press the Enter key to exit.");
            });

            // A simple blocking producer with no cancellation.
            Task.Run(() =>
            {
                for (int i = 0; i < itemsToAdd; i++)
                {
                    numbers.Add(i);
                    Console.WriteLine("Add:{0} Count={1}", i, numbers.Count);
                }

                // See documentation for this method.
                numbers.CompleteAdding();
            });

            // Keep the console display open in debug mode.
            Console.ReadLine();
        }
    }
}
