using BsnssX.Core.Models;
using BsnssX.Core.Utils;
using Web.Models;

namespace Web.Extensions
{
    public static class ExpenseCreateModelExtension
    {        
        public static Expense BuildExpense(this ExpenseEditCreateModel model, Mandant mandant, Address vendor)
        {
            var res = new Expense()
            {
                Id = IdGenerator.CreateId(),
                ExpenseState = FromString(model.SelectedState)
            };                       
            MapFromModel(model, res, vendor, mandant.Address);
            return res;
        }

        public static void MapExpense(this ExpenseEditCreateModel model, Mandant mandant, Expense res, Address vendor)
        {            
            res.ExpenseState = FromString(model.SelectedState);            
            MapFromModel(model, res, vendor, mandant.Address);            
        }
        static void MapFromModel(ExpenseEditCreateModel model, Expense exp, Address vendor, Address client)
        {
            exp.PaymentDate = model.PaymentDate;
            exp.ReceiveDate = model.ReceiveDate;
            exp.Number = model.Number;
            exp.Description = model.Description;
            exp.Comment = model.Comment;
            exp.Type = Consts.Expense;
            exp.Price = model.Price;

            exp.Year = exp.PaymentDate.Value.Year;
            exp.Quart = Enums.GetQuartal(exp.PaymentDate.Value);

            exp.Client = client;
            exp.Vendor = new Vendor();
            exp.Vendor.Address = vendor;
        }

        static Expense.State FromString(string s)
        {            
            if (s == Expense.State.Payed.ToString())
                return  Expense.State.Payed;
            if (s == Expense.State.Denied.ToString())
                return Expense.State.Denied;
            return Expense.State.Open;
        }
    }
}