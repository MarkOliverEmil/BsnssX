using BsnssX.Core.Interfaces;
using BsnssX.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace BsnssX.Core.Services
{
    public class TaxService :  Service<TaxForYear>, ITaxService
    {
        public TaxService()
        {
            FileName = "Tax.json";
            Load();
        }

        public List<TaxForYear> GetForMandant(string mandantId)
        {
            return Get().Where(x => x.MandantId == mandantId).ToList();
        }
    }
}
