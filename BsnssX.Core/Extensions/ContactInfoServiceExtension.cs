using BsnssX.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BsnssX.Core.Extensions
{
    public static class ContactInfoServiceExtension
    {
        public static List<string> ListReportEmailReceivers(this IContactInfoService service, string mandantId)
        {
            var res = service
                .GetByOwnerId(mandantId)
                .Where(ci => ci.Type == Config.ContactInfo.Email && ci.Subtype == Config.ContactInfo.Report)
                .Select(ci => ci.Value)
                .ToList();
            return res;
        }
        public static string ReportEmailFrom(this IContactInfoService service, string mandantId)
        {
            var res = service
                .GetByOwnerId(mandantId)
                .Where(ci => ci.Type == Config.ContactInfo.Email && ci.Subtype == Config.ContactInfo.Reportfrom)
                .Select(ci => ci.Value)
                .ToList()
                .FirstOrDefault();
            return res;
        }
        
    }
}
