using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Medivh;
using Medivh.Models;

namespace MedivhTest
{
    class Demo
    {
        public static void Run()
        {
            //初始化日志记录器
            MedivhSdk.SetLogger(Log, 1);
            //初始化medivh引擎
            MedivhSdk.Init(new ClientInfo() { AppName = "消息中心监控测试NO1", AppKey = "aaaaaaaaaaaaaaaaaa", AppSecret = "..." }, "192.168.155.239", 5000);

            //添加测试数据

            Test();
        }


        static Random rdm = new Random();
        static void Test()
        {
            while (true)
            {
                var a = rdm.Next(1, 30);
                //业务计数器
                MedivhSdk.OnceCounter.BusinessCounter("业务" + a, 1, a % 30);
                ////自定义计数器
                MedivhSdk.OnceCounter.CustomCounter("自定义" + a, a % 30);
                //Console.Write(". ");
                Thread.Sleep(a * 1000);
            }
        }
        static string Action()
        {
            return "0";
        }
        static void Log(string msg)
        {
            msg = string.Format("{0}:{1} ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg);
            Console.WriteLine(msg);
        }

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
    }
}
