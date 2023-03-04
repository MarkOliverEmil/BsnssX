using BsnssX.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Web.Models
{
    public class EmailModel
    {
        public EmailModel() { }
        public EmailModel(string email) { Email = email;  IsChecked = true; }
        public string Email { get; set; }
        public bool IsChecked { get; set; }
    }

    public class InvoiceExpensesViewModel
    {
        public Mandant Mandant { get; set; }

        public string Title { get; set; }
        public string SelectedReportPeriod { get; set; }
        
        public string ID { get => Mandant.Id + "_" + SelectedYear.ToString(); }
        public List<InvoiceViewModel> Invoices { get; set; }

        public List<ExpenseViewModel> Expenses { get; set; }

        public InvoiceExpenseState.State InvoiceExpenseState { get; set; }

        public List<Note> Notes { get; set; }
        //public List<string> ReportEmails { get; set; }
        public List<EmailModel> Emails { get; set; }

        public bool HasInvoices { get => Invoices != null && Invoices.Any(); }
        public bool HasExpenses { get => Expenses != null && Expenses.Any(); }

        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal InvoicesSum { get => Invoices.Select(x => x.Invoice.Price).Sum(); }

        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal ExpensesSum { get => Expenses.Select(x => x.Expense.Price).Sum(); }

        public string SelectedYear { get; set; }

       
    }
}
