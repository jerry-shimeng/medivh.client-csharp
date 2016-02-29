using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medivh.Content;
using Medivh.Models;

namespace Medivh.DataStorage.HeartBeatData
{
    internal class HeartBeatDataStorage : BaseDataStorage
    {
        static HeartBeatDataStorage storage = new HeartBeatDataStorage();
        static List<BaseModel> cache = new List<BaseModel>(); 
        private HeartBeatDataStorage()
        {

        }
        public static HeartBeatDataStorage Instance()
        {
            return storage;
        } 
        public override void Add(BaseModel model)
        {
            cache.Add(model);
        }

        public override IList<BaseModel> Get(Predicate<BaseModel> match)
        {
            return cache.FindAll(match);
        }
         
        public override object ExecCmd(CmdModel model)
        {
             
             if (model.Operate.Equals("types"))
            {
                return GetTypes();
            }
            else if (model.Operate.Equals("level"))
            {
                return GetLevel(model.Mark);
            }
            else if (model.Operate.Equals("period"))
            {
                return HeartBeatJob.Instance().GetPeriod();
            }
            else if (model.Operate.Equals("change"))
            {
                long t = 0;
                try
                {
                    t = long.Parse(model.Extend.ToString());
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                if (t<=5000)
                {
                    return "the min period is 5(s)";
                }
                return HeartBeatJob.Instance().Change(t);
            }
            else
            {
                return Get(model);
            }
        }

        private IList<BaseModel> GetLevel(string mark)
        {
            var list = cache;
            var slist = list.Select(x => x.Level).Distinct().ToList();
            if (slist.Count == 0)
            {
                return new List<BaseModel>();
            }
            return slist.Select(s => new BaseModel() { Level = s }).ToList();
        }

        private IList<BaseModel> GetTypes()
        {
            var list = cache;
            var slist = list.Select(x => x.Mark).Distinct().ToList();
            if (slist.Count == 0)
            {
                return new List<BaseModel>();
            }
            return slist.Select(s => new BaseModel() { Mark = s }).ToList();
        }

        /// <summary>
        /// 心跳数据组合
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public override IList<BaseModel> Get(CmdModel cmd)
        {
            Predicate<BaseModel> match = null;
            if (string.IsNullOrWhiteSpace(cmd.Mark) )
            { 
                match = cmd.Level == 0 ? new Predicate<BaseModel>(x => x.ModuleType == cmd.ModuleType) : new Predicate<BaseModel>(x => x.ModuleType == cmd.ModuleType && x.Level == cmd.Level);
            }
            else if (!string.IsNullOrWhiteSpace(cmd.Mark))
            {
                match = cmd.Level == 0 ? new Predicate<BaseModel>(x => x.ModuleType == cmd.ModuleType && x.Mark == (cmd.Mark)) : new Predicate<BaseModel>(x => x.ModuleType == cmd.ModuleType && x.Level == cmd.Level && x.Mark == (cmd.Mark));
            }
            else if (string.IsNullOrWhiteSpace(cmd.Mark))
            {
                match = cmd.Level == 0 ? match = new Predicate<BaseModel>(x => x.ModuleType == cmd.ModuleType) : match = new Predicate<BaseModel>(x => x.ModuleType == cmd.ModuleType && x.Level == cmd.Level );
            }
            else
            {
                match = cmd.Level == 0 ? match = new Predicate<BaseModel>(x => x.ModuleType == cmd.ModuleType && x.Mark == (cmd.Mark)) : match = new Predicate<BaseModel>(x => x.ModuleType == cmd.ModuleType && x.Level == cmd.Level  && x.Mark == (cmd.Mark));
            }
            return Get(match);
        }

        public void Clear()
        {
            cache.Clear();
        }
    }
}
