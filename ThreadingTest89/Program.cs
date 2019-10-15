using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadingTest89
{
    internal class Program
    {
        private static readonly object _locker = new object();
        private static bool _ready, _go;

        private static void Main(string[] args)
        {
            new Thread(SaySomething).Start();

            for (int i = 0; i < 5; i++)
                lock (_locker)
                {
                    while (!_ready) Monitor.Wait(_locker);
                    _ready = false;
                    _go = true;
                    Monitor.PulseAll(_locker);
                }
        }

        private static void SaySomething()
        {
            for (int i = 0; i < 5; i++)
            {
                lock (_locker)
                {
                    _ready = true;
                    Monitor.PulseAll(_locker);
                    while (!_go) Monitor.Wait(_locker);
                    _go = false;
                    Console.WriteLine("Wassup?");
                }
            }
        }

        //public int[] TwoSum(int[] nums, int target)
        //{
        //    Dictionary<int, int> map = new Dictionary<int, int>();
        //    for (int i = 0; i < nums.Length; i++)
        //    {
        //        int complement = target - nums[i];
        //        if (map.ContainsKey(complement))
        //        {
        //            return new int[] { map[complement], i };
        //        }
        //        map[nums[i]] = i;
        //    }
        //    throw new Exception();
        //}
    }
}