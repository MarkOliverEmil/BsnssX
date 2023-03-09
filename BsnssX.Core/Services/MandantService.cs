using BsnssX.Core.Interfaces;
using BsnssX.Core.Models;
using BsnssX.Core.Utils;
using System.Collections.Generic;
using System.Linq;

namespace BsnssX.Core.Services
{

    public class MandantService : IMandantService
    {
        const string FileName = "Mandants.json";

        static private List<Mandant> s_items;

        public MandantService()
        {
        }
        public Mandant Add(Mandant obj)
        {
            return obj;
        }

        public void Delete(string id)
        {
            ;
        }

        public List<Mandant> Get()
        {
            if (s_items == null)
            {
                s_items = new List<Mandant>();
                var m2 = DbUtils<Mandant2>.Load(FileName);
                foreach (var m in m2)
                    s_items.Add(CreateFrom(m));
            }
            return s_items;
        }

        Mandant CreateFrom(Mandant2 src)
        {
            Mandant res = new Mandant();
            res.Id = src.Id;
            res.Email = src.Email;
            res.Image = src.Image;
            res.Address = src.Address;
            res.BookableItems = src.BookableItems;
            res.Years = src.Years;

            if (src.Banks.Any())
            {
                BankDetailService bankService = new BankDetailService();
                res.BankDetails = bankService.Get(src.Banks.First());
            }

            AddressService adrService = new AddressService();
            res.Clients = src.Clients.Select(x => adrService.Get(x)).ToList();

            res.Vendors = src.Vendors.Select(x => new Vendor()
            { Id = x,
                Address = adrService.Get(x),
            }).ToList();

            res.Vendors = res.Vendors.Where(x => x.Address != null).ToList();

            DocumentService docService = new DocumentService();
            res.Documents = docService.Get().Where(x => x.MandantId == res.Id).ToList();

            res.TaxesForYear = new TaxService().GetForMandant(res.Id);
            return res;
        }


        public Mandant Get(string id) => Get().FirstOrDefault(x => x.Id == id);

        public Mandant Update(Mandant obj)
        {
            return obj;
        }        
    }
}