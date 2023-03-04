using BsnssX.Core.Interfaces;
using BsnssX.Core.Utils;
using System;

namespace BsnssX.Core.Models
{
    public class Note : IWithId
    {
        public string Id { get; set; } = IdGenerator.CreateId();
        public string Title { get; set; }
        public string Text { get; set; }

        public string HtmlText { get; set; }

        public DateTime Date { get; set; } = DateTime.MinValue;

        public string MandantId { get; set; }
        public string OwnerId { get; set; }

        public string NoteType { get; set; }
        public int Year { get; set; }
    }
}
