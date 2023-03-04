
namespace BsnssX.Core.Tax.Malta
{
    public class SocialSecurityResult
    {      
        public string Id { get; set; }

        public int Year { get; set; }
        public decimal AmountPeriod1 { get; set; }
        public decimal AmountPeriod2 { get; set; }
        public decimal AmountPeriod3 { get; set; }

        public decimal AmountSum { get => AmountPeriod1 + AmountPeriod2 + AmountPeriod3; }
    }
}
