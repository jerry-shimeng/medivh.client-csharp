using System;
using System.Collections.Generic;
using System.Threading;
using Medivh.Common;

namespace Medivh.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            this.CreateTime = DateTime.UtcNow.Unix();
        }
        public ModuleTypeEnum ModuleType { get; set; }
        public CounterTypeEnum CounterType { get; set; }
        public string Mark { get; set; }
        public int Count { get; set; }
        public List<string> Other { get; set; }
        public long CreateTime { get; set; }
        public int Level { get; set; }
        public object Result { get; set; }
        public double RunTime { get; set; }

        public string AppKey { get { return MedivhSdk.Client.AppKey; } }

        internal void Check()
        {
            //检测替换用户输入文字中的\n
            if (!string.IsNullOrWhiteSpace(this.Mark))
            {
                this.Mark = this.Mark.Replace("\n", " ");
            }


            if (this.Other != null && this.Other.Count > 0)
            {
                for (int i = 0; i < Other.Count; i++)
                {
                    if (Other[i].IndexOf("\n", StringComparison.Ordinal) >= 0)
                    {
                        Other[i] = Other[i].Replace("\n", " ");
                    }
                }
            }

            //result 中\n替换
            if (Result != null)
            {
                var json = JsonHelper.JsonObjectToString(Result);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    json = json.Replace("\n", " ");
                    this.Result = JsonHelper.JosnStringToObject<object>(json);
                }
            }
        }

    }
}
