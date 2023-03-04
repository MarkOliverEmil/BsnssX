using BsnssX.Core.Interfaces;
using BsnssX.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace BsnssX.Core.Services
{
    public class ExpenseService :  Service<Expense>, IExpenseService
    {
        public ExpenseService()
        {
            FileName = "Expenses.json";
            Load();
        }                
            
        public List<Expense> Get(string mandantId, int year)
        {
            var res = Get()
                .Where(x => x.MandantId == mandantId)
                .Where(x => x.Year == year)
                .OrderByDescending(x => x.PaymentDate)
                .ToList();
            return res;
        }           
    }
}
