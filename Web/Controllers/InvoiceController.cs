using BsnssX.Core;
using BsnssX.Core.Extensions;
using BsnssX.Core.Interfaces;
using BsnssX.Core.Models;
using BsnssX.Core.Services;
using BsnssX.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Web.Controllers;
using Web.Extensions;
using Web.Helpers;
using Web.Models;

namespace Web
{
    public class InvoiceController : MyBaseController
    {
        const string DefaultAction = "Index";
        const string DocView = "DocumentView";
        static HttpClient s_HttpClient = new HttpClient();
    
        public InvoiceController(
            IInvoiceService service,
            IExpenseService expensesService,
            IMandantService mandantService,
            IDocumentService documentService,
            IAddressService addressService,
            INoteService noteService,
            IContactInfoService ciService,
            IConfigurationService configurationService,
            IInvoiceExpenseStateService invoiceExpenseStateService,
            IEmailConfigService emailConfigService)
        {
            ConfigurationService = configurationService;
            InvoiceService = service;
            MandantService = mandantService;
            ExpenseService = expensesService;
            DocumentService = documentService;            
            AddressService = addressService;
            NoteService = noteService;
            ContactInfoService = ciService;
            InvoiceExpenseStateService = invoiceExpenseStateService;
            EmailConfigService = emailConfigService;
        }

