using BsnssX.Core.Models;
using System.Collections.Generic;

namespace BsnssX.Core.Interfaces
{
    public interface IAddressService
    {
        List<Address> Get();
        Address Get(string id);
        Address Add(Address adr);
        void Delete(string id);
        Address Update(Address adr);
    }
}
