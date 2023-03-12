
using System.Collections.Generic;

namespace BsnssX.Core.Models
{
    
    public class Mandant2
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public Address Address { get; set; }

        public List<string> Banks { get; set; } = new List<string>();

        public List<string> Clients { get; set; } = new List<string>();

        public List<string> Vendors { get; set; } = new List<string>();

        public List<BookableItem> BookableItems { get; set; } = new List<BookableItem>();

        public List<int> Years { get; set; } = new List<int>();
        public List<string> InvoiceText { get; set; }
    }
}
