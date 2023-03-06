using BsnssX.Core;
using BsnssX.Core.Interfaces;
using BsnssX.Core.Services;
using BsnssX.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Web.Extensions;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DocumentController : MyBaseController
    {
        const string DefaultAction = "Index";
        const string DocView = "DocumentView";

        public DocumentController(        
            IMandantService mandantService,        
            IDocumentService documentService,
            IConfigurationService configurationService,        
            IWebHostEnvironment webHostEnvironment)
            
        {
            ConfigurationService = configurationService;
            MandantService = mandantService;         
            DocumentService = documentService;
            WebRootPath = webHostEnvironment.WebRootPath;
        }
        
        // list all documents for mandant
        public ActionResult Index()
        {
            var model = GetListModel(null, null);
            AddViewBagsListMode(null, null);            
            return View(model);
        }
        #region CRUD & download
        public IActionResult CreateDocument()
        {
            var mandant = MandantService.Get(MandantId);
            DocumentViewModel model = new DocumentViewModel(mandant);
            return View(DocView, model);
        }
        
        public IActionResult DisplayDocument(string id)
        {
            var mandant = MandantService.Get(MandantId);
            var doc = DocumentService.Get().FirstOrDefault(x => x.Id == id);
            DocumentViewModel model = new DocumentViewModel(doc, mandant);
            model.Modus = Modus.Readonly;
            FillRoutes(model);
            return View(DocView, model);
        }

        public IActionResult EditDocument(string id)
        {
            var mandant = MandantService.Get(MandantId);
            var doc = DocumentService.Get().FirstOrDefault(x => x.Id == id);
            DocumentViewModel model = new DocumentViewModel(doc, mandant);
            FillRoutes(model);
            return View(DocView, model);
        }
        public IActionResult DeleteDocument(string id)
        {
            DocumentService.Delete(id);
            return RedirectToAction(DefaultAction);
        }

        public IActionResult DownloadDocument(string id)
        {
            var doc = DocumentService.Get().FirstOrDefault(x => x.Id == id);
            if (doc == null)
                return View(DefaultAction);

            var filePath = Path.Combine(WebRootPath, doc.Directory, doc.StorageFile);
            var stream = new FileStream(filePath, FileMode.Open);
            return File(stream, doc.ContentType, doc.FileName);
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
                            DocumentService.CreateDocument(model, WebRootPath);
                        break;
                    }
            }
            return RedirectToAction(DefaultAction);
        }
        #endregion

        #region Filtering, helpers        
        [HttpPost]
        public ActionResult OnFilterChanged(DocumentsListViewModel param)
        {            
            var model = GetListModel(param.SelectedDocType, param.SelectedYear);
            AddViewBagsListMode(param.SelectedDocType, param.SelectedYear);
            return View("Index", model);
        }
        
        DocumentsListViewModel GetListModel(string selectedDocType, string selectedYear)
        {
            var model = new DocumentsListViewModel()
            {
                Mandant = MandantService.Get(MandantId),
                Documents = DocumentService.GetForMandant(MandantId),
                SelectedDocType = selectedDocType,
                SelectedYear = selectedYear
            }.FilterDocuments();            
            return model;
        }
        
        void AddViewBagsListMode(string selectedDocType, string selectedYear)
        {
            ViewBag.DocTypes = DocumentService.GetTypes(MandantId, selectedDocType);
            ViewBag.Years = DocumentService.GetYears(MandantId, selectedYear);
        }        

        void FillRoutes(DocumentViewModel model)
        {                           
            model.AspController = "Document";
            model.AspActionDelete = "DeleteDocument";
            model.AspAction = "Index";
            model.Tooltip = "Back to document list";
        }
        #endregion
       
        public IActionResult DownloadZip()
        {
            string fileName = $"data_{IdGenerator.CreateId()}.zip";
            var zipFile = Path.Combine(Config.TmpDir, fileName);            
            Maintenance.CleanTmpDir();
            ZipFile.CreateFromDirectory(Config.RootDir, zipFile);            
            var stream = new FileStream(zipFile, FileMode.Open);
            return File(stream, Config.ZipContentType, fileName);
        }
        public IActionResult CheckForMissingDocFiles()
        {
            var res = SanityChecks.CheckForMissingDocFiles(DocumentService);
            string fileName = $"docs_{IdGenerator.CreateId()}.txt";
            return File(Encoding.Unicode.GetBytes(res), Config.TextContentType, fileName);
        }
    }
}
