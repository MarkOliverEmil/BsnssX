using BsnssX.Core.Models;
using System.Collections.Generic;

namespace BsnssX.Core.Services
{
    public interface INoteService
    {
        List<Note> Get();
        Note Get(string id);
        List<Note> GetForMandant(string id);
        Note GetByOwnerId(string id);

        Note Add(Note adr);

        void Delete(string id);

        Note Update(Note adr);
        
    }
}
