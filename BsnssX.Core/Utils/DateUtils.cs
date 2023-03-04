using System;
using System.Collections.Generic;

namespace BsnssX.Core.Utils
{
    public class DateUtils
    {
        static public List<int> GetMonthsForString(string src, out string dest)
        {
            List<int> months = new List<int>();            
            switch (src)
            {
                case Config.Calendar.Year:
                    {
                        for (int i = 0; i <= 12; i++) months.Add(i);
                        dest = Config.Calendar.Jan_Dec;
                        break;
                    }
                case Config.Calendar.Q1: 
                    months.Add(1);
                    months.Add(2);
                    months.Add(3);
                    dest = Config.Calendar.Quartal1;
                    break;
                case Config.Calendar.Q2:
                    months.Add(4);
                    months.Add(5);
                    months.Add(6);
                    dest = Config.Calendar.Quartal2;
                    break;
                case Config.Calendar.Q3:
                    months.Add(7);
                    months.Add(8);
                    months.Add(9);
                    dest = Config.Calendar.Quartal3;
                    break;
                case Config.Calendar.Q4:
                    months.Add(10);
                    months.Add(11);
                    months.Add(12);
                    dest = Config.Calendar.Quartal4;
                    break;
                default:
                    {
                        int m = int.Parse(src);
                        months.Add(m);
                        dest = DateUtils.GetMonthName(m);
                        break;
                    }
            }
            return months;
        }

        public static string GetMonthName(int month)
        {
            DateTime date = new DateTime(2000, month, 1);
            var res = date.ToString("MMMM");
            return res;
        }
        public static int NoMondaysInYear(int y)
        {
            var dt = new DateTime(y, 01, 01);
            while (dt.DayOfWeek != DayOfWeek.Monday)
                dt = dt.AddDays(1);
            return dt.AddDays(52 * 7).Year == y ? 53 : 52;
        }

        // trimester
        //  1 : Jan - April
        //  2 : May - Aug
        //  3 : Sept - Dec

        public static int NoMondaysInTrimester(int year, int trimester)
        {
            if (trimester < 1 || trimester > 3)
                return 0;
            var mondaysInMonth = NoMondaysInMonth(year);

            int noMondays = 0;
            int offset = (trimester - 1) * 4;
            for (int i = 0; i < 4; i++)
                noMondays += mondaysInMonth[offset + i];
            return noMondays;
        }

        public static List<int> NoMondaysInMonth(int y)
        {
            List<int> res = new List<int>();
            var dt = new DateTime(y, 01, 01);
            while (dt.DayOfWeek != DayOfWeek.Monday)
                dt = dt.AddDays(1);

            while (res.Count < 12)
            {
                int noMondays = 4;
                if (dt.AddDays((28)).Month == dt.Month)
                    noMondays = 5;
                res.Add(noMondays);
                dt = dt.AddDays(noMondays * 7);
            }
            return res;
        }
    }
}