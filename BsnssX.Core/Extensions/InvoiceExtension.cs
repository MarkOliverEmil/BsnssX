using BsnssX.Core.Models;
using BsnssX.Core.Services;
using System.Linq;

namespace BsnssX.Core.Extensions
{

    public static class InvoiceExtension
    {        

        public static Invoice Calculate(this Invoice invoice)
        {            
            decimal price = 0;
            decimal priceVat = 0;
            decimal priceWithVat = 0;

            if (invoice.InvoiceItems != null )
            {
                foreach (var item in invoice.InvoiceItems)
                {
                    price += item.Price * item.Quantity;
                }
                    
                priceVat = ((decimal)invoice.Vat * price) / 100;
                priceWithVat = price + priceVat;
            }
            invoice.Price = priceWithVat;
            invoice.PriceVat = priceVat;
            invoice.PriceWithoutVat = price;
            return invoice;
        }
        
        public static bool IsValid(this Invoice invoice)
        {
            if (invoice.Vendor == null)
                return false;
            if (invoice.Client == null)
                return false;
            if (invoice.Vendor.BankDetails == null)
                return false;
            if (invoice.InvoiceItems == null)
                return false;
            if (invoice.InvoiceItems.Any() == false)
                return false;

            return true;
        }
    }
}
