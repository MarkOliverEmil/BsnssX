using System;

namespace BsnssX.Core.Models
{
    public class Enums
    {
        public enum Quartal
        {
            Q1 = 1,
            Q2 = 2,
            Q3 = 3,
            Q4 = 4
        }

        public static Quartal GetQuartal(DateTime dt)
        {
            switch (dt.Month)
            {
                case 1:
                case 2:
                case 3:
                    return Quartal.Q1;
                case 4:
                case 5:
                case 6:
                    return Quartal.Q2;
                case 7:
                case 8:
                case 9:
                    return Quartal.Q3;
                default:
                    return Quartal.Q4;
            }

        }   
         
        public static string YearQuartToString(int year, Quartal q) => $"{year} - {q}"; 
    }
}
