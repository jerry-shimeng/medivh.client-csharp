using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Medivh.Models;

namespace Medivh.Content
{
    /// <summary>
    /// 数据存储的结构
    /// </summary>
    internal class DataCache
    {
        Cache cache = new Cache();
        internal void Add(BaseModel model)
        {
            if (model != null)
            {
                cache.Set(model);
            }
        }

        internal IList<BaseModel> Get(Predicate<BaseModel> match)
        {
            if (match == null)
            {
                return null;
            }
            return cache.Get(match);
        }
        internal void Clear()
        {
            cache.Clear();
        }

        internal List<BaseModel> GetAll()
        {
            return cache.GetAll();
        }

        private class Cache
        {
            private List<BaseModel> list = new List<BaseModel>();
            public IList<BaseModel> Get(Predicate<BaseModel> match)
            {
                return list.FindAll(match);
            }
            public void Clear()
            {
                list.Clear();
            }

            public List<BaseModel> GetAll()
            {
                return list;
            }
            public void Set(BaseModel model)
            {
                switch (model.ModuleType)
                {
                    case ModuleTypeEnum.OnceData:
                        AddCounter(model);
                        break;
                }
            }

            private void AddCounter(BaseModel model)
            {
                list.Add(model);
                
            }

        }
    }
}
