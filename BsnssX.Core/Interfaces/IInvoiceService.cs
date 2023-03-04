using BsnssX.Core.Models;
using System.Collections.Generic;

namespace BsnssX.Core.Interfaces
{
    public interface IInvoiceService
    {
        List<Invoice> Get();
        Invoice Get(string id);
        List<Invoice> Get(string mandantId, int year);
        Invoice Add(Invoice adr);

        void Delete(string id);

        Invoice Update(Invoice adr);

    }
}
