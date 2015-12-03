namespace Medivh.Models
{
    /// <summary>
    /// 1一次性数据  2系统信息     3心跳数据    4事务数据
    /// </summary>
    public enum ModuleTypeEnum
    {
        /// <summary>
        /// 一次性数据
        /// </summary>
        OnceData = 1,
        /// <summary>
        /// 系统信息
        /// </summary>
        SystemData = 2,
        /// <summary>
        /// 心跳数据
        /// </summary>
        HeartBeatData = 3,
        /// <summary>
        /// 事务数据
        /// </summary>
        TransactionData = 4
    }
}
