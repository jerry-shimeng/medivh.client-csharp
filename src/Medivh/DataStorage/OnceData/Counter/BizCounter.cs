using System;
using System.Collections.Generic;
using Medivh.Content;
using Medivh.Models;

namespace Medivh.DataStorage.OnceData.Counter
{
    //业务计数器
    internal class BizCounter:OnceDataStorage
    {
        private static DataCache cache = new DataCache(ModuleTypeEnum.OnceData, CounterTypeEnum.Business);
        public override void Add(BaseModel model)
        {
            if (model == null)
            {
                return;
            }
            cache.Add(model);
        }

//        public override IList<BaseModel> Get(Predicate<BaseModel> match)
//        {
//            return cache.Get(match);
//        }
//         

        public override DataCache GetDataCache()
        {
            return cache;
        }
    }
}
