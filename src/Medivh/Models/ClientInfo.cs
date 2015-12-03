namespace Medivh.Models
{
    public class ClientInfo
    {
        /// <summary>
        /// 该程序的别名
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// app的public key，必须是唯一的
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 密钥:安全验证使用
        /// </summary>
        public string AppSecret { get; set; }
    }
}
