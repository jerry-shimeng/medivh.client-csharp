using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Medivh.Common;
using Medivh.Logger;
using Medivh.Models;
using Timer = System.Timers.Timer;

namespace Medivh.Content
{
    /// <summary>
    /// 数据存储的结构
    /// 获取数据单次有上限，上限为1000条
    /// </summary>
    public class DataCache
    {
        public ModuleTypeEnum ModuleType { get; set; }
        public CounterTypeEnum CounterType { get; set; }
        public DataCache(ModuleTypeEnum module, CounterTypeEnum counter)
        {
            this.ModuleType = module;
            this.CounterType = counter;
            //启动数据整合 ，根据小时整合
            AddByCondense();
        }

        private ConcurrentQueue<BaseModel> dataQueue = new ConcurrentQueue<BaseModel>(); 
        /// <summary>
        /// 添加数据
        /// 添加的数据如果超过设置的最大数量，则压缩数据，压缩方式是已小时为单位，合并现有数据
        /// </summary>
        /// <param name="model"></param>
        internal void Add(BaseModel model)
        {
            if (model != null)
            {
                AddQueue(model);
            }
        }

        internal void AddQueue(BaseModel model)
        {
            dataQueue.Enqueue(model);
        }

        /// <summary>
        /// 压缩数据的方式添加
        /// </summary> 
        internal void AddByCondense()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    BaseModel model;
                    if (GetOne(out model))
                    {
                        CacheCenter.Set(model);
                    }
                    else
                    {
                        Thread.Sleep(50);
                    }
                }
            }); 
        }


        #region 普通操作

//        /// <summary>
//        /// 废弃
//        /// </summary>
//        /// <param name="match"></param>
//        /// <returns></returns>
//        internal IList<BaseModel> Get(Predicate<BaseModel> match)
//        {
//            return CacheCenter.Get(match); 
//        }

//        /// <summary>
//        /// 获取并清空当前列表
//        /// 最大获取200条数据
//        /// </summary>
//        /// <returns></returns>
//        internal object GetAndClear()
//        {
//            return CacheCenter.GetAndClear(this.ModuleType, this.CounterType);
//        }

//        /// <summary>
//        /// 废弃
//        /// </summary>
//        /// <returns></returns>
//        internal List<BaseModel> GetAll()
//        {
//            return null;
//        }

        internal bool GetOne(out BaseModel model)
        {
            if (dataQueue.IsEmpty)
            {
                model = null;
                return false;
            }

            return dataQueue.TryDequeue(out model);
        }

        internal void Clear()
        {
            BaseModel item;
            while (dataQueue.TryDequeue(out item))
            {
                // do nothing
            }
        }
        #endregion


    }
}
