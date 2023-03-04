using BsnssX.Core.Models;
using BsnssX.Core.Utils;
using System.Collections.Generic;

namespace Web.Models
{
    public class AddressView
    {
        public AddressView() { }
        public AddressView(bool isNew)
        {
            Address = new Address();
            Address.Id = IdGenerator.NextId;
            IsNew = isNew;
            ContactInfos = new List<ContactInfo>();
        }
        public Address Address { get; set; }
        public List<ContactInfo> ContactInfos { get; set; }

        public bool IsNew { get; set; }
    }
}
