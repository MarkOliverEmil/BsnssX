using BsnssX.Core.Tax.Malta;
using System.Collections.Generic;

namespace BsnssX.Core.Models
{
    public class MaltaTax
    {        
        public string Id { get; set; }

        public int Year { get; set; }
        public List<SocialSecurityResult> SocialSecurityContributions { get; set; }        
        public List<TaxResult> TaxResults { get; set; }


        //  for calculator
        public int Income { get; set; }
        public int Tax { get; set; }
        public int SocialSecurity { get; set; }

        public int Netto { get => Income -  SocialSecurity - Tax; }
        public double Percentage { get => (Netto / (double)Income); }
        public string PercentageString { get => ((int)(Percentage * 100)).ToString() + "%"; }
    }
}
