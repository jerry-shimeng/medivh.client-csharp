using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Medivh.Command;
using Medivh.Config;
using Medivh.Logger;

namespace Medivh.Network
{
    /// <summary>
    /// 心跳数据是 0\n
    /// 所有数据传输结尾都使用 \n 所以消息体内避免使用\n
    /// </summary>
    internal class TcpClient
    {

        private static Socket socket = null;
        static string  ip = String.Empty;
        private static int port = 0;
        static string info = String.Empty;
        public static bool RunAync(string _ip, int _port, string _info)
        {
            ip = _ip;
            port = _port;
            info = _info;

            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(info) || port <= 0)
            {
                LogHelper.Error("init error，please check the params :ip port clientinfo");
                return false;
            }

            ConnectionServer();
            //开启心跳
            HeartBeatAsnyc();

            return true;
        }

        private static async Task ConnectionServer()
        { 
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            byte[] buffer = new byte[1024 * 1024]; 
            try
            {
                socket.Connect(IPAddress.Parse(ip), port);
                LogHelper.Info("connection service success！");

                //socket.Send(Encoding.UTF8.GetBytes("hello world!\n"));
                //发送消息 
                Send(info);

                //连接处理
                await ProcessAync(socket, buffer);

                MedivhConfig.ResetConnectionInterval();

            }
            catch (SocketException ex)
            {
                LogHelper.Error(ex);
                MedivhConfig.ConnectionInterval += 1;
            }
        }


        internal static int Send(string msg)
        {
            if (msg.LastIndexOf('\n') < msg.Length-1)
            {
                msg += "\n";
            }
            if (socket!=null && socket.Connected)
            {
                return socket.Send(Encoding.UTF8.GetBytes(msg));
            }
            else
            {
                throw new SocketException();
            }

        }

        internal static bool GetSocketStatus()
        {
            return socket != null && socket.Connected;
        }


        private static async  Task ProcessAync(Socket socket, byte[] b)
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    var len = socket.Receive(b);
                    if (len == 0)
                    {
                        throw new SocketException(-1);
                    }
                    if (len <= 2)
                    {
                        //心跳消息包，不处理
                    }
                    else if (len == 4)
                    {
                        //结束信号
                        throw new SocketException(-1);
                    }
                    else
                    {

//                        byte[] buffer = null;
//                        try
//                        {
//
//                            //消息处理
//                            try
//                            {
//                                buffer = Cmd.ProcessCmd(b, len);
//                            }
//                            catch (Exception ex)
//                            {
//                                buffer = Encoding.UTF8.GetBytes("[Exception]" + ex.Message + "\n");
//                            }
//
//                            if (buffer != null)
//                            {
//                                socket.Send(buffer);
//                            }
//                            else
//                            {
//                                socket.Send(Encoding.UTF8.GetBytes("cmd exec error\n"));
//                            }
//                        }
//                        catch (Exception ex)
//                        {
//                            LogHelper.Error(ex, "send resultset error");
//                            if (buffer != null)
//                                LogHelper.Info($"[resultset]   {Encoding.UTF8.GetString(buffer)}");
//                        }
                    }
                    Array.Clear(b, 0, len);
                }

            });
        }

        private static void ReConnection()
        { 
            LogHelper.Info(string.Format("reconnection service , interval {0} seconds", MedivhConfig.ConnectionInterval));
            socket.Close();
            socket.Dispose();
            ConnectionServer();
        }

        //        private System.Threading.Timer timersTimer;
        private static async Task HeartBeatAsnyc()
        { 
           await Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(MedivhConfig.ConnectionInterval * 1000);
                    TimerElapsed();
                }
            });
        }

        private static int lastTime = MedivhConfig.ConnectionInterval;
        static void TimerElapsed()
        {
            try
            {
                if (socket != null && socket.Connected == true)
                {
                    socket.Send(Encoding.UTF8.GetBytes("0\n"));
                }
                else
                {
                    ReConnection();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            finally
            {
                if (lastTime != MedivhConfig.ConnectionInterval)
                {
                    //                    timersTimer.Change(MedivhConfig.ConnectionInterval * 1000, MedivhConfig.ConnectionInterval * 1000);
                }
            }
        }
    }
}
