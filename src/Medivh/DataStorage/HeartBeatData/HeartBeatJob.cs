using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Medivh.Models;

namespace Medivh.DataStorage.HeartBeatData
{
    /// <summary>
    /// 心跳数据任务类
    /// 主要做周期性的数据采集，采集完毕的数据加入到HeartBeatDataStorage中
    /// </summary>
    internal class HeartBeatJob
    {
        private System.Threading.Timer timer;
        private static HeartBeatJob job = new HeartBeatJob();
        private Queue<HeartBeatModel> funcQueue = new Queue<HeartBeatModel>();

        private long dueTime = 5 * 1000;
        private long period = 10 * 1000;
        Stopwatch swatch = new Stopwatch();
        private HeartBeatJob()
        {
            timer = new Timer(new TimerCallback(Action), this, dueTime, period);
        }

        private void Action(object state)
        {
            var len = funcQueue.Count;
            Console.WriteLine("[queue] :" + len);
            if (len <= 0)
            {
                return;
            }
            else
            {
                //先清空历史数据
                HeartBeatDataStorage.Instance().Clear();

                for (int i = 0; i < len; i++)
                {
                    var t = funcQueue.Dequeue();
                    if (t != null)
                    {
                        swatch.Start();
                        var r = t.Action();
                        swatch.Stop(); 
                        BaseModel model = new BaseModel() { Mark = t.Mark, ModuleType = ModuleTypeEnum.HeartBeatData, 
                            Level = t.Level, Alias = t.Alias, Other = t.Other, Result = r ,RunTime = swatch.Elapsed.TotalMilliseconds};
                        swatch.Reset();
                        HeartBeatDataStorage.Instance().Add(model);

                        //重新归队
                        Add(t);
                    }
                }
            }
        }

        public static HeartBeatJob Instance()
        {
            return job;
        }

        public void Add(HeartBeatModel model)
        {
            if (model == null)
            {
                return;
            }
            funcQueue.Enqueue(model);
        }

        /// <summary>
        /// 获取执行间隔
        /// </summary>
        /// <returns></returns>
        public long GetPeriod()
        {
            return period;
        }

        /// <summary>
        /// 改变执行间隔
        /// </summary>
        /// <param name="p"></param>
        public string Change(long p)
        {
            period = p;
            timer.Change(dueTime, period);
            return "change is success";
        }
    }
}
