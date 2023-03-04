using BsnssX.Core.Extensions;
using BsnssX.Core.Models;
using BsnssX.Core.Utils;
using System.Collections.Generic;
using System.Linq;
using Web.Models;

namespace Web.Extensions
{
    public static class InvoiceCreateModelExtension
    {
        public static Invoice BuildInvoice(this InvoiceEditCreateModel model, Mandant mandant)
        {
            Invoice inv = new Invoice();
            

            inv.Id = IdGenerator.CreateId();
            inv.InvoiceNumber = model.InvoiceNumber;
            inv.Vendor = new Vendor()
            {
                Id = IdGenerator.CreateId(),
                BankDetails = mandant.BankDetails,
                Address = mandant.Address
            };

            inv.Client = mandant.DefaultClient;
            inv.InvoiceBillingDate = model.BillingDate;
            inv.Year = inv.InvoiceBillingDate.Year;
            inv.Quart = Enums.GetQuartal(inv.InvoiceBillingDate);
            inv.InvoiceState = Invoice.State.Billed;
            inv.InvoicePaymentDate = model.BillingDate;

            inv.InvoiceItems = new List<InvoiceItem>();
            var item = new InvoiceItem();
            item.BookableItem = mandant.BookableItems.FirstOrDefault();
            item.Quantity = model.Quantiy;
            item.Id = IdGenerator.CreateId();

            inv.InvoiceItems.Add(item);
            inv.Calculate();
            return inv;
        }

        static Invoice.State GetState(string s)
        {
            if (s == Invoice.State.Billed.ToString())
                return Invoice.State.Billed;
            if (s == Invoice.State.Payed.ToString())
                return Invoice.State.Payed;
            if (s == Invoice.State.Created.ToString())
                return Invoice.State.Created;
            return Invoice.State.Denied;
        }
        public static void MapInvoice(this InvoiceEditCreateModel model, Invoice inv)
        {
            inv.InvoiceNumber = model.InvoiceNumber;
            inv.Comment = model.Comment;
          
            inv.InvoiceBillingDate = model.BillingDate;
            inv.InvoicePaymentDate = model.PaymentDate;

            inv.Year = inv.InvoiceBillingDate.Year;
            inv.Quart = Enums.GetQuartal(inv.InvoicePaymentDate);
            inv.InvoiceState = GetState(model.SelectedState);

            inv.InvoiceItems.First().Quantity = model.Quantiy;           
            inv.Calculate();            
        }
    }
}