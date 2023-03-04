using System;
using System.ComponentModel.DataAnnotations;

namespace BsnssX.Core.Models
{
    public class PaymentDetails
    {
        public string Id { get; set; }
        public BankDetails BankDetails { get; set; }
        public string Info { get; set; }
        public string Comment { get; set; }
        public string Comment2 { get; set; }
        public string Receiver { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime PaymentDate { get; set; }        
        public decimal Amount { get; set; }
    }
}

