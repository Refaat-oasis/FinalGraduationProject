using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.Hubs;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.Controllers
{
    public class InventoryController : Controller
    {

        private readonly InventoryBussinesLogicLayer _businessLogicL;
  
        private readonly IHubContext<ProductHub> _hubContext;
        public InventoryController(InventoryBussinesLogicLayer businessLogicL , IHubContext<ProductHub> hubContext)
        {
            _businessLogicL = businessLogicL;
            _hubContext = hubContext;
        }

        // refaat section
        [HttpGet]
        public IActionResult NewPaper() { 
            return View( new Paper());
        }
        [HttpPost]
        public IActionResult NewPaper(Paper newPaper) {
            try {

                if (!ModelState.IsValid)
                {
                    return View("newpaper",NewPaper());

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
            return View(new Ink());
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
             return View(new Supply());
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
        public IActionResult EditVendor(int vendorID, VendorEditDTO newvendor) {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Inventory/EditVendor.cshtml");
            }

            bool isVendorEdited = _businessLogicL.EditVendor(vendorID, newvendor);

            if (!isVendorEdited)
            {
                ModelState.AddModelError("", "البريد الالكتروني او رقم الهاتف تم استخدامه من قبل");
                return View(newvendor);
            }

            return RedirectToAction("ViewAllVendor", "Inventory");

            //if (newvendor == null)
            //{
            //    return BadRequest("Invalid data.");
            //}

            //try
            //{
            //    bool isEditSuccess = _businessLogicL.EditVendor(vendorID, newvendor); 


            //    if (!isEditSuccess)
            //    {
            //        ModelState.AddModelError("", "البريد الالكتروني او رقم الهاتف  تم استخدامه من قبل");
            //        return View(newvendor);
            //    }
            //    return RedirectToAction("ViewAllVendor"); 

            //}
            //catch (ArgumentException ex)
            //{
            //    return NotFound(ex.Message); 
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, ex.Message);     
            //}
        }


        // Sherwet section
        [HttpGet]
        public IActionResult AddVendor()
        {
            try
            {
                VendorEditDTO empty = new VendorEditDTO();
                return View("~/Views/Inventory/AddVendor.cshtml", empty);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message); // Internal server error
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        public IActionResult AddVendor(VendorEditDTO vendor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("~/Views/Inventory/AddVendor.cshtml");
                }

                bool isVendorAdded = _businessLogicL.AddVendor(vendor);

                if (!isVendorAdded)
                {
                    ModelState.AddModelError("", "الايميل او رقم الهاتف تم استخدامه من قبل");
                    return View(vendor);
                }

                return RedirectToAction("ViewAllVendor", "inventory");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Internal server error
            }
        }

        // Sandra section
        [HttpGet]
        public IActionResult ViewAllVendor()
        {
            List<Vendor> vendorList = _businessLogicL.ViewAllVendor();
            return View("~/Views/Inventory/ViewAllVendor.cshtml", vendorList);
        }


        public async Task <IActionResult> ViewAllInk()
        {
            List<Ink> inkList = await _businessLogicL.ViewAllInk();
            await _hubContext.Clients.All.SendAsync("CheckInkReorderPoint");
            return View(inkList);
        }
        public async Task<IActionResult> ViewAllPaper()
        {
            List<Paper> paperList = await _businessLogicL.ViewAllPaper();
            await _hubContext.Clients.All.SendAsync("CheckPaperReorderPoint");
            return View(paperList);
        }

        public async Task<IActionResult> ViewAllSupply()
        {
            List<Supply> suppplyList = await _businessLogicL.ViewAllSupply();
            await _hubContext.Clients.All.SendAsync("CheckSupplyReorderPoint");
            return View(suppplyList);
        }


    }
}
