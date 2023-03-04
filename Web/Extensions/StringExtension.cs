using System;

namespace Web.Extensions
{
    public static class StringExtension
    {
        public static bool IsValidFilter(this string s)
        {
            return (string.IsNullOrWhiteSpace(s) || s == Consts.All) ? false : true;
        }
        public static string GetFirstLine(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return s;
            return s.Split(Environment.NewLine)[0];
        }

        public static string MakeShort(this string s, int maxLen)
        {
            if (string.IsNullOrWhiteSpace(s) || s.Length <= maxLen)
                return s;
            return s.Substring(0, maxLen) + "...";
            
        }
    }
}