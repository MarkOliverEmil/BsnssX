using BsnssX.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace BsnssX.Core.Models
{
    public class Expense : IWithId
    {        
        
        public enum State
        {
            Open,    // ist eingegangen            
            Payed,   // wurde bezahlt
            Denied  // nein danke
        }
        public Expense()
        {
            ExpenseState = State.Open;
        }
        [Key]
        public string Id { get; set; }

        public string Number { get; set; }
        public string Description { get; set; }

        public string Type { get; set; }

        public string Comment { get; set; }        

        public int Year { get; set; }
        public Enums.Quartal Quart { get; set; }        

        public DateTime ReceiveDate { get; set; } = DateTime.MinValue;
        
        
        public DateTime? PaymentDate { get; set; }         

        public string MandantId { get => Client.Id; }
        public Vendor Vendor { get; set; }

        public Address Client { get; set; }
       

        public decimal PriceWithoutVat { get; set; }
        public decimal PriceVat { get; set; }
        public decimal Price { get; set; }

        public State ExpenseState { get; set; }                                       
    }
}
