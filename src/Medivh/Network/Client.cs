using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Medivh.Command;
using Medivh.Logger;

namespace Medivh.Network
{
    internal class Client
    {
        public void Run(string ip, int port, string info)
        {
            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(info) || port <= 0)
            {
                LogHelper.Error("网络模块初始化异常，请检查参数:ip port clientinfo");
                return;
            }
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            byte[] buffer = new byte[1024 * 1024];
            int len = 0;
            try
            {
                socket.Connect(IPAddress.Parse(ip), port);
                LogHelper.Info("连接服务器成功！");
                //socket.Send(Encoding.UTF8.GetBytes("hello world!\n"));
                //发送消息
                socket.Send(Encoding.UTF8.GetBytes(info + "\n"));
                Process(socket, buffer);
            }
            catch (SocketException ex)
            {
                LogHelper.Info("连接服务器异常,5秒后重试");
                socket.Close();
                socket.Dispose();
                Thread.Sleep(5000);
                Run(ip, port, info);
            }

        }

        private static void Process(Socket socket, byte[] b)
        {
            int len = 0;
            while (true)
            {
                len = socket.Receive(b);
                if (len == 0)
                {
                    throw new SocketException(-1);
                }
                if (len <= 2)
                {
                    //反馈消息，不处理
                }else if (len == 4)
                {
                    //结束信号
                    throw new SocketException(-1);
                }
                else
                {

                    byte[] buffer = null;

                    //消息处理
                    try
                    {
                        buffer = Cmd.ProcessCmd(b, len);
                    }
                    catch (Exception ex)
                    {
                        buffer = Encoding.UTF8.GetBytes("[Exception]" + ex.Message + "\n");
                    }

                    if (buffer != null)
                    {
                        socket.Send(buffer);
                    }
                    else
                    {
                        socket.Send(Encoding.UTF8.GetBytes("cmd exec error\n"));
                    }
                }
                Array.Clear(b, 0, len);
            }
        }
    }
}
