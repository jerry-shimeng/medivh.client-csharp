using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medivh.Models;

namespace Medivh.Config
{
    public class MedivhConfig
    {

        public ClientInfo Client { get; set; }
        public string ServerIp { get; set; }
        public int ServerPort { get; set; }

        private static int maxHeartBeatCount = 100;

        /// <summary>
        /// 心跳队列最大数量
        /// </summary>
        public static int MaxHeartBeatCount
        {
            get { return maxHeartBeatCount; }
            set { maxHeartBeatCount = value; }
        }



        private static int connectionInterval = 10;

        /// <summary>
        /// 心跳保持间隔 最小5秒 最大5分钟 单位秒
        /// </summary>
        public static int ConnectionInterval
        {
            get { return connectionInterval; }
            set
            {
                if (value <= 5)
                {
                    connectionInterval = 5;
                }
                else if (value >= 5 * 60)
                {
                    connectionInterval = 300;
                }
                else
                {
                    connectionInterval = value;
                }

            }
        }

        /// <summary>
        /// 重置重连间隔
        /// </summary>
        public static void ResetConnectionInterval()
        {
            connectionInterval = 10;
        }

        private int maxOnceDataCount = 1000;
        /// <summary>
        /// 一次性数据最大数量
        /// </summary>
        public int MaxOnceDataCount
        {
            get { return maxOnceDataCount; }
            set { maxOnceDataCount = value; }
        }

    }
}
