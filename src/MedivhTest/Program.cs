using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using Medivh;
using Medivh.Models;

namespace MedivhTest
{
    class Program
    {


        static void Main(string[] args)
        { 

            Demo.Run();

            //UsingProcess(); 
            Console.ReadKey();
        }


        static void UsingProcess()
        {
            using (var pro = Process.GetCurrentProcess())
            {
                //间隔时间（毫秒）
                int interval = 1000;
                //上次记录的CPU时间
                var prevCpuTime = TimeSpan.Zero;
                while (true)
                {
                    //当前时间
                    var curTime = pro.TotalProcessorTime;
                    //间隔时间内的CPU运行时间除以逻辑CPU数量
                    var value = (curTime - prevCpuTime).TotalMilliseconds / interval / Environment.ProcessorCount * 100;
                    prevCpuTime = curTime;
                    //输出
                    Console.WriteLine(value);

                    Thread.Sleep(interval);
                }
            }
        }

  



       

       
    }

    class MyEx1 : Exception { }
    class MyEx2 : Exception { }
    class MyEx3 : Exception { }
    class MyEx4 : Exception { }
    class MyEx5 : Exception { }
    class MyEx6 : Exception { }
    class MyEx7 : Exception { }
    class MyEx8 : Exception { }
    class MyEx9 : Exception { }
    class MyEx19 : Exception { }
    class MyEx29 : Exception { }
    class MyEx39 : Exception { }
    class MyEx49 : Exception { }
    class MyEx59 : Exception { }
    class MyEx6a9 : Exception { }
    class MyEx69s : Exception { }
    class MyEx69d : Exception { }
    class MyEx69as : Exception { }
    class MyEx69z : Exception { }
    class MyEx69c : Exception { }
    class MyEx69f : Exception { }
}
