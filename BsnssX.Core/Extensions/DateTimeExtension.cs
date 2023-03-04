using System;

namespace BsnssX.Core.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToStringOrEmpty(this DateTime dt)
        {
            if (dt == DateTime.MinValue)
                return string.Empty;
            return dt.ToShortDateString();
        }
        public static string ToStringOrEmpty(this DateTime? dt)
        {
            if (dt.HasValue)
                return ToStringOrEmpty(dt.Value);                                   
            return string.Empty;            
        }
    }
}
