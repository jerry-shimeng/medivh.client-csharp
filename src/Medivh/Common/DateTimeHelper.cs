using System;

namespace Medivh.Common
{
    public  static class DateTimeHelper
    {
        public static long Unix(this DateTime dt)
        {
            return (long)(dt - DateTime.Parse("1970-01-01")).TotalSeconds; 
        }
    }
}
