using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.Hubs;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.ViewModels;

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


        // new paper 
        [HttpGet]
        public IActionResult NewPaper() { 
            return View( new Paper());
        }
        [HttpPost]
        public IActionResult NewPaper(Paper newPaper) {
            try {

                if (ModelState.IsValid)
                {
                    _businessLogicL.addPaper(newPaper);
                    return RedirectToAction("viewallpaper", "inventory");
                }
                else {
                    return View("newpaper", newPaper);
                }
            
            } catch (Exception ex) {
                return BadRequest(ex);
            }
            
        }
        

        // new ink

        [HttpGet]
        public IActionResult NewInk() { 
            return View(new Ink());
        }

        [HttpPost]
        public IActionResult NewInk(Ink newInk) {
            try
            {
                //newInk.Activated = true;

                if (ModelState.IsValid)
                {
                    _businessLogicL.addInk(newInk);
                    return RedirectToAction("ViewAllInk", "inventory");
                }
                else { 
                    return View(newInk) ;
                }

            } catch (Exception ex) {

                return BadRequest(ex);
            }
        }

        // new supply

        [HttpGet]
        public IActionResult NewSupply() {
             return View(new Supply());
        }

        [HttpPost]
        public IActionResult NewSupply(Supply newSupply) {

            try
            {

                if (ModelState.IsValid)
                {
                    _businessLogicL.addSupply(newSupply);
                    return RedirectToAction("viewallsupply", "inventory");
                }
                else
                {
                    return View(newSupply);
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }

        // paper purchase

        [HttpGet]
        public async Task <IActionResult> paperPurchase() 
        {

            ViewBag.paperList = _businessLogicL.getAllActivePaper();
            ViewBag.vendorList = _businessLogicL.ViewAllVendor();
            return View();

        }

        [HttpPost]
        public IActionResult paperPurchase(purchaseOrderDTO purchaseDto)
        {
            //store the employeeid in the dto
            string employeeId = HttpContext.Session.GetString("EmployeeID");
            purchaseDto.EmployeeId = employeeId;
            _businessLogicL.purchaseNewPaper(purchaseDto);
            return RedirectToAction("ViewAllPaper", "inventory");


        }



        // ink purchase

        public IActionResult inkPurchase() {
            ViewBag.inkList = _businessLogicL.getAllActiveInk();
            ViewBag.vendorList = _businessLogicL.ViewAllVendor();

            return View(); 
        }

        [HttpPost]
        public IActionResult inkPurchase(purchaseOrderDTO purchaseDto)
        {
            //store the employeeid in the dto
            string employeeId = HttpContext.Session.GetString("EmployeeID");
            purchaseDto.EmployeeId = employeeId;
            _businessLogicL.purchaseNewInk(purchaseDto);
            return RedirectToAction("ViewAllink", "inventory");


        }

        // supply purchase

        public IActionResult supplypurchase()
        {
            ViewBag.supplyList = _businessLogicL.getAllActiveSupply();
            ViewBag.vendorList = _businessLogicL.ViewAllVendor();

            return View();
        }

        [HttpPost]
        public IActionResult supplyPurchase(purchaseOrderDTO purchaseDto)
        {
            //store the employeeid in the dto
            string employeeId = HttpContext.Session.GetString("EmployeeID");
            purchaseDto.EmployeeId = employeeId;
            _businessLogicL.purchaseNewSupply(purchaseDto);
            return RedirectToAction("viewallsupply", "inventory");


        }

        // paper Reports

        [HttpGet]
        public IActionResult inventoryReports()
        {
            ViewBag.PaperList = _businessLogicL.GetActivePapers();
            ViewBag.InkList = _businessLogicL.GetActiveInks();
            ViewBag.SupplyList = _businessLogicL.GetActiveSupplies();

            return View();
        }
        [HttpPost]
        public IActionResult inventoryReports(string itemType, int itemId, DateOnly beginingDate, DateOnly endingDate)
        {
            InventoryReportViewModel invViewModel= _businessLogicL.invetoryReports(itemType, itemId, beginingDate, endingDate);
           
            return View(invViewModel);

        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Mariam section

        [HttpGet]
        public IActionResult VendorReport()
        {

            return View(new InventoryReportViewModel());
        }

        [HttpPost]
        public IActionResult VendorReport(DateOnly beginingDate, DateOnly endingDate)
        {

            InventoryReportViewModel invViewModel = _businessLogicL.VendorReportRanking(beginingDate, endingDate);

            return View(invViewModel);
        }
        [HttpGet]
        public IActionResult EditVendor(int vendorID) {


            Vendor foundVendor = _businessLogicL.GetVendorByID(vendorID);
            return View("~/Views/Inventory/EditVendor.cshtml", foundVendor);

        }

        [HttpPost]
        public IActionResult EditVendor(int vendorID, Vendor newvendor)
        {
            if (!ModelState.IsValid)
            {
                // Pass newvendor back to the view so that the user
                // does not lose the entered data
                return View("~/Views/Inventory/EditVendor.cshtml", newvendor);
            }

            bool isVendorEdited = _businessLogicL.EditVendor(vendorID, newvendor);

            if (!isVendorEdited)
            {
                ModelState.AddModelError("", "البريد الالكتروني او رقم الهاتف تم استخدامه من قبل");
                // Also pass newvendor back here
                return View("~/Views/Inventory/EditVendor.cshtml", newvendor);
            }

            return RedirectToAction("ViewAllVendor", "Inventory");
        }



////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Sherwet section

        [HttpGet]
        public IActionResult AddVendor()
        {
            try
            {
                VendorAddDTO empty = new VendorAddDTO();
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
        public IActionResult AddVendor(VendorAddDTO vendor)
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
        //edit ink
        [HttpGet]
        public IActionResult EditInk(int inkId)
        {
            try
            {
                Ink ink = _businessLogicL.GetInkByID(inkId);
                return View("~/Views/Inventory/EditInk.cshtml", ink);
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
        public IActionResult EditInk(int inkId, Ink updatedInk)
        {
            try
            {
                if (updatedInk == null)
                {
                    return BadRequest("Invalid data.");
                }
                bool isEditSuccess = _businessLogicL.EditInk(inkId, updatedInk);
                if (!isEditSuccess)
                {
                    ModelState.AddModelError("", "قيمة نقطة اعادة الشراء يجب ان تكون اكبر من صفر");
                    return View("~/Views/Inventory/EditInk.cshtml", updatedInk);

                }
                return RedirectToAction("ViewAllInk", "Inventory");
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

        //edit paper
        [HttpGet]
        public IActionResult EditPaper(int paperId)
        {
            try
            {
                Paper paper = _businessLogicL.GetPaperByID(paperId);
                return View("~/Views/Inventory/EditPaper.cshtml", paper);
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
        public IActionResult EditPaper(int paperId, Paper updatedPaper)
        {
            try
            {
                if (updatedPaper == null)
                {
                    return BadRequest("Invalid data.");
                }


                bool isEditSuccess = _businessLogicL.editPaper(paperId, updatedPaper);


                if (!isEditSuccess)
                {
                    ModelState.AddModelError("", "قيمة نقطة اعادة الشراء يجب ان تكون اكبر من صفر");
                    return View("~/Views/Inventory/EditPaper.cshtml", updatedPaper);
                }
                return RedirectToAction("ViewAllPaper", "Inventory");

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

        //edit supply
        [HttpGet]
        public IActionResult EditSupply(int suppliesId)
        {
            try
            {
                Supply supply = _businessLogicL.GetSupplyByID(suppliesId);
                return View("~/Views/Inventory/EditSupply.cshtml", supply);
            }

            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message); // Internal server error
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); // Employee not found
            }
        }
        [HttpPost]
        public IActionResult EditSupply(int suppliesId, Supply updatedSupply)
        {
            try
            {
                if (updatedSupply == null)
                {
                    return BadRequest("Invalid data.");
                }
                bool isEditSuccess = _businessLogicL.editSupply(suppliesId, updatedSupply);
                if (!isEditSuccess)
                {
                    ModelState.AddModelError("", "قيمة نقطة اعادة الشراء يجب ان تكون اكبر من صفر");
                    return View("~/Views/Inventory/EditSupply.cshtml", updatedSupply);

                }
                return RedirectToAction("ViewAllSupply", "Inventory");
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

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
            await _hubContext.Clients.All.SendAsync("CheckInkReorderPoint" , "Reorder point reached for Ink: [InkName]");
            return View(inkList);
        }

        public async Task<IActionResult> ViewAllPaper()
        {
            List<Paper> paperList = await _businessLogicL.ViewAllPaper();
            await _hubContext.Clients.All.SendAsync("ReceiveReorderMessage", "Reorder point reached for paper: [PaperName]");
            return View(paperList);
        }

        public async Task<IActionResult> ViewAllSupply()
        {
            List<Supply> suppplyList = await _businessLogicL.ViewAllSupply();
            await _hubContext.Clients.All.SendAsync("CheckSupplyReorderPoint", "Reorder point reached for Supply: [SupplyName]");
            return View(suppplyList);
        }

        [HttpGet]
        public IActionResult PhysicalCount()
        {
            ViewBag.PaperList = _businessLogicL.GetActivePapers();
            ViewBag.InkList = _businessLogicL.GetActiveInks();
            ViewBag.SupplyList = _businessLogicL.GetActiveSupplies();
            return View();
        }

        public JsonResult GetCurrentQuantity(string itemType, int itemId)
        {
            int quantity = _businessLogicL.GetCurrentQuantity(itemType, itemId);
            return Json(new { currentQuantity = quantity });
        }

        [HttpPost]
        public IActionResult PhysicalCount(PhysicalCountDTO phcountDto)
        {
            string employeeId = HttpContext.Session.GetString("EmployeeID");
            phcountDto.employeeId = employeeId;



            var result = _businessLogicL.UpdateQuantity(phcountDto);

            if (result.Success)
                TempData["Success"] = result.Message;
            else
                TempData["Error"] = result.Message;

            return RedirectToAction("PhysicalCount");
        }
    }
}
