namespace Medivh.Models
{
    public class ClientInfo
    {
        /// <summary>
        /// 该程序的别名
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// app的key,由监控中心分配
        /// </summary>
        public string AppKey { get; set; } 
    }
}
