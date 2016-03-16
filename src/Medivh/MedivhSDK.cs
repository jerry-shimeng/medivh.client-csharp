using System;
using System.Configuration;
using System.Threading.Tasks;
using Medivh.Common;
using Medivh.Config;
using Medivh.DataPush;
using Medivh.DataStorage.HeartBeatData;
using Medivh.Logger;
using Medivh.Models;
using Medivh.Network;
using Medivh.SdkMain;

namespace Medivh
{
    public class MedivhSdk
    {
        public static ClientInfo _ClientInfo { get; private set; }
        /// <summary>
        /// 设置日志记录
        /// </summary>
        /// <param name="action"></param>
        /// <param name="logLevel">0 product 1debug</param>
        public static void SetLogger(Action<string> action, uint logLevel = 0)
        {
            LogHelper.Init(action, logLevel);
        }


        /// <summary>
        /// 类型计数器
        /// </summary>
        public static CounterStorageMain OnceCounter { get; set; }

        /// <summary>
        /// 心跳数据
        /// </summary>
        public static HeartBeatStorageMain HeartBeat { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        private static void Init(ClientInfo info, string ip, int port)
        {
            _ClientInfo = info;
            LogHelper.Info("Medivh starting！");
            try
            {
                Init();

                string json = String.Empty;
                if (info != null)
                {
                    json = JsonHelper.JsonObjectToString(info);
                }
                //启动网络连接
                TcpClient.RunAync(ip, port, json);

                //启动消息推送
                new PushClient().RunAync();

                LogHelper.Info("Medivh success！");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                LogHelper.Info("Medivh error！");
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="config"></param>
        public static void Init(MedivhConfig config)
        {
            if (config == null)
            {
                throw new Exception("param config is null");
            }
            ConfigHelper.SetMedivhConfig(config);
            Init(config.Client, config.ServerIp, config.ServerPort);
        }

        private static void Init()
        {
            OnceCounter = new CounterStorageMain();
            HeartBeat = new HeartBeatStorageMain();
        }
    }
}
