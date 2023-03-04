using BsnssX.Core.Models;
using System.Collections.Generic;

namespace BsnssX.Core.Interfaces
{
    public interface IBankDetailService
    {
        List<BankDetails> Get();
        BankDetails Get(string id);

        BankDetails Add(BankDetails adr);

        void Delete(string id);

        BankDetails Update(BankDetails adr);
    }
}
