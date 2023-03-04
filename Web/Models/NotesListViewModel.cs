using BsnssX.Core.Models;
using System.Collections.Generic;
using System.Linq;
using Web.Extensions;

namespace Web.Models
{
    public class NotesListViewModel
    {
        public List<NoteView> Notes { get; set; } = new List<NoteView>();

        public Mandant Mandant { get; set; }

        public string SelectedYear { get; set; }
        public string SelectedNoteType { get; set; }

        public NotesListViewModel FilterNotes()
        {
            if (SelectedNoteType.IsValidFilter())
                Notes = Notes.Where(x => x.Note.NoteType == SelectedNoteType).ToList();

            if (SelectedYear.IsValidFilter())
                Notes = Notes.Where(x => x.Note.Year.ToString() == SelectedYear).ToList();
            return this;
        }
    }
}