        void Filter(InvoiceExpensesViewModel model, List<int> months)
        {
            model.Expenses = model.Expenses
                .Where(x => x.Expense.PaymentDate.HasValue)
                .Where(x => months.Contains(x.Expense.PaymentDate.Value.Month))
                .ToList();
            model.Invoices = model.Invoices
                .Where(x => months.Contains(x.Invoice.InvoicePaymentDate.Month))
                .ToList();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var mandantId = MandantId;
            int year = Year;
            InvoiceExpensesViewModel model = new InvoiceExpensesViewModel();
            model.SelectedYear = year.ToString();
            model.Mandant = MandantService.Get(mandantId);

            model.InvoiceExpenseState = InvoiceExpenseStateService.Get(mandantId, year).Status;
            

            model.Invoices = InvoiceService.Get(mandantId, year)
                .Select(x => new InvoiceViewModel(x, DocumentService.GetListByOwnerId(x.Id).Count))
                .ToList();

            model.Expenses = ExpenseService.Get(mandantId, year)
                .Select(x => new ExpenseViewModel(x, DocumentService.GetListByOwnerId(x.Id).Count))
                .ToList();

            var YearsList = model.Mandant.Years.OrderByDescending(x => x)
                .Select(it => new SelectListItem() { Text = it.ToString(), Value = it.ToString() })
                .ToList();
            ViewBag.Years = YearsList;
            ViewBag.ReportList = ControllerUtils.CreateReportList();
            model.Notes = ControllerUtils.GenerateNotes();
            model.Notes.ForEach(n => n.MandantId = model.Mandant.Id);
            model.Notes.ForEach(n => n.OwnerId = model.ID);
            Filter(model, DateUtils.GetMonthsForString(Period, out string xxx));
            model.SelectedReportPeriod = Period;
            model.Title = $"Report for {Period}-{year}";
            model.Emails = ContactInfoService.ListReportEmailReceivers(MandantId)
                .Select(x => new EmailModel(x))
                .ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult OnYear(InvoiceExpensesViewModel model)
        {
            Year = int.Parse(model.SelectedYear);
            return RedirectToAction(DefaultAction);
        }
        [HttpPost]
        public ActionResult OnPeriodChanged(InvoiceExpensesViewModel model)
        {
            Period = model.SelectedReportPeriod;
            return RedirectToAction(DefaultAction);
        }
        public ActionResult Details(string id)
        {
            InvoiceViewModel model = new InvoiceViewModel(InvoiceService.Get(id), 0);
            model.Invoice.Calculate();
            ViewBag.ImageFile = MandantService.Get(model.Invoice.MandantId).Image;
            return View(model);
        }
        #region reporting                

        [HttpPost]        
        public ActionResult SendReport(InvoiceExpensesViewModel model)
        {
            var emailRecipients = model.Emails.
                Where(x => x.IsChecked).
                Select(x => x.Email).
                ToList();

            if (emailRecipients.Count < 1)
                return RedirectToAction(DefaultAction);
            var from = GetMandant().Email;
            if (string.IsNullOrEmpty(from))
                return RedirectToAction(DefaultAction);

            var period = model.SelectedReportPeriod;
            var year = model.SelectedYear;
            var url = HttpContext.Request.GetEncodedUrl().Replace("SendReport", "ShowReport");
            url += $"/{MandantId}?year={year}&period={period}";
            
            
            var body = s_HttpClient.GetStringAsync(url).Result;
            DateUtils.GetMonthsForString(period, out string p);
            string subject = $"Report {p} - {year}";

            var sender = new SendEmail();
            bool success = sender.SendHtmlEmail(EmailConfigService.Get().FirstOrDefault(), from, emailRecipients, subject, body);
            
            Note newNote = new Note();
            newNote.MandantId = MandantId;
            newNote.Title = subject;
            newNote.Text = sender.Info;
            newNote.HtmlText = body;
            newNote.Date = DateTime.Now;
            NoteService.Add(newNote);

            return RedirectToAction(DefaultAction);
        }

        public ActionResult ShowReport(string id, int year, string period)
        {
            var model = CreateReportModel(id, year, period);
            return View("Report", model);
        }

        public InvoiceExpensesViewModel CreateReportModel(string id, int year, string period)
        {
            var mandantId = id;
            InvoiceExpensesViewModel model = new InvoiceExpensesViewModel();
            model.SelectedYear = year.ToString();
            model.Mandant = MandantService.Get(mandantId);

            model.Invoices = InvoiceService.Get(mandantId, year)
                .Select(x => new InvoiceViewModel(x, 0))
                .ToList();

            model.Expenses = ExpenseService.Get(mandantId, year)
                .Select(exp => new ExpenseViewModel(exp, 0))
                .ToList();

            string x;
            List<int> months = DateUtils.GetMonthsForString(period, out x);
            Filter(model, months);
            model.Title = $"Report for {x}-{year}";
            // filter            
            return model;
        }
        #endregion

        public ActionResult CreatePdf(string id)
        {
            var url = HttpContext.Request.GetEncodedUrl().Replace("CreatePDF", "Details");
            var invoice = InvoiceService.Get(id);
            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument pdf = converter.ConvertUrl(url);
            var bytes = pdf.Save();
            return File(bytes, BsnssX.Core.Config.PdfContentType, $"Invoice-{invoice.InvoiceNumber}.pdf");
        }

        #region Expense : Create, Detatils, Edit, Delet
        [Authorize(Roles = "Admin")]
        public ActionResult ExpenseDetails(string id)
        {
            var expense = ExpenseService.Get(id);
            var mandant = MandantService.Get(expense.MandantId);            
            var attachments = DocumentService.GetListByOwnerId(id);
            var model = new ExpenseEditCreateModel(expense, mandant, attachments);
            model.Modus = Modus.Readonly;

            return View("ExpenseEdit", model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult ExpenseCreate(string id, int year)
        {
            var mandant = MandantService.Get(id);
            var model = new ExpenseEditCreateModel(mandant);
            model.Modus = Modus.Create;
            ViewBag.States = ControllerUtils.GetExpenseStates();
            ViewBag.Vendors = ControllerUtils.GetVendorsList(mandant);
            return View("ExpenseEdit", model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ExpenseEdit(string id)
        {
            var expense = ExpenseService.Get(id);
            var mandant = MandantService.Get(expense.MandantId);
            var attachments = DocumentService.GetListByOwnerId(id);
            var model = new ExpenseEditCreateModel(expense, mandant, attachments);
            model.Modus = Modus.Edit;
            ViewBag.States = ControllerUtils.GetExpenseStates();
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteExpense(string id)
        {
            ExpenseService.Delete(id);
            return RedirectToAction(DefaultAction);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ExpenseEdit(ExpenseEditCreateModel model)
        {
            var mandant = MandantService.Get(model.MandantId);
            var vendorAddress = AddressService.Get(model.SelectedVendor);
            var expense = ExpenseService.Get(model.Id);
            if (expense == null) // coming from create
            {
                expense = model.BuildExpense(mandant, vendorAddress);
                ExpenseService.Add(expense);
            }
            else
            {
                model.MapExpense(mandant, expense, vendorAddress);
                ExpenseService.Update(expense);
            }
            return RedirectToAction(DefaultAction);
        }
        #endregion

        #region invoice
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteInvoice(string id)
        {
            InvoiceService.Delete(id);
            return RedirectToAction(DefaultAction);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult InvoiceCreate(string id, int year)
        {
            var model = new InvoiceEditCreateModel(GetMandant());
            ViewBag.States = ControllerUtils.GetInvoiceStates();
            return View("InvoiceEdit", model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult InvoiceEdit(string id)
        {
            var invoice = InvoiceService.Get(id);
            var mandant = MandantService.Get(invoice.MandantId);
            var attachments = DocumentService.GetListByOwnerId(id);
            ViewBag.States = ControllerUtils.GetInvoiceStates();
            var model = new InvoiceEditCreateModel(invoice, mandant, attachments);
            if (InvoiceExpenseStateService.Get(invoice.MandantId, invoice.Year).Status != InvoiceExpenseState.State.Open)
                model.IsReadOnly = true;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult InvoiceEdit(InvoiceEditCreateModel model)
        {
            var invoice = InvoiceService.Get(model.Id);
            if (invoice == null) // coming from create
            {
                var mandant = MandantService.Get(model.MandantId);
                invoice = model.BuildInvoice(mandant);
                InvoiceService.Add(invoice);
            }
            else
            {
                model.MapInvoice(invoice);
                InvoiceService.Update(invoice);
            }
            return RedirectToAction(DefaultAction);
        }
        #endregion


        #region document management     

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteDocument(string id)
        {
            DocumentService.Delete(id);
            return RedirectToAction(DefaultAction);
        }
        public IActionResult EditInvoiceDocument(string id) => EditDocument(id, Consts.Invoice);

        public IActionResult EditExpenseDocument(string id) => EditDocument(id, Consts.Expense);

        public IActionResult EditDocument(string id, string type)
        {
            var doc = DocumentService.Get().FirstOrDefault(x => x.Id == id);
            DocumentViewModel model = new DocumentViewModel(doc, GetMandant());
            model.AspController = "Invoice";
            if (type == Consts.Invoice)
            {
                model.AspAction = "InvoiceEdit";
                model.Tooltip = $"Back to invoice";
            }
            else {
                model.AspAction = "ExpenseEdit";
                model.Tooltip = $"Back to expense";
            }
            return View(DocView, model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddExpenseDocument(string id) => DocumentView(id, Consts.Expense);

        [Authorize(Roles = "Admin")]
        public IActionResult AddInvoiceDocument(string id) => DocumentView(id, Consts.Invoice);

        [Authorize(Roles = "Admin")]
        public IActionResult DocumentView(string id, string docType)
        {
            var mandant = MandantService.Get(MandantId);
            DocumentViewModel model = new DocumentViewModel();

            model.Year = Year;
            model.MandantId = mandant.Id;
            model.MandantName = mandant.Address.Name;
            model.DocumentType = docType;
            model.OwnerId = id;
            model.AspAction = "DocumentView";
            model.AspController = "Invoice";

            if (docType == Consts.Invoice)
            {
                var inv = InvoiceService.Get(id);
                model.Info1 = inv.InvoiceNumber;
                model.Info2 = inv.InvoiceBillingDate.ToShortDateString();
            }
            else
            {
                var exp = ExpenseService.Get(id);
                model.Info1 = exp.Number;
                model.Info2 = exp.PaymentDate?.ToShortDateString();               
            }
            return View("DocumentView", model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
            if (model.DocumentType == Consts.Invoice)
                return RedirectToAction("InvoiceEdit", new { Id = model.OwnerId });
            return RedirectToAction("ExpenseEdit", new { Id = model.OwnerId });
        }
        #endregion
    }
}
