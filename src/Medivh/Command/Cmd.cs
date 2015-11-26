﻿using System;
using System.Text;
using Medivh.Common;
using Medivh.DataStorage.OnceData;
using Medivh.DataStorage.SystemData;
using Medivh.Logger;
using Medivh.Models;

namespace Medivh.Command
{
    internal class Cmd
    { 
        /// <summary>
        /// 处理接受到的字节流
        /// </summary>
        /// <param name="buffers"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte[] ProcessCmd(byte[] buffers, int len)
        {
            try
            {
                //移除最后一位
                string msg = Encoding.UTF8.GetString(buffers, 0, len - 1);

                if (msg.Length == 0)
                {
                    return new byte[] { 10 };
                }
                //Console.Write(msg);
                var model = msg.JosnStringToObject<CmdModel>();
                Console.WriteLine("[msg]" + msg);
                if (model != null)
                {
                    var r = ProcessCmd(model);
                    if (r == null)
                    {
                        return Encoding.UTF8.GetBytes("o\n");
                    }
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(r);
                    
                    return Encoding.UTF8.GetBytes(json + "\n");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("cmd convert error :" + ex.Message);
            }
            return null;
        }

        private static object ProcessCmd(CmdModel model)
        {
            object obj = null;

            switch (model.ModuleType)
            {
                case ModuleTypeEnum.OnceData:
                    if (model.Operate == "clear")
                    {
                        OnceDataStorage.DelData(model);
                        obj = "exec is ok";
                    }
                    else
                    {
                        obj = OnceDataStorage.GetData(model);
                    }
                    break;
                case ModuleTypeEnum.StatusData:
                    break;
                case ModuleTypeEnum.SystemData:
                    obj = SystemDataStorage.GetSystemInfo();
                    break;
                case ModuleTypeEnum.TransactionData:
                    break;
                default:
                    break;
            }

            return obj;
        }
    }
}