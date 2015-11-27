using System;
using Medivh.Models;

namespace Medivh.SdkMain
{
    public class CounterClass
    {
        static CounterStorageMain storage = new CounterStorageMain();


        /// <summary>
        /// 错误计数器
        /// </summary>
        /// <param name="mark"></param>
        /// <param name="ex"></param>
        /// <param name="num"></param>
        /// <param name="level"></param>
        public void ErrorCounter(string mark, Exception ex, int num = 1, int level = 0)
        {
            if (ex == null)
            {
                return;
            }
            try
            {
                storage.SaveCounter(mark, CounterTypeEnum.Error, ex.GetType().FullName, num, level);
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
        public void BusinessCounter(string mark, string busName, int num = 1, int level = 0)
        {
            try
            {
                storage.SaveCounter(mark, CounterTypeEnum.Business, busName, num, level);
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
                storage.SaveCounter(mark, CounterTypeEnum.Custom, null, num, level);
            }
            catch (Exception ex)
            {
                Logger.LogHelper.Error(ex);
            }
        }
    }
}
