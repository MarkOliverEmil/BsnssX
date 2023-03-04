using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class TaxCalcModel
    {        
        [Key]
        public string Id { get; set; }
        
        public int Income { get; set; }


        public int Expenses { get; set; }


        public int Tax { get; set; }


        public int SocialSecurity { get; set; }


        public int Netto { get => Income - Expenses - SocialSecurity - Tax; }
        
        public int Percentage { get => Netto / Income; }

        public string PercentageString { get => ((int)(Percentage * 100)).ToString() + "%"; }
    }
}
