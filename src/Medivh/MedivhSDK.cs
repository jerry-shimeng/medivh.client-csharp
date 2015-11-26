using System;
using System.Configuration;
using System.Threading.Tasks;
using Medivh.Config;
using Medivh.Logger;
using Medivh.Models;
using Medivh.Network;
using Medivh.SdkMain;

namespace Medivh
{
    public class MedivhSdk
    {
        /// <summary>
        /// 设置日志记录
        /// </summary>
        /// <param name="action"></param>
        /// <param name="logLevel">0 product 1debug</param>
        public static void SetLogger(Action<string> action,uint logLevel = 0)
        {
            LogHelper.Init(action, logLevel);
        }
        /// <summary>
        /// 类型计数器
        /// </summary>
        public static CounterClass OnceCounter { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init(ClientInfo info, string ip, int port)
        {
            LogHelper.Info("Medivh starting！");

            string json = String.Empty;
            if (info != null)
            {
                json = Newtonsoft.Json.JsonConvert.SerializeObject(info);
            }

            Task.Run(() => { new Client().Run(ip, port, json); });

            LogHelper.Info("Medivh success！");

            OnceCounter = new CounterClass();
        }

        public static void Init(MedivhConfig config)
        {
            if (config == null)
            {
                throw new Exception("param config is null");
            }
            Init(config.Client, config.ServerIp, config.ServerPort);
        }
    }
}
