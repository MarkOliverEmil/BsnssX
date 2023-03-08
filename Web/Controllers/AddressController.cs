using BsnssX.Core.Interfaces;
using BsnssX.Core.Models;
using BsnssX.Core.Utils;
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

        #region contact info add, edit, delete
        public ActionResult AddContactInfo(string id)
        {
            var adr = AddressService.Get(id);
            ContactInfoModel model = new ContactInfoModel();
            model.ContactInfo = new ContactInfo();
            model.ContactInfo.Id = IdGenerator.CreateId();
            model.ContactInfo.OwnerId = adr.Id;
            model.Name = adr.Name;
            model.Modus = Modus.Create;            
            return View("ContactInfo", model);
        }           
        public ActionResult EditContactInfo(string id)
        {            
            ContactInfoModel model = new ContactInfoModel();
            model.ContactInfo = ContactInfoService.Get(id);            
            var adr = AddressService.Get(model.ContactInfo.OwnerId);
            model.Name = adr.Name;
            model.Modus = Modus.Edit;
            model.AspController = "Address";
            model.AspActionDelete = "DeleteContactInfo";
            return View("ContactInfo", model);
        }
        public ActionResult DeleteContactInfo(string id)
        {
            ContactInfoService.Delete(id);
            return RedirectToAction(DefaultAction);
        }
        [HttpPost]
        public IActionResult EditContactInfo(ContactInfoModel model)
        {
            if (model.Modus == Modus.Create)
            {
                ContactInfoService.Add(model.ContactInfo);
            }
            else if (model.Modus == Modus.Edit)
            {
                ContactInfoService.Update(model.ContactInfo);
            }            
            return RedirectToAction(DefaultAction);
        }
        #endregion
    }
}
