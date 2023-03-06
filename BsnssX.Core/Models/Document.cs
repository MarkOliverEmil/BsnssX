using BsnssX.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace BsnssX.Core.Models
{
    public class Document : IWithId
    {
        [Key]
        public string Id { get; set; }
        public string StorageFile { get; set; }
        
        public string MandantId { get; set; }
        public int Year { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string OwnerId { get; set; }
        
        public string Comment { get; set; }

        public string ShortComment { get; set; }

        public string DocumnentType { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
