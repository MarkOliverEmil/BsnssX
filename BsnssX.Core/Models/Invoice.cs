using BsnssX.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BsnssX.Core.Models
{


    public class Invoice : IWithId
    {        
        public enum State
        {
            Created,    // invoice wurde erzeugt
            Billed,     // wurde an Kunden gesendet
            Payed,      // wurde vom Kunden bezahlt
            Denied
        }
        public Invoice()
        {
            InvoiceState = State.Created;
        }
        public string Id { get; set; }
        
        public string InvoiceNumber { get; set; }

        public int Year { get; set; }
        public Enums.Quartal Quart { get; set; }        

        public DateTime InvoiceBillingDate { get; set; } = DateTime.MinValue;        
        public DateTime InvoicePaymentDate { get; set; } = DateTime.MinValue;
        

        public string MandantId { get => Vendor.Address.Id; }
        public Vendor Vendor { get; set; }

        public Address Client { get; set; }

        public List<InvoiceItem> InvoiceItems { get; set; }
        

        public decimal PriceWithoutVat { get; set; }
        public decimal PriceVat { get; set; }
        public decimal Price { get; set; }

        public State InvoiceState { get; set; }        
        public int Vat { get; set; }
        
        public string Comment { get; set; }

        [NotMapped]
        public List<string> Texts { get; set; }        
    }
}
