using BsnssX.Core.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Web.Helpers;

namespace Web.Extensions
{
    public static class NoteServiceExtension
    {
        public static List<SelectListItem> GetTypes(this INoteService service,  string selectedType)
        {
            var items = service.Get()
                .Select(x => x.NoteType)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
            items.Insert(0, Consts.All);
            if (!string.IsNullOrEmpty(selectedType))
            {
                items = items.Where(x => x != selectedType).ToList();
                items.Insert(0, selectedType);
            }

            return items.Select(x => ControllerUtils.CreateSelectListItem(x)).ToList();
        }
        public static List<SelectListItem> GetYears(this INoteService service,  string selectedYear)
        {
            var items = service.Get()
                .Select(x => x.Year.ToString())
                .Distinct()
                .OrderBy(x => x)
                .ToList();
            items.Insert(0, Consts.All);
            if (!string.IsNullOrEmpty(selectedYear))
            {
                items = items.Where(x => x != selectedYear).ToList();
                items.Insert(0, selectedYear);
            }

            return items.Select(x => ControllerUtils.CreateSelectListItem(x)).ToList();
        }
    }
}