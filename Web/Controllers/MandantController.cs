using BsnssX.Core.Interfaces;
using BsnssX.Core.Models;
using BsnssX.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Web.Extensions;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MandantController :  MyBaseController
    {
        const string DefaultAction = "Details";
        const string TaxAction = "TaxDetails";
        const string DocView = "DocumentView";        

        public MandantController(IInvoiceService service, 
            IMandantService mandantService, 
            ITaxService taxService,
            IInvoiceExpenseStateService invoiceExpenseStateService,
            IDocumentService documentService, 
            IConfigurationService configurationService)
        {
            InvoiceService = service;            
            TaxService = taxService;
            DocumentService = documentService;
            ConfigurationService = configurationService;            
            MandantService = mandantService;
            DocumentService = documentService;
            InvoiceExpenseStateService = invoiceExpenseStateService;                       
        }
               
        public ActionResult Details(string id)
        {                           
            MandantId = id;
            var mandant = GetMandant();
            mandant.TaxesForYear = TaxService.GetForMandant(MandantId);
            mandant.Documents = DocumentService.GetForMandant(MandantId);
            ViewBag.Mandants = GetMandantsList();            
            return View(mandant);
        }        
        public ActionResult TaxDetails(string id)
        {
            var model = TaxService.Get().FirstOrDefault(x => x.Id == id);
            model.Documents = DocumentService.GetListByOwnerId(model.Id);
            return View(model);
        }
        [HttpPost]
        public IActionResult OnSaveTax(TaxForYear model)
        {
            var tfy = TaxService.Get().FirstOrDefault(x => x.Id == model.Id);
            tfy.Comment = model.Comment;
            TaxService.Update(tfy);
            return RedirectToAction(TaxAction, new { Id = model.Id });
        }


        #region CRUD & download
        public IActionResult CreateDocument(string id, int year, string info)
        {
            var mandant = GetMandant();
            DocumentViewModel model = new DocumentViewModel(mandant)
            {
                OwnerId = id,
                Year = year,
                DocumentType = Consts.Tax
            };            
            return View(DocView, model);
        }

        public IActionResult DisplayDocument(string id)
        {
            var mandant = GetMandant();
            var doc = DocumentService.Get().FirstOrDefault(x => x.Id == id);
            DocumentViewModel model = new DocumentViewModel(doc, mandant);
            model.Modus = Modus.Readonly;
            FillTaxRoutes(model);
            return View(DocView, model);
        }

        public IActionResult EditDocument(string id)
        {
            var doc = DocumentService.Get().FirstOrDefault(x => x.Id == id);
            DocumentViewModel model = new DocumentViewModel(doc, GetMandant());
            FillTaxRoutes(model);
            return View(DocView, model);
        }
        public IActionResult DeleteDocument(string id)
        {
            var doc = DocumentService.Get(id);
            DocumentService.Delete(id);
            return RedirectToAction(TaxAction, new { Id = doc.OwnerId } );
        }
        
        [HttpPost]
        public IActionResult DocumentView(DocumentViewModel model)
        {
            switch (model.Modus)
            {
                case Modus.Readonly:
                    break;
                case Modus.Edit:
                    {
                        var doc = DocumentService.Get().FirstOrDefault(x => x.Id == model.Id);
                        doc.Comment = model.Comment;
                        DocumentService.Update(doc);
                        break;
                    }
                case Modus.Create:
                    {
                        if (ModelState.IsValid && model != null && model.Document != null)
                            DocumentService.CreateDocument(model);
                        break;
                    }
            }
            return RedirectToAction(TaxAction, new { Id = model.OwnerId });
        }
        void FillTaxRoutes(DocumentViewModel model)
        {
            model.AspController = "Mandant";            
            model.AspAction = TaxAction;
            model.Tooltip = $"Back to tax details '{model.MandantName} - {model.Year}'";
        }
        #endregion        

        [HttpPost]
        public ActionResult OnMandant(Mandant m)
        {
            MandantId = m.SelectedMandant;
            return RedirectToAction(DefaultAction);
        }
    }
}
