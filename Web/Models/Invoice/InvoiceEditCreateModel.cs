using BsnssX.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Models
{
    public class InvoiceEditCreateModel
    {
        public bool IsReadOnly { get; set; } 
        public InvoiceEditCreateModel() { }
        public InvoiceEditCreateModel(Mandant mandant)
        {
            IsCreateMode = true;
            InvoiceNumber = DateTime.Now.ToString("yyyy_MM_dd");
            MandantId = mandant.Id;
            Mandant = mandant;

            BillingDate = DateTime.Today;
            Client = mandant.DefaultClient;
            Bank = mandant.BankDetails;
            PaymentDate = DateTime.Now;
            BillingDate = DateTime.Now;

            Item = new InvoiceItem();
            Item.BookableItem = mandant.BookableItems.First();
            Quantiy = 100;
        }

        public InvoiceEditCreateModel(Invoice invoice, Mandant mandant, List<Document> attachments)
        {
            Id = invoice.Id;
            IsCreateMode = false;
            Mandant = mandant;
            MandantId = mandant.Id;
            InvoiceNumber = invoice.InvoiceNumber;
            Client = invoice.Client;
            Comment = invoice.Comment;
            Quantiy = invoice.InvoiceItems.First().Quantity;
            Bank = mandant.BankDetails;
            PaymentDate = invoice.InvoicePaymentDate;
            BillingDate = invoice.InvoiceBillingDate;
            SelectedState = invoice.InvoiceState.ToString();
            Item = invoice.InvoiceItems.First();
            Documents = attachments;
        }

        public bool IsCreateMode { get; set; }
        public Mandant Mandant { get; set; }
        public Address Client { get; set; }
        public  List<Document> Documents { get; set; }
        public string Id { get; set; }
        public string MandantId { get; set; }                
        public string InvoiceNumber { get; set; }
        public DateTime BillingDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public BankDetails Bank { get; set; }
        public InvoiceItem Item { get; set; }
        public decimal Quantiy { get; set; }
        public string Comment { get; set; }
        public string SelectedState { get; set; }
    }
}
