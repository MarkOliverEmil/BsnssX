using BsnssX.Core.Models;
using System.Collections.Generic;

namespace BsnssX.Core.Interfaces
{
    public interface ITaxService
    {
        List<TaxForYear> Get();

        TaxForYear Get(string id);

        List<TaxForYear> GetForMandant(string mandantId);

        TaxForYear Update(TaxForYear item);
    }
}
