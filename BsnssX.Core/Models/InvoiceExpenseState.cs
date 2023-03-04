using BsnssX.Core.Interfaces;

namespace BsnssX.Core.Models
{
    public class InvoiceExpenseState : IWithId
    {
        public enum State
        {
            Open,    // invoice wurde erzeugt
            InvoiceExpensesLocked,     // wurde an Kunden gesendet
            TaxFinished
        }
        
        public string Id { get; set; }
        public string MandantId { get; set; }        
        public int Year { get; set; }

        public State Status { get; set; }
    }
}
