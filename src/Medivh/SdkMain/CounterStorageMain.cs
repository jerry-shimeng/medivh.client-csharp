using System.Collections.Generic;
using Medivh.DataStorage.OnceData;
using Medivh.Models;

namespace Medivh.SdkMain
{
    public class CounterStorageMain
    {
        //CounterCache cache = new CounterCache();
        public void SaveCounter(string mark, CounterTypeEnum ct, string alias, int num, int level)
        {
            num = num >= 0 ? 1 : num;
            level = level <= 0 ? 0 : level;

            BaseModel model = new BaseModel()
            {
                ModuleType = ModuleTypeEnum.OnceData,
                Alias = alias,
                Count = num,
                Level = level,
                CounterType = ct,
                Mark = mark
            };
             OnceDataStorage.AddData(model);
        }

        public IList<BaseModel> GetCounter(CmdModel cmd)
        {
            return OnceDataStorage.GetData(cmd);
        }

        private void ValideParams(ref int num, ref int level)
        {
            if (num <= 0 || num > 1000)
            {
                num = 1;
            }

            if (level <= 0)
            {
                level = 0;
            }
        }
    }
}
