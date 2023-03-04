
using System.ComponentModel.DataAnnotations;

namespace BsnssX.Core.Models
{
    public class TaxResult
    {
        public string Id { get; set; }
        public int Year { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal Income { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal Expenses { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal Tax { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal SocialSecurity { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal Netto { get => Income - Expenses - SocialSecurity - Tax; }
    
        public decimal Percentage { get => Netto / Income; }

        public string PercentageString { get => ((int)(Percentage * 100)).ToString() + "%"; }
    }
}
