using System;

namespace Medivh.Common
{
    public static class DateTimeHelper
    {
        public static long Unix(this DateTime dt)
        {
            return (long)(dt - DateTime.Parse("1970-01-01")).TotalSeconds;
        }

        public static long GetNowHourUnix()
        {
            return (long)(DateTime.UtcNow - DateTime.Parse("1970-01-01")).TotalHours * 60 * 60;
        }

        public static long GetMinuteUnix()
        {
            var r = (long)(DateTime.UtcNow - DateTime.Parse("1970-01-01")).TotalMinutes / 10;

            return r * 10 * 60;
        }
    }
}
