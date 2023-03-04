using BsnssX.Core.Interfaces;
using BsnssX.Core.Models;
using BsnssX.Core.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Web.Helpers
{
    public class ControllerUtils
    {
        public static SelectListItem CreateSelectListItem(string text, string value)
        {
            var res = new SelectListItem()
            {
                Text = text,
                Value = value
            };
            return res;
        }
        public static SelectListItem CreateSelectListItem(string text)
        {
            var res = new SelectListItem()
            {
                Text = text,
                Value = text
            };
            return res;
        }


        public static List<SelectListItem> CreateReportList()
        {
            List<SelectListItem> res = new List<SelectListItem>();
            res.Add(CreateSelectListItem("Jan - Dec", "Year"));
            res.Add(CreateSelectListItem("Quartal 1", "Q1"));
            res.Add(CreateSelectListItem("Quartal 2", "Q2"));
            res.Add(CreateSelectListItem("Quartal 3", "Q3"));
            res.Add(CreateSelectListItem("Quartal 4", "Q4"));
            for (int i = 1; i <= 12; i++)
            {
                res.Add(CreateSelectListItem(DateUtils.GetMonthName(i), i.ToString()));
            }
            return res;
        }
        public static List<SelectListItem> GetExpenseStates()
        {
            List<SelectListItem> res = new List<SelectListItem>();
            res.Add(new SelectListItem() { Text = Expense.State.Open.ToString(), Value = Expense.State.Open.ToString() });
            res.Add(new SelectListItem() { Text = Expense.State.Payed.ToString(), Value = Expense.State.Payed.ToString() });
            res.Add(new SelectListItem() { Text = Expense.State.Denied.ToString(), Value = Expense.State.Denied.ToString() });
            return res;
        }
        public static List<SelectListItem> GetInvoiceStates()
        {
            List<SelectListItem> res = new List<SelectListItem>();
            res.Add(new SelectListItem() { Text = Invoice.State.Created.ToString(), Value = Invoice.State.Created.ToString() });
            res.Add(new SelectListItem() { Text = Invoice.State.Payed.ToString(), Value = Invoice.State.Payed.ToString() });
            res.Add(new SelectListItem() { Text = Invoice.State.Billed.ToString(), Value = Invoice.State.Billed.ToString() });
            res.Add(new SelectListItem() { Text = Invoice.State.Denied.ToString(), Value = Invoice.State.Denied.ToString() });
            return res;
        }
        public static List<SelectListItem> GetVendorsList(Mandant mandant)
        {
            var res = mandant.Vendors
                .Where(v => v != null)
                .Select(v => new SelectListItem() { Text = v.Address.Name, Value = v.Address.Id })
                .ToList();
            return res;
        }
        public static List<SelectListItem> GetVendorsList(string exclude, IAddressService addressService)
        {
            var res = addressService.Get().Where(x => x.Id != exclude)
                .Select(adr => new SelectListItem() { Text = adr.Name, Value = adr.Id })
                .ToList();
            return res;
        }
        public static List<Note> GenerateNotes()
        {
            List<Note> res = new List<Note>();
            for (int i = 0; i < 10; i++)
            {
                var n = new Note();
                n.Date = DateTime.Now;
                n.Title = "Titel " + i.ToString();
                n.Text = "Hadfjkad jkajdf kjdsfkjd fjdkfjakd fajkdfdksfj adfjajsfdlkad sf";
                res.Add(n);
            }
            return res;
        }

    }
}
