using BsnssX.Core.Interfaces;
using BsnssX.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace BsnssX.Core.Services
{
    public class ContactInfoService : Service<ContactInfo>, IContactInfoService
    {
        public ContactInfoService()
        {
            FileName = "ContactInfo.json";
            Load();
        }        
        

        public List<ContactInfo> GetByOwnerId(string id)
        {
            return Get().Where(x => x.OwnerId == id).ToList();
        }
    }
}
