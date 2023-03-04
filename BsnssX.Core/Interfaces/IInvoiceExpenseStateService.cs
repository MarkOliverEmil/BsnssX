using BsnssX.Core.Models;
using System.Collections.Generic;

namespace BsnssX.Core.Interfaces
{
    public interface IInvoiceExpenseStateService
    {
        List<InvoiceExpenseState> Get();
        InvoiceExpenseState Get(string id);
        InvoiceExpenseState Get(string mandantId, int year);

        InvoiceExpenseState Add(InvoiceExpenseState adr);
        void Delete(string id);
        InvoiceExpenseState Update(InvoiceExpenseState adr);
    }
}
