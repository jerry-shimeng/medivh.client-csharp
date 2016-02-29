namespace Medivh.Models
{
    public class CmdModel
    {
        //{"Mark":"","Alias":"sys","CounterType":"System","Level":0,"Module":2,"Counter":0}


        /// <summary>
        /// 1一次性数据  2系统信息     3心跳数据    4事务数据
        /// </summary>
        public ModuleTypeEnum ModuleType { get; set; }
        /// <summary>
        /// 1err 2object 3business 4custom
        /// </summary>
        public CounterTypeEnum CounterType { get; set; } 
        public string Mark { get; set; }  
        public int Level { get; set; }
        public string Operate { get; set; }
        public string Extend { get; set; }
    }
}
