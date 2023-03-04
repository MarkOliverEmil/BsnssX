using BsnssX.Core.Extensions;
using BsnssX.Core.Models;
using System.ComponentModel.DataAnnotations;
using Web.Extensions;

namespace Web.Models
{

    public class InvoiceViewModel
    {
        public InvoiceViewModel(Invoice inv, int noAttachments)
        {
            Invoice = inv;
            NoAttachments = noAttachments;
        }
        public Invoice Invoice { get; set; }

        public string Id { get => Invoice.Id; }

        public string InvoiceNumber { get => Invoice.InvoiceNumber; }
        public string Description { get => Invoice.Comment.GetFirstLine(); }

        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal Price { get => Invoice.Price; }
        public string InvoiceState { get => Invoice.InvoiceState.ToString(); }
        public string Client { get => Invoice.Client.Name; }

        public string YearQuart { get => $"{Invoice.Year} - {Invoice.Quart}"; }

        public string PaymentDate { get => Invoice.InvoicePaymentDate.ToStringOrEmpty();}

        public string BillingDate { get => Invoice.InvoiceBillingDate.ToStringOrEmpty(); }

        public int NoAttachments { get; set; }
    }
}
