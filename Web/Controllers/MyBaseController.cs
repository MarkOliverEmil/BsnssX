using BsnssX.Core.Extensions;
using BsnssX.Core.Interfaces;
using BsnssX.Core.Models;
using BsnssX.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Web.Controllers
{
    public class MyBaseController : Controller
    {        
        public MyBaseController() {}
        
        public IInvoiceService InvoiceService { get; set; }
        public ITaxService TaxService { get; set; }
        public IMandantService MandantService { get; set; }
        public IDocumentService DocumentService { get; set; }
        public IContactInfoService ContactInfoService { get; set; }
        public IConfigurationService ConfigurationService { get; set; }
        public INoteService NoteService { get; set; }
        public IAddressService AddressService { get; set; }
        public IExpenseService ExpenseService { get; set; }
        public IEmailConfigService  EmailConfigService { get; set; }        
        public IInvoiceExpenseStateService InvoiceExpenseStateService { get; set; }
        public Mandant GetMandant() => MandantService.Get(MandantId);
        
        public string MandantId
        {
            get => ConfigurationService.Get().MandantId;
            set
            {
                if (value == null)
                    return;
                var item = ConfigurationService.Get();
                if (item.MandantId == value)
                    return;
                item.MandantId = value;
                ConfigurationService.Update(item);
            }
        }
        public string Period
        {
            get => ConfigurationService.Get().Period;
            set
            {
                var item = ConfigurationService.Get();
                item.Period = value;
                ConfigurationService.Update(item);
            }
        }
        public int Year
        {
            get => ConfigurationService.Get().Year;
            set
            {
                var item = ConfigurationService.Get();
                if (item.Year == value)
                    return;
                item.Year = value;
                ConfigurationService.Update(item);
            }
        }

        public IActionResult DownloadDocument(string id, string alternativeAction)
        {
            var doc = DocumentService.Get().FirstOrDefault(x => x.Id == id);
            if (doc == null)
                return View(alternativeAction);

            var filePath = doc.GetFilePath();
            var stream = new FileStream(filePath, FileMode.Open);
            return File(stream, doc.ContentType, doc.FileName);
        }
        public List<SelectListItem> GetMandantsList()
        {
            var mndts = MandantService.Get();
            var res = mndts.Select(m => new SelectListItem()
            {
                Text = m.Address.Name,
                Value = m.Address.Id
            }).ToList();
            if (res.First().Value != MandantId)
                res.Reverse();
            return res;
        }
    }
}
