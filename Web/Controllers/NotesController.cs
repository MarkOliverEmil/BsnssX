using BsnssX.Core;
using BsnssX.Core.Interfaces;
using BsnssX.Core.Models;
using BsnssX.Core.Services;
using BsnssX.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using Web.Extensions;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class NotesController : MyBaseController
    {
        const string DefaultAction = "Index";
        
        public NotesController(INoteService service, IDocumentService documentService)
        {
            NoteService = service;
            DocumentService = documentService;
        }
        
        public ActionResult Index()
        {
            var model = new NotesListViewModel()
            {
                Notes = NoteService.Get().OrderByDescending(x => x.Date).Select(x => new NoteView(x)).ToList()
            };
            AddViewBagsListMode(null, null);
            return View(model);
        }
        public IActionResult DeleteNote(string id)
        {
            NoteService.Delete(id);
            return RedirectToAction(DefaultAction);
        }        
        public IActionResult NoteCreate()
        {
            var newItem = new Note();
            newItem.Date = System.DateTime.Now;
            newItem.NoteType = Consts.General;
            newItem.Year = newItem.Date.Year;
            return View("NotesEdit", newItem);
        }
        public ActionResult NotesEdit(string id)
        {
            var model = NoteService.Get(id);
            ViewBag.IsLeistungsnachweis = model.IsLeistungsnachweis();
            return View(model);
        }
        [HttpPost]
        public IActionResult NotesEdit(Note model)
        {
            if (model.Id == null)
            {
                model.Id = IdGenerator.CreateId();
                NoteService.Add(model);
            }
            else
            {
                NoteService.Update(model);
            }
            return RedirectToAction(DefaultAction);
        }
        #region Filtering, helpers        
        [HttpPost]
        public ActionResult OnFilterChanged(NotesListViewModel param)
        {
            var model = GetListModel(param.SelectedNoteType, param.SelectedYear);
            AddViewBagsListMode(param.SelectedNoteType, param.SelectedYear);
            return View("Index", model);
        }

        NotesListViewModel GetListModel(string selectedNoteType, string selectedYear)
        {
            var model = new NotesListViewModel()
            {
                Notes = NoteService.Get().OrderByDescending(x => x.Date).Select(x => new NoteView(x)).ToList(),
                SelectedNoteType = selectedNoteType,
                SelectedYear = selectedYear
            }.FilterNotes();
            
            AddViewBagsListMode(selectedNoteType, selectedYear);
            return model;
        }

        void AddViewBagsListMode(string selectedDocType, string selectedYear)
        {
            ViewBag.DocTypes = NoteService.GetTypes(selectedDocType);
            ViewBag.Years = NoteService.GetYears(selectedYear);
        }
        #endregion

       
        public IActionResult CreateLeistungsnachweisPdf(string title, int year, string text)
        {
            int month = 0;
            if (title.Length >= 4)
            {
                var ms = title.Substring(2, 2);
                if (int.TryParse(ms, out month) == false)
                    return Ok();                
            }
            if (month < 1 || month > 12)
                return Ok();

            string fileName = $"LN_{year}_{month}_{IdGenerator.CreateId()}.pdf";
            var filePath = Path.Combine(Config.TmpDir, fileName);
            Maintenance.CleanTmpDir();
            CreateLeistungsnachweis.Create(text, month, year, filePath);
            var stream = new FileStream(filePath, FileMode.Open);
            return File(stream, Config.PdfContentType, fileName);
        }
    }
}
