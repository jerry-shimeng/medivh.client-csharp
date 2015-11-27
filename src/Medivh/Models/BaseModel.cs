using System;
using System.Collections.Generic;
using System.Threading;
using Medivh.Common;

namespace Medivh.Models
{
    public class BaseModel
    {
        Random random = new Random();
        public BaseModel()
        {
            //Thread.Sleep(10);
            //var date = random.Next(240);
            //this.CreateTime = DateTime.UtcNow.AddHours(-1 * date).Unix();
            //Console.WriteLine(date);

            this.CreateTime = DateTime.UtcNow.Unix();
        }
        public ModuleTypeEnum ModuleType { get; set; }
        public CounterTypeEnum CounterType { get; set; }
        public string Mark { get; set; }
        public int Count { get; set; }
        public string Alias { get; set; }
        public List<string> Other { get; set; }
        public long CreateTime { get; set; }
        public int Level { get; set; }
    }
}
