using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Medivh.Command;
using Medivh.Logger;

namespace Medivh.Network
{
    internal class Client
    {
        private static int count = 0;

        private Socket _socket = null;
        string _ip = String.Empty;
        private int _port = 0;
        string _info =String.Empty;
        public void Run(string ip, int port, string info)
        {
            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(info) || port <= 0)
            {
                LogHelper.Error("网络模块初始化异常，请检查参数:ip port clientinfo");
                return;
            }

            _ip = ip;
            _port = port;
            _info = info;

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            byte[] buffer = new byte[1024 * 1024];
            int len = 0;
            try
            {
                _socket.Connect(IPAddress.Parse(ip), port);
                LogHelper.Info("连接服务器成功！" + count++);
                //socket.Send(Encoding.UTF8.GetBytes("hello world!\n"));
                //发送消息
                _socket.Send(Encoding.UTF8.GetBytes(info + "\n"));
                HeartBeatAsnyc();
                Process(_socket, buffer);
            }
            catch (SocketException ex)
            {
                ReConnection();
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

        private  void ReConnection()
        {
            LogHelper.Info("连接服务器异常,5秒后重试");
            _socket.Close();
            _socket.Dispose();
            Thread.Sleep(5000);
            Run(_ip, _port, _info);
        }

        private void HeartBeatAsnyc()
        {
            if (_socket!=null && _socket.Connected == true)
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        try
                        {
                            if (_socket != null && _socket.Connected == true)
                                _socket.Send(Encoding.UTF8.GetBytes("0\n"));
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error(ex);
                        }
                        Thread.Sleep(5 * 1000); 
                    }
                });
            }
        }
    }
}
