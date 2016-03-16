using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Medivh.Common;
using Medivh.Content;
using Medivh.Logger;
using Medivh.Models;
using Medivh.Network;

namespace Medivh.DataPush
{
    internal class PushClient
    {
        internal void RunAync()
        {
            Task.Run(() =>
            {
                //启动job 推送数据
                Run();
            });
        }

        private void Run()
        {
            while (true)
            {
                Thread.Sleep(100 * 60);
                //获取数据
                var list = CacheCenter.GetAndClear();
                if (list != null)
                {
                    LogHelper.Debug("本次推送消息数量：" + list.Count + ",数据总量：" + list.Sum(x => x.Count));
                    Push(list);
                }
            }
        }

        //消息分割推送
        private void Push(IList<BaseModel> list)
        {

            if ( list == null || list.Count == 0)
            {
                return;
            }

            var len = 10;

            if (list.Count <= len)
            {
                BeginPush(list);
            }
            else
            {
                var t = new List<BaseModel>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (i % len == 0 && i != 0)
                    {
                        BeginPush(t);
                        t.Clear();
                    }
                    t.Add(list[i]);
                }

                if (t.Count > 0)
                {
                    BeginPush(t);
                    t.Clear();
                }
            }
        }
        //开始推送
        private bool BeginPush(IList<BaseModel> list)
        {
            var json = JsonHelper.JsonObjectToString(list);

            bool result = false;

            //获取当前连接状态
            if (TcpClient.GetSocketStatus())
            {
                try
                {
                    result = TcpClient.Send(json) > 0;
                }
                catch (Exception ex)
                {
                    LogHelper.Error(ex);
                }
            }
            var config = ConfigHelper.GetMedivhConfig();

            if (!result && !string.IsNullOrWhiteSpace(config?.ApiPostUrl))
            {
                string response = null;
                //走http
                try
                {
                    result = HttpHelper.Post(json, config.ApiPostUrl, out response);
                }
                catch (Exception ex)
                { 
                     LogHelper.Error(ex);
                }

                LogHelper.Debug($"本次发送结果: {result} , {response}");
            }
            if (!result)
            {
                //发送失败，入下次发送的队列 
                CacheCenter.Set(list);
            }
            return result;
        }
    }
}
