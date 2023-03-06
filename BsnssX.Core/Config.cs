using BsnssX.Core.Interfaces;

namespace BsnssX.Core
{
    public class Config
    {    
        public const string DefaultCurrency = "€";
        public const string Undefined = "---";                
        
        public static string DbDir { get; set; } = @"wwwroot\data\db"; 
        public static string TmpDir { get => @"wwwroot\tmp"; }
        public static string RootDir { get => @"wwwroot\data"; }
        public static string BlobDir { get => @"data\Blobs"; }

        public static string PdfContentType { get => "application/pdf"; }
        public static string ZipContentType { get => "application/zip"; }
        public static string TextContentType { get => "application/text"; }

        public class Calendar
        {
            public const string Q1 = "Q1";
            public const string Q2 = "Q2";
            public const string Q3 = "Q3";
            public const string Q4 = "Q4";
            public const string Quartal1 = "Quartal 1";
            public const string Quartal2 = "Quartal 2";
            public const string Quartal3 = "Quartal 3";
            public const string Quartal4 = "Quartal 4";

            public const string Year = "Year";
            public const string Jan_Dec = "Jan - Dec";
        }


        public class ContactInfo
        {
            public const string Email = "email";
            public const string Report = "report";
            public const string Reportfrom = "reportfrom";
        }
    }   
}
