using BsnssX.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BsnssX.Core.Models
{
    public class TaxForYear : IWithId
    {
        public enum State
        {
            Estimated,
            Declared,
            Payed
        };

        
        public string Id { get; set; }
        
        public string MandantId { get; set; }

        public int Year { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal Turnover { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal ExpensesAmount { get => Expenses.Select(x => x.Amount).Sum(); }

        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal Tax { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal SocialSecurity { get => CalcSocials(); }

        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal ProfitBeforeTax { get => Turnover - ExpensesAmount; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal ProfitAfterTax { get => ProfitBeforeTax - Tax; }

        
        public State TaxState { get; set; }

        public int Profit { get => (int) ((ProfitAfterTax / ProfitBeforeTax) * 100); }

        public List<TaxExpense> Expenses { get; set; } = new List<TaxExpense>();
        public List<PaymentDetails> PaymentDetails { get; set; }
        public List<PaymentDetails> SocialSecurityPayments { get; set; }
        decimal CalcSocials()
        {
            decimal res = 0;
            if (SocialSecurityPayments != null)
                res = SocialSecurityPayments.Select(x => x.Amount).Sum();
            return res;                        
        }

        public List<Document> Documents { get; set; }


        public string Comment { get; set;  }
    }

}

