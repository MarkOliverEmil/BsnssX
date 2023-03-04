using BsnssX.Core.Models;

namespace Web.Extensions
{
    public static class NoteExtension
    {
        public static bool IsLeistungsnachweis(this Note note)
        {
            if (note == null) return false;
            if (note.Title.Length < 4) return false;
            if (note.Title.Substring(0,2) != "LN") return false;            

            if (int.TryParse(note.Title.Substring(2,2), out int val) == false) return false;
                
            return (val >= 1 && val < 13);
        }
    }
}