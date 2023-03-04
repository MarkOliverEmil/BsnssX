using BsnssX.Core.Extensions;
using BsnssX.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ExpenseViewModel
    {
        public ExpenseViewModel(Expense exp, int noAttachments)
        {
            Expense = exp;
            NoAttachments = noAttachments;
        }
        public Expense Expense { get; set; }

        public string PaymentDate { get => Expense.PaymentDate.ToStringOrEmpty(); }

        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal Price { get => Expense.Price; }
        public string YearQuart { get => $"{Expense.Year} - {Expense.Quart}"; }

        public int NoAttachments { get; set; }
    }
}
