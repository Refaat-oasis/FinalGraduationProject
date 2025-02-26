using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.Controllers
{
    public class InventoryController : Controller
    {

        private readonly InventoryBussinesLogicLayer _businessLogicL;

        public InventoryController(InventoryBussinesLogicLayer businessLogicL)
        {
            _businessLogicL = businessLogicL;
        }

        // refaat section
        [HttpGet]
        public IActionResult NewPaper() { 
            return View();
        }
        [HttpPost]
        public IActionResult NewPaper(Paper newPaper) {
            _businessLogicL.addPaper(newPaper);
            return RedirectToAction("","");
        }
        
        [HttpGet]
        public IActionResult NewInk() { 
            return View();
        }
        [HttpPost]
        public IActionResult NewInk(Ink newInk) {
            _businessLogicL.addInk(newInk);
            return RedirectToAction("","");
        }

        [HttpGet]
        public IActionResult NewSupply() {
             return View();
        }
        [HttpPost]
        public IActionResult NewSupply(Supply NewSupply) { 
            _businessLogicL.addSupply(NewSupply);
            return RedirectToAction("","");
        
        }



        // Mariam section
        [HttpGet]
        public IActionResult EditVendor(int vendorID) {   
            Vendor foundVendor = _businessLogicL.GetVendorByID(vendorID);
            return View("~/Views/Inventory/EditVendor.cshtml", foundVendor);      
        
        }

        [HttpPost]
        public IActionResult EditVendor(int vendorID, Vendor vendor) {
            if (vendor == null) {
                return BadRequest("invalid data was added");
                
            }
            _businessLogicL.EditVendor(vendorID, vendor);
            return RedirectToAction("ViewAllVendor","Inventory");
        
        }


        // Sherwet section


        // Sandra section


    }
}
