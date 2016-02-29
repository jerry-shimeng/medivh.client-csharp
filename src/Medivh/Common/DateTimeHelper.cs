using System;

namespace Medivh.Common
{
    public  static class DateTimeHelper
    {
        public static long Unix(this DateTime dt)
        {
            return (long)(dt - DateTime.Parse("1970-01-01")).TotalSeconds; 
        }

        public static DateTime UnixToDateTime(long d)
        {
            System.DateTime time = System.DateTime.MinValue;
            System.DateTime startTime = (new System.DateTime(1970, 1, 1));
            time = startTime.AddSeconds(d);
            return time;
        }

        /// <summary>
        /// 转换为以小时为单位的unix
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ConvertToMinutesUnix(DateTime dt)
        {
            return (long) (dt - DateTime.Parse("1970-01-01")).TotalMinutes*60;
        }


        public static long GetNowMinutesUnix()
        {
            return (long)(DateTime.UtcNow - DateTime.Parse("1970-01-01")).TotalMinutes * 60;
        }
    }
}
