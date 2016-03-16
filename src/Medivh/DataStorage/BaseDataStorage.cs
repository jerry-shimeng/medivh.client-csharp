using System;
using System.Collections.Generic;
using Medivh.Contract;
using Medivh.Models;

namespace Medivh.DataStorage
{
    internal abstract class BaseDataStorage : IDataStorageable
    {
        public abstract void Add(BaseModel model);

//        public abstract IList<BaseModel> Get(Predicate<BaseModel> match);

//        public virtual IList<BaseModel> Get(CmdModel cmd)
//        {
//            Predicate<BaseModel> match = null;
//            if (string.IsNullOrWhiteSpace(cmd.Mark))
//            { 
//                match = cmd.Level == 0 ? new Predicate<BaseModel>(x => x.CounterType == cmd.CounterType) : new Predicate<BaseModel>(x => x.CounterType == cmd.CounterType && x.Level == cmd.Level);
//            }
//            else if (!string.IsNullOrWhiteSpace(cmd.Mark) )
//            {
//                match = cmd.Level == 0 ? new Predicate<BaseModel>(x => x.CounterType == cmd.CounterType && x.Mark == (cmd.Mark)) : new Predicate<BaseModel>(x => x.CounterType == cmd.CounterType && x.Level == cmd.Level && x.Mark == (cmd.Mark));
//            }
//            else if (string.IsNullOrWhiteSpace(cmd.Mark) )
//            {
//                match = cmd.Level == 0 ? match = new Predicate<BaseModel>(x => x.CounterType == cmd.CounterType ) : match = new Predicate<BaseModel>(x => x.CounterType == cmd.CounterType && x.Level == cmd.Level );
//            }
//            else
//            {
//                match = cmd.Level == 0 ? match = new Predicate<BaseModel>(x => x.CounterType == cmd.CounterType  && x.Mark == (cmd.Mark)) : match = new Predicate<BaseModel>(x => x.CounterType == cmd.CounterType && x.Level == cmd.Level  && x.Mark == (cmd.Mark));
//            }
//            return Get(match);
//        }

         
         

//        public abstract object ExecCmd(CmdModel model);
         

    }
}
