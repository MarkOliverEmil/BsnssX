using BsnssX.Core.Models;

namespace Web.Models
{
    public class NoteView
    {

        public NoteView(Note note)
        {
            Note = note;
        }
        public Note Note { get; private set; }
        public string Id { get => Note.Id; }

        public string Timestamp { get => Note.Date.ToShortDateString() + " " + Note.Date.ToShortTimeString(); }
        public string TextShort { get
            {
                if (string.IsNullOrEmpty(Note.Text))
                    return string.Empty;
                if (Note.Text.Length < 21)
                    return Note.Text;
                return Note.Text.Substring(0, 20) + " ...";
            }
        }
    }
}
