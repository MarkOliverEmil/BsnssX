using BsnssX.Core.Models;

namespace Web.Models
{
    public class ContactInfoModel
    {
        public ContactInfoModel() { }
        public Modus Modus { get; set; }
        public string Name { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public string AspActionDelete { get; set; }
        public string AspController { get; set; }        
    }
}
