using BsnssX.Core.Interfaces;
using BsnssX.Core.Models;

namespace BsnssX.Core.Services
{
    public class AddressService :  Service<Address>,  IAddressService
    {        
        public AddressService()
        {
            FileName = "Addresses.json";
            Load();
        }        
    }
}
