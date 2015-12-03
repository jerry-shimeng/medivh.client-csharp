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

        private HeartBeatDataStorage()
        {

        }
        public static HeartBeatDataStorage Instance()
        {
            return storage;
        }

        private static DataCache cache = new DataCache();
        public override void Add(BaseModel model)
        {
            cache.Add(model);
        }

        public override IList<BaseModel> Get(Predicate<BaseModel> match)
        {
            return cache.Get(match);
        }

        public override object Clear()
        {
            return cache.Clear();
        }

        public override object ExecCmd(CmdModel model)
        {
            if (model.Operate.Equals("clear"))
            {
                return Clear();
            }
            else if (model.Operate.Equals("types"))
            {
                return GetTypes();
            }
            else if (model.Operate.Equals("level"))
            {
                return GetLevel();
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

        private IList<BaseModel> GetLevel()
        {
            var list = cache.GetAll().ToList();
            var slist = list.Select(x => x.Level).Distinct().ToList();
            if (slist.Count == 0)
            {
                return new List<BaseModel>();
            }
            return slist.Select(s => new BaseModel() { Level = s }).ToList();
        }

        private IList<BaseModel> GetTypes()
        {
            var list = cache.GetAll().ToList();
            var slist = list.Select(x => x.Mark).Distinct().ToList();
            if (slist.Count == 0)
            {
                return new List<BaseModel>();
            }
            return slist.Select(s => new BaseModel() { Mark = s }).ToList();
        }
    }
}
