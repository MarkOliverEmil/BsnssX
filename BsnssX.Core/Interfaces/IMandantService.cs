using BsnssX.Core.Models;
using System.Collections.Generic;

namespace BsnssX.Core.Interfaces
{

    public interface IMandantService
    {
        List<Mandant> Get();
        Mandant Get(string id);

        Mandant Add(Mandant adr);

        void Delete(string id);

        Mandant Update(Mandant adr);
    }
}
