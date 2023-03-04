using System.ComponentModel.DataAnnotations;

namespace BsnssX.Core.Models
{
    public class BankDetails
    {        
        [Key]
        public string Id { get; set; }        
        public string BankName { get; set; }    
        public string AccountHolder { get; set; } 
        public string Bic { get; set; }
        public string Iban { get; set; }
    }

}
