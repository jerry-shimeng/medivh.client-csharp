using System.Collections.Generic;
using Medivh.DataStorage.OnceData.Counter;
using Medivh.Models;

namespace Medivh.DataStorage.OnceData
{
    /// <summary>
    /// 
    /// </summary>
    internal  abstract class OnceDataStorage : BaseDataStorage
    { 

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="model"></param>
        public static void AddData(BaseModel model)
        {
            OnceDataStorage obj = null;

            switch (model.CounterType)
            {
                case CounterTypeEnum.Nil:
                    break;
                case CounterTypeEnum.Business:
                    obj = new BizCounter();
                    break;
                case CounterTypeEnum.Custom:
                    obj = new CoustomCounter();
                    break;
                case CounterTypeEnum.Error:
                    obj = new ErrorCounter();
                    break;  
                default:
                    break;
            }
            if (obj != null)
            {
                obj.Add(model);
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static IList<BaseModel> GetData(CmdModel cmd)
        {
            OnceDataStorage obj = null;

            switch (cmd.CounterType)
            {
                case CounterTypeEnum.Nil:
                    break;
                case CounterTypeEnum.Business:
                    obj = new BizCounter();
                    break;
                case CounterTypeEnum.Custom:
                    obj = new CoustomCounter();
                    break;
                case CounterTypeEnum.Error:
                    obj = new ErrorCounter();
                    break; 
                default:
                    break;
            }
            if (obj != null)
            {
                return obj.Get(cmd);
            }
            return null;
        }


        public static void DelData(CmdModel cmd)
        {
            OnceDataStorage obj = null;

            switch (cmd.CounterType)
            {
                case CounterTypeEnum.Nil:
                    break;
                case CounterTypeEnum.Business:
                    obj = new BizCounter();
                    break;
                case CounterTypeEnum.Custom:
                    obj = new CoustomCounter();
                    break;
                case CounterTypeEnum.Error:
                    obj = new ErrorCounter();
                    break; 
                default:
                    break;
            }
            if (obj != null)
            {
                obj.Clear();
            } 
        }

         
    }
}
