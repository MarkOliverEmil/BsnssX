using BsnssX.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BsnssX.Core.Models
{

    public class Address : IWithId
    {        
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }

        public string Country { get; set; }
        public string VatNumber { get; set; }

        public string Comment { get; set; }        
    }
}
