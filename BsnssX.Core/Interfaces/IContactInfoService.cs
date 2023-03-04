using BsnssX.Core.Models;
using System.Collections.Generic;

namespace BsnssX.Core.Interfaces
{
    public interface IContactInfoService
    {
        List<ContactInfo> Get();
        List<ContactInfo> GetByOwnerId(string id);
        ContactInfo Get(string id);
        ContactInfo Add(ContactInfo adr);
        void Delete(string id);
        ContactInfo Update(ContactInfo adr);
    }
}
