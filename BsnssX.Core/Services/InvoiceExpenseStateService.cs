using BsnssX.Core.Interfaces;
using BsnssX.Core.Models;
using System.Linq;

namespace BsnssX.Core.Services
{
    public class InvoiceExpenseStateService : Service<InvoiceExpenseState>, IInvoiceExpenseStateService
    {
        public InvoiceExpenseStateService()
        {
            FileName = "InvoiceExpenseState.json";
            Load();
        }
        public InvoiceExpenseState Get(string mandantId, int year)

            => Get().FirstOrDefault(x => x.MandantId == mandantId && year == x.Year);
    }
}
