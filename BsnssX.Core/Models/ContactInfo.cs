using BsnssX.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BsnssX.Core.Models
{
    public class ContactInfo : IWithId
    {
        public class Types
        {
            public const string Email = "email";
            public const string PhoneMobile = "mobile";
            public const string PhoneLanline = "landline";
            public const string WWW = "www";
            public const string ContactPerson = "contact person";
            public const string Report = "report";
        }
        public class Subtypes
        {
            public const string PhoneMobile = "private";
            public const string PhoneLandline = "work";
            public const string Report = "report";
            public const string ReportFrom = "reportfrom";
        }
        public ContactInfo() { }               

        [Key]
        public string Id { get; set; }

        public string OwnerId { get; set; }
        
        public string Type { get; set; }    
        public string Subtype { get; set; } 

        public string Value { get; set; }
        public string Comment { get; set; }
    }
}
