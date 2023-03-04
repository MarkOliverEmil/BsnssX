using BsnssX.Core.Utils;

namespace BsnssX.Core.Tax.Malta
{
    public class TaxCalculatorMalta
    {
        public const string Period1 = "Jan - Apr";
        public const string Period2 = "May - Aug";
        public const string Period3 = "Sep - Dec";

        public static decimal SocialSecurityPerWeek(int year)
        {
            if (year >= 2023)
                year = 2023;

            switch (year)
            {
                case 2021:
                    return 72.86M;
                case 2022:
                    return 74.96M;
                case 2023:
                    return 77.40M;
                default:
                    return 69.79M; // 2019 rate
            }
        }
        static decimal CalcTax(decimal income, decimal rate, decimal subtract)
        {
            var res = (income * rate) / 100 - subtract;
            return res;
        }
        // single, non parent
        public static decimal CalculateTax(int year, decimal income)
        {            
            // https://cfr.gov.mt/en/rates/Pages/TaxRates/Tax-Rates-2023.aspx
            if (income <= 9100)
                return 0M; // :-) 

            if (income <= 14500)            
                return CalcTax(income, 15, 1365);

            if (income <= 19500)
                return CalcTax(income, 25, 2815);

            if (income <= 60000)
                return CalcTax(income, 25, 2725);

            // :-(
            return CalcTax(income, 35, 8725);
        }

        public static SocialSecurityResult CalcSocialSecurityContributions(int year)
        {            
            var sspw = SocialSecurityPerWeek(year);            
            SocialSecurityResult item = new SocialSecurityResult();
            item.Year = year;
            item.AmountPeriod1 = DateUtils.NoMondaysInTrimester(year, 1) * sspw;
            item.AmountPeriod2 = DateUtils.NoMondaysInTrimester(year, 2) * sspw;
            item.AmountPeriod3 = DateUtils.NoMondaysInTrimester(year, 3) * sspw;
            return item;
        }
        public static decimal CalcSocialSecurityContributionsSum(int year, decimal income)
        {
            if (income <= 25986)
            {
                var r = (income * 15) / 100;
                return r;
            }
            var x = CalcSocialSecurityContributions(year);            
            return x.AmountSum;
        }
    }
}
