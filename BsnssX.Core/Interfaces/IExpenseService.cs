using BsnssX.Core.Models;
using System.Collections.Generic;

namespace BsnssX.Core.Interfaces
{
    public interface IExpenseService
    {
        List<Expense> Get();

        List<Expense> Get(string mandantId, int year);
          
        Expense Get(string id);

        Expense Add(Expense adr);

        void Delete(string id);

        Expense Update(Expense adr);
    }
}
