using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO.MemoryMappedFiles;
using System.IO;

namespace ThreadTest35
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() => { P1(); });
            Task.Run(() => { P2(); }).Wait();
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }

        static void P1()
        {
            //throw new Exception();
            //Thread.SpinWait(500000);
            MemoryMappedFile memoryMappedFile = MemoryMappedFile.CreateFromFile(
                new FileStream(@"D:\Temp\Map.mp", FileMode.Create), //Any stream will do it
                "MyMemoryMappedFile",   //Name
                1024 * 1024,    //Size in bytes
                MemoryMappedFileAccess.ReadWrite, //可以针对内存映射文件授予的文件访问和操作权限。
                HandleInheritability.None, //指定内存映射文件的句柄能否由子进程继承的枚举值之一。 默认值为 None。
                false); //若为 true，则在关闭 MemoryMappedFile 后不释放 fileStream；若为 false，则释放 fileStream。

            ///这里，我使用了 MemoryMappedFile 类的一个特简单的构造函数。我们定义了要用到的流，然后给内存映射文件起了个名字。
            ///此外，我们还需要知道内存映射文件的大小(按字节计)和访问的类型。这样，我们就创建了一个内存映射文件。但是在开始使用它之前，
            ///我们还需要一个映射视图。接下来，我们就来创建它 :
            MemoryMappedViewAccessor FileMapView = memoryMappedFile.CreateViewAccessor();

            int number = Int32.MaxValue;
            Random rd = new Random();
            int rd1 = 0;//= rd.Next(10000);
            while (rd1 != 5000)
            {
                rd1 = rd.Next(10000);
                FileMapView.Write(0, rd1);
                Thread.SpinWait(500000);
            }
            FileMapView.Write(0, 5000);
            //FileMapView.Write<int>(4, ref number);
            //Console.WriteLine(FileMapView.ReadInt32(0));
            //int i = -1;
            //FileMapView.Read<int>(4, out i);
            //Console.WriteLine("P1 Struct is : {0}", i);
        }
        static void P2()
        {
            //Thread.SpinWait(500000);
            MemoryMappedFile memoryMappedFile = null;
            while (memoryMappedFile == null)
            {
                try
                {
                    memoryMappedFile = MemoryMappedFile.OpenExisting("MyMemoryMappedFile");
                }
                catch (FileNotFoundException)
                {
                    //CPU空转可以减少线程的上下文切换, 但是长时间空转挺废CPU的
                    Thread.SpinWait(5); //CPU消耗11%
                    //会引起上下文切换, 也就是切换到内核模式并等待. 但是不太耗CPU
                    //Thread.Sleep(1000); // CPU消耗0%
                    Console.WriteLine("Enviroment.ProcessorCount: {0}", Environment.ProcessorCount);
                }
            }
            
            using (MemoryMappedViewAccessor FileMap = memoryMappedFile.CreateViewAccessor())
            {
                while (FileMap.ReadInt32(0) != 5000)
                {
                    Console.WriteLine(FileMap.ReadInt32(0));
                    Thread.SpinWait(50000000);
                }
                Console.WriteLine(FileMap.ReadInt32(0));
                //int i = -1;
                //FileMap.Read<int>(4, out i);
                //Console.WriteLine("P2 Struct is : {0}", i);
            }
        }
        
    }
}
