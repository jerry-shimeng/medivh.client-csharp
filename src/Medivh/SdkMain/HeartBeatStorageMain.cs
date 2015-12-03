using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medivh.DataStorage.HeartBeatData;
using Medivh.Logger;
using Medivh.Models;

namespace Medivh.SdkMain
{
    public class HeartBeatStorageMain
    {


        public void Add(HeartBeatModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Mark) || model.Action == null)
            {
                return;
            }
            try
            {
                HeartBeatJob.Instance().Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }
    }
}
