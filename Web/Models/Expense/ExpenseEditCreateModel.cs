using BsnssX.Core.Models;
using BsnssX.Core.Utils;
using System;
using System.Collections.Generic;

namespace Web.Models
{    
    public class ExpenseEditCreateModel
    {        
        public ExpenseEditCreateModel()
        {
            Modus = Modus.Create;
            PaymentDate = ReceiveDate = DateTime.Today;
        }

        public ExpenseEditCreateModel(Mandant mandant)
        {
            Modus = Modus.Create;
            Id = IdGenerator.CreateId();
            Mandant = mandant;            
            MandantId = mandant.Id;
            PaymentDate = ReceiveDate = DateTime.Today;
            SelectedState = Expense.State.Open.ToString();
        }

        public ExpenseEditCreateModel(Expense expense, Mandant mandant, List<Document> documents)
        {            
            Id = expense.Id;
            Mandant = mandant;            
            MandantId = mandant.Id;
            
            Number = expense.Number;

            SelectedVendor = expense.Vendor?.Address?.Name;
            Comment = expense.Comment;
            Description = expense.Description;
            Price = expense.Price;

            PaymentDate = expense.PaymentDate.Value;
            ReceiveDate = expense.ReceiveDate;

            SelectedState = expense.ExpenseState.ToString();                
            SelectedVendor = expense.Vendor?.Address?.Id;
            Vendor = expense.Vendor.Address;

            Documents = documents;
        }
        public Modus  Modus { get; set; }
        
        public Mandant Mandant { get; set; }

        public string Id { get; set; }
        public string MandantId { get; set; }

        public List<Document> Documents { get; set; }
        public string SelectedState { get; set; }
        public string SelectedVendor { get; set; }

        public string Number { get; set; }
        public string Description { get; set; }        
        public string Comment { get; set; }                

        public DateTime ReceiveDate { get; set; }
        public DateTime PaymentDate { get; set; }

        public decimal Price  { get; set; }
        public Address Vendor { get; set; }       
    }
}
