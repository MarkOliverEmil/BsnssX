using BsnssX.Core.Interfaces;
using BsnssX.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AddressController : MyBaseController
    {
        const string DefaultAction = "Index";
        
        public AddressController(IAddressService addressService, IContactInfoService contactInfoService)
        {
            AddressService = addressService;
            ContactInfoService = contactInfoService;
        }
        AddressView FromAddress(Address adr)
        {
            var res = new AddressView();
            res.Address = adr;
            res.ContactInfos = ContactInfoService.GetByOwnerId(adr.Id);
            return res;
        }
        public ActionResult Index()
        {
            var adrs = AddressService.Get().OrderBy(x => x.Name).ToList();
            var model = new AddressListViewModel();
            model.Addresses = adrs.Select(a => FromAddress(a)).ToList();
            return View(model);
        }
        public IActionResult AddressCreate() => View("AddressEdit", new AddressView(true));
        
        public ActionResult AddressEdit(string id)
        {
            var adr = AddressService.Get(id);
            var model = FromAddress(adr);
            return View(model);
        }        
        [HttpPost]
        public IActionResult AddressEdit(AddressView model)
        {
            if (model.IsNew)
                AddressService.Add(model.Address);            
            else
                AddressService.Update(model.Address);
            return RedirectToAction(DefaultAction);
        }
    }
}
