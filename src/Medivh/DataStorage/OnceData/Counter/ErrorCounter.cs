using System;
using System.Collections.Generic;
using Medivh.Content;
using Medivh.Models;

namespace Medivh.DataStorage.OnceData.Counter
{
    /// <summary>
    /// 错误计数器
    /// 错误计数器记录数据：异常类型，异常时间，异常级别
    /// </summary>
    internal class ErrorCounter : OnceDataStorage
    {
        private static DataCache cache = new DataCache(ModuleTypeEnum.OnceData, CounterTypeEnum.Error);
        public override void Add(BaseModel model)
        {
            model.Count = 1;
            cache.Add(model);
        }

//        public override IList<BaseModel> Get(Predicate<BaseModel> match)
//        {
//            return cache.Get(match);
//        }

         
        public override DataCache GetDataCache()
        {
            return cache;
        }
    }
}
