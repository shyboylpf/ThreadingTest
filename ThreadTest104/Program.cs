using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest104
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Camera[] cameras = Enumerable.Range(0, 4)
                .Select(i => new Camera(i))
                .ToArray();

            while (true)
            {
                string[] data = cameras
                    .AsParallel().AsOrdered()
                    .WithDegreeOfParallelism(4)
                    .Select(c => c.GetNextFrame()).ToArray();

                Console.WriteLine(string.Join(", ", data));
            }
        }
    }

    internal class Camera
    {
        public readonly int CameraID;

        public Camera(int cameraID)
        {
            CameraID = cameraID;
        }

        public string GetNextFrame()
        {
            Thread.Sleep(1000);
            return "Frame from camera " + CameraID;
        }
    }
}