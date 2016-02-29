using System;
using System.Collections.Generic;
using Medivh.DataStorage.OnceData;
using Medivh.Models;

namespace Medivh.SdkMain
{
    public class CounterStorageMain
    {

        /// <summary>
        /// 错误计数器
        /// </summary>
        /// <param name="mark"></param>
        /// <param name="ex"></param>
        /// <param name="num"></param>
        /// <param name="level"></param>
        public void ErrorCounter(Exception ex, int num = 1, int level = 0)
        {
            if (ex == null)
            {
                return;
            }
            try
            {
                SaveCounter(ex.GetType().FullName, CounterTypeEnum.Error, num, level);
            }
            catch (Exception ex1)
            {
                Logger.LogHelper.Error(ex1);
            }
        }


        /// <summary>
        /// 业务计数器
        /// </summary>
        /// <param name="mark"></param>
        /// <param name="busName"></param>
        /// <param name="num"></param>
        /// <param name="level"></param>
        public void BusinessCounter(string busName, int num = 1, int level = 0)
        {
            try
            {
                SaveCounter(busName, CounterTypeEnum.Business,  num, level);
            }
            catch (Exception ex)
            {
                Logger.LogHelper.Error(ex);
            }
        }

        /// <summary>
        /// 自定义计数器
        /// </summary>
        /// <param name="mark">数据类型</param>
        /// <param name="level"></param>
        /// <param name="num"></param>
        /// <param name="s"></param>
        public void CustomCounter(string mark, int level, int num = 1, params string[] s)
        {
            try
            {
                SaveCounter(mark, CounterTypeEnum.Custom, num, level);
            }
            catch (Exception ex)
            {
                Logger.LogHelper.Error(ex);
            }
        }

         
        private void SaveCounter(string mark, CounterTypeEnum ct, int num, int level)
        {
            num = num >= 0 ? 1 : num;
            level = level <= 0 ? 0 : level;

            BaseModel model = new BaseModel()
            {
                ModuleType = ModuleTypeEnum.OnceData,
                Count = num,
                Level = level,
                CounterType = ct,
                Mark = mark
            };
             OnceDataStorage.AddData(model);
        }
    }
}
