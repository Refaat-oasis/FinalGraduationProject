using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.Hubs;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.Controllers
{
    public class InventoryController : Controller
    {

        private readonly InventoryBussinesLogicLayer _businessLogicL;
        private readonly ThothContext _context;
        private readonly IHubContext<ProductHub> _hubContext;
        public InventoryController(InventoryBussinesLogicLayer businessLogicL, ThothContext context, IHubContext<ProductHub> hubContext)
        {
            _businessLogicL = businessLogicL;
            _context = context;
            _hubContext = hubContext;
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
        public IActionResult EditVendor(int vendorID, Vendor newvendor) {

            if (newvendor == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                bool isEditSuccess = _businessLogicL.EditVendor(vendorID, newvendor); 


                if (!isEditSuccess)
                {
                    ModelState.AddModelError("", "البريد الالكتروني او رقم الهاتف  تم استخدامه من قبل");
                    return View(newvendor);
                }
                return RedirectToAction("ViewAllVendor"); 

            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);     
            }
        }


        // Sherwet section


        // Sandra section
        [HttpGet]
        public IActionResult ViewAllVendor()
        {
            List<Vendor> vendorList = _businessLogicL.ViewAllVendor();
            return View("~/Views/Inventory/ViewAllVendor.cshtml", vendorList);
        }

        //trying signal r
        public async Task<IActionResult> ViewAllinventory()
        {
            List<Paper> papersList = _context.Papers.ToList();
            Console.WriteLine("Calling CheckPaperReorderPoint on the hub...");
            await _hubContext.Clients.All.SendAsync("CheckPaperReorderPoint");
            return View(papersList);
        }


    }
}
