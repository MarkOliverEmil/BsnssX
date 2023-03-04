using BsnssX.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace BsnssX.Core.Services
{

    public class NoteService :  Service<Note>, INoteService
    {
        public NoteService()
        {
            FileName = "Notes.json";
            Load();
        }        

        public Note GetByOwnerId(string id)
        {
            return Get().FirstOrDefault(x => x.OwnerId == id);
        }

        public List<Note> GetForMandant(string id)
        {
            return Items.Where(x => x.MandantId == id).ToList();
        }
    }
}
