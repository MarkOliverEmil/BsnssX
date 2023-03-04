using BsnssX.Core.Interfaces;
using BsnssX.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace BsnssX.Core.Services
{
    public class InvoiceService : Service<Invoice>, IInvoiceService
    {        
        public InvoiceService()
        {
            FileName = "Invoices.json";
            Load();
        }

        public List<Invoice> Get(string mandantId, int year)
        {
            var res = Get()
                .Where(x => x.MandantId == mandantId)
                .Where(x => x.Year == year)
                .OrderByDescending(x => x.InvoiceBillingDate)
                .ToList();
            return res;
        }

        //void Save() => DbUtils<Invoice>.Save(FileName, s_items);
        //public Invoice Add(Invoice obj)
        //{
        //    obj.Calculate();
        //    s_items.Add(obj);
        //    Save();
        //    return obj;
        //}               

        //public void Delete(string id)
        //{
        //    s_items = s_items.Where(x => x.Id != id).ToList();
        //    Save();
        //    return;
        //}

        //public List<Invoice> Get()
        //{

        //    if (s_items == null)
        //        s_items = DbUtils<Invoice>.Load(FileName);
        //    return s_items;
        //}
        //public List<Invoice> Get(string mandantId, int year)
        //{
        //    var res = Get()
        //        .Where(x => x.MandantId == mandantId)
        //        .Where(x => x.Year == year)
        //        .OrderByDescending(x => x.InvoiceBillingDate)
        //        .ToList();
        //    return res;
        //}



        //public Invoice Get(string id) => Get().FirstOrDefault(x => x.Id == id);


        //public Invoice Update(Invoice obj)
        //{
        //    obj.Calculate();
        //    return obj;
        //}
    }
}
