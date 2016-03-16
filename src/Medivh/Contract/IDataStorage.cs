using System;
using System.Collections.Generic;
using Medivh.Models;

namespace Medivh.Contract
{
    internal interface IDataStorageable
    {
        void Add(BaseModel model);

//        IList<BaseModel> Get(Predicate<BaseModel> match);
    }
}
