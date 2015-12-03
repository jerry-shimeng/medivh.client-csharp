using System;
using System.Net.Sockets;
using Medivh;
using Medivh.Models;

namespace MedivhTest
{
    class Program
    {


        static void Main(string[] args)
        {
            MedivhSdk.SetLogger(Log);

            MedivhSdk.Init(new ClientInfo() { AppName = "消息中心监控测试NO1", AppKey = "aaaaaaaaaaaaaaaaaa" }, "192.168.155.239", 5000);

            Test();
            Console.WriteLine("over");
            Console.ReadKey();
        }

        static void Test()
        {
            for (int i = 0; i < 1000; i++)
            {
                Exception ex = null;
                try
                {
                    Ex();
                }
                catch (Exception ex1)
                {
                    ex = ex1;
                }
                var a = rdm.Next(1, 15);
                if (ex != null)
                {
                    MedivhSdk.OnceCounter.ErrorCounter("text", ex, 1, i % a);
                }
                MedivhSdk.OnceCounter.BusinessCounter("test", a.ToString(), 1, i * 2 % a);

                MedivhSdk.OnceCounter.CustomCounter("mq send succeess" + i % 5, i * 2 % a, 1, "notify", "error");

                MedivhSdk.HeartBeat.Add(new HeartBeatModel() { Mark = "test" + i % 5, Level = i * 2 % a, Action = Action });
            }

        }
        static Random rdm = new Random();
        static void Ex()
        {
            var a = rdm.Next(15);
            switch (a)
            {
                case 0:
                    throw new SocketException(100);
                case 1:
                    throw new Exception();
                case 2:
                    throw new MyEx1();
                case 3:
                    throw new MyEx3();
                case 4:
                    throw new MyEx4();
                case 5:
                    throw new MyEx5();
                case 6:
                    throw new MyEx6();
                case 7:
                    throw new MyEx7();
                case 8:
                    throw new MyEx8();
                case 9:
                    throw new MyEx9();
                case 10:
                    throw new MyEx19();
                case 11:
                    throw new MyEx6a9();
                case 12:
                    throw new MyEx29();
                case 13:
                    throw new MyEx39();
                case 14:
                    throw new MyEx49();
                case 15:
                    throw new MyEx59();
            }

        }


        static object Action()
        {
            return new { code = 0, data = DateTime.Now };
        }

        static void Log(string msg)
        {
            Console.WriteLine(msg);
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
