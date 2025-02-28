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
            try {

                if (!ModelState.IsValid)
                {
                    return View("");

                }
                else {

                    _businessLogicL.addPaper(newPaper);
                    return RedirectToAction("", "");
                }
            
            } catch (Exception ex) {
                return BadRequest(ex);
            }
            
        }
        
        [HttpGet]
        public IActionResult NewInk() { 
            return View();
        }
        [HttpPost]
        public IActionResult NewInk(Ink newInk) {
            try
            {

                if (!ModelState.IsValid)
                {
                    _businessLogicL.addInk(newInk);
                    return RedirectToAction("", "");
                }
                else { 
                    return View() ;
                }

            } catch (Exception ex) {

                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult NewSupply() {
             return View();
        }
        [HttpPost]
        public IActionResult NewSupply(Supply newSupply) {

            try
            {

                if (!ModelState.IsValid)
                {
                    _businessLogicL.addSupply(newSupply);
                    return RedirectToAction("", "");
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

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
