using System.Collections.Generic;
using System.Linq;

namespace BsnssX.Core.Models
{
    public class Mandant
    {        
        public string Id { get; set; }
        public string Email { get; set; }
        
        public string Image { get; set; }
        public Address Address { get; set; }        
        public BankDetails BankDetails { get; set; }
        
        public List<Address> Clients { get; set; }
        public Address DefaultClient { get => Clients?.FirstOrDefault(); }

        public List<Vendor> Vendors { get; set; }
        public Vendor DefaultVendor { get => Vendors?.FirstOrDefault(); }

        public List<BookableItem> BookableItems { get; set; }

        public string SelectedMandant { get; set; }

        public List<TaxForYear> TaxesForYear { get; set; }

        public List<Document> Documents { get; set; }

        
        public List<int> Years { get; set; }

        public List<string> InvoiceText { get; set; }
        
    }
}
