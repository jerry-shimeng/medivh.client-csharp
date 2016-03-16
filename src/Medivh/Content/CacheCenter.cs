using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medivh.Common;
using Medivh.Models;

namespace Medivh.Content
{
    /// <summary>
    /// 缓存中心
    /// </summary>
    internal class CacheCenter
    {
        static List<BaseModel> list = new List<BaseModel>();

        static Dictionary<string, BaseModel> dict = new Dictionary<string, BaseModel>();

        /// <summary>
        /// 缓存里面加入数据
        /// </summary>
        /// <param name="model"></param>
        public static void Set(BaseModel model)
        {
            //分解数据。 先获取对象对应的hashkey 
            string key = GetKey(model);
            //判断dict中是否有该key

            lock ("Medivh.Content.CacheCenter.operation")
            {
                if (dict.Keys.Any(x => x.Equals(key)))
                {
                    //直接获取对象 总数量+count
                    var m = dict[key];
                    if (m != null)
                    {
                        m.Count += model.Count;
                    }
                    else
                    {
                        dict[key] = model;
                        list.Add(model);
                    }
                }
                else
                {
                    dict.Add(key, model);
                    list.Add(model);
                }
            }
        }

        /// <summary>
        /// 获取并清除这部分数据
        /// </summary>
        /// <returns></returns>
        public static IList<BaseModel> GetAndClear(ModuleTypeEnum module, CounterTypeEnum counter)
        {
            IList<BaseModel> r = new List<BaseModel>();

            lock ("Medivh.Content.CacheCenter.operation")
            {
                r = list.Where(x => x.ModuleType == module && x.CounterType == counter).ToList();
                list.RemoveAll(x => x.ModuleType == module && x.CounterType == counter);

                //删除dict里面的数据
                foreach (BaseModel baseModel in r)
                {
                    dict.Remove(GetKey(baseModel));
                }
            }
            return r;
        }



        /// <summary>
        /// 获取并清除全部数据
        /// </summary>
        /// <returns></returns>
        public static IList<BaseModel> GetAndClear()
        {
            IList<BaseModel> r = null;

            lock ("Medivh.Content.CacheCenter.operation")
            {
                r = list;
                list = new List<BaseModel>(); ;

                //删除dict里面的数据
                dict = new Dictionary<string, BaseModel>(); 
            }
            return r;
        }

        internal static void Set(IList<BaseModel> list)
        {
            if (list == null || list.Count == 0)
            {
                return;
            }
            else
            {
                foreach (var model in list)
                {
                    Set(model);
                }
            }
        }

        private static string GetKey(BaseModel model)
        {
            var sb = new StringBuilder(100);
            sb = sb.Append(model.Mark).Append(":")
                .Append(model.Level).Append(":")
                .Append(model.CreateTime).Append(":")
                .Append(model.ModuleType).Append(":")
                .Append(model.CounterType).Append(":")
                .Append(model.Result);

            return sb.ToString();
            //            return string.Format("{0}:{1}:{2}:{3}:{4}:{5}", model.Mark, model.Level, model.CreateTime, model.ModuleType,
            //                model.CounterType, model.Result);

        }

//        public static IList<BaseModel> Get(Predicate<BaseModel> match)
//        {
//            return list.FindAll(match);
//        }
 
    }
}
