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

        [HttpGet]
        public IActionResult NewPaper()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole ==1 || jobRole ==2 )
            {
                return View(new Paper());
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }

        }
       
        [HttpPost]
        public IActionResult NewPaper(Paper newPaper) {
            try {

                if (ModelState.IsValid)
                {
                    bool result = _businessLogicL.addPaper(newPaper);
                    string messageSuccess = "تم اضافة الورق الجديد";
                    string messageError = "هناك خظأ في اضافة الورق الجديد";
                    
                    if (result) { 
                    TempData["Success"] = messageSuccess;
                    return View("newpaper", newPaper);
                    }else {
                        TempData["Error"] = messageError;
                        return View("newpaper", newPaper);

                    }
                    

                    //return RedirectToAction("viewallpaper", "inventory");
                }
                else {
                    return View("newpaper", newPaper);
                }
            
            } catch (Exception ex) {
                return BadRequest(ex);
            }
            
        }
        
        [HttpGet]
        public IActionResult NewInk() {
           
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
            {
                return View(new Ink());
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }

        }

        [HttpPost]
        public IActionResult NewInk(Ink newInk) {
            try
            {
                //newInk.Activated = true;

                if (ModelState.IsValid)
                {
                   bool result = _businessLogicL.addInk(newInk);
                    string messageSuccess = "تم اضافة الحبر الجديد";
                    string messageError = "هناك خظأ في اضافة الحبر الجديد";

                    if (result)
                    {
                        TempData["Success"] = messageSuccess;
                        return View( newInk);
                    }
                    else
                    {
                        TempData["Error"] = messageError;
                        return View(newInk);

                    }


                    //return RedirectToAction("ViewAllInk", "inventory");
                }
                else { 
                    return View(newInk) ;
                }

            } catch (Exception ex) {

                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult NewSupply() {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
            {
                return View(new Supply());
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
            

        }

        [HttpPost]
        public IActionResult NewSupply(Supply newSupply) {

            try
            {

                if (ModelState.IsValid)
                {
                   bool result = _businessLogicL.addSupply(newSupply);

                    string messageSuccess = "تم اضافة الورق الجديد";
                    string messageError = "هناك خظأ في اضافة الورق الجديد";

                    if (result)
                    {
                        TempData["Success"] = messageSuccess;
                        return View(newSupply);
                    }
                    else
                    {
                        TempData["Error"] = messageError;
                        return View(newSupply);

                    }
                    //return RedirectToAction("viewallsupply", "inventory");
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

        [HttpGet]
        public async Task <IActionResult> paperPurchase() 
        {

            
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
            {
                ViewBag.paperList = _businessLogicL.getAllActivePaper();
                ViewBag.vendorList = _businessLogicL.ViewAllVendor();
                return View();

            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }

        }

        [HttpPost]
        public IActionResult paperPurchase(purchaseOrderDTO purchaseDto)
        {
            //store the employeeid in the dto
            string employeeId = HttpContext.Session.GetString("EmployeeID");
            purchaseDto.EmployeeId = employeeId;
            bool result =_businessLogicL.purchaseNewPaper(purchaseDto);
            //return RedirectToAction("ViewAllPaper", "inventory");
            string messageSuccess = "تم سراء الورق الجديد";
            string messageError = "هناك خظأ في شراء الورق الجديد";
            if (result)
            {
                TempData["Success"] = messageSuccess;
                return View("paperPurchase", purchaseDto);
            }
            else {
                TempData["Error"] = messageError;
                return View("paperPurchase", purchaseDto);
            }

        }

        [HttpGet]
        public IActionResult inkPurchase() {
            
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
            {
            ViewBag.inkList = _businessLogicL.getAllActiveInk();
            ViewBag.vendorList = _businessLogicL.ViewAllVendor();

            return View();
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }

        [HttpPost]
        public IActionResult inkPurchase(purchaseOrderDTO purchaseDto)
        {
            //store the employeeid in the dto
            string employeeId = HttpContext.Session.GetString("EmployeeID");
            purchaseDto.EmployeeId = employeeId;
            bool result =_businessLogicL.purchaseNewInk(purchaseDto);
            string messageSuccess = "تم سراء الحبر الجديد";
            string messageError = "هناك خظأ في شراء الحبر الجديد";

            if (result)
            {
                TempData["Success"] = messageSuccess;
                return View("inkPurchase", purchaseDto);
            }
            else
            {
                TempData["Error"] = messageError;
                return View("inkPurchase", purchaseDto);

            }
                //return RedirectToAction("ViewAllink", "inventory");


        }

        [HttpGet]
        public IActionResult supplypurchase()
        {
            
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
            {
                ViewBag.supplyList = _businessLogicL.getAllActiveSupply();
            ViewBag.vendorList = _businessLogicL.ViewAllVendor();

            return View();

            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }

        [HttpPost]
        public IActionResult supplyPurchase(purchaseOrderDTO purchaseDto)
        {
            //store the employeeid in the dto
            string employeeId = HttpContext.Session.GetString("EmployeeID");
            purchaseDto.EmployeeId = employeeId;
            bool result =_businessLogicL.purchaseNewSupply(purchaseDto);
            //return RedirectToAction("viewallsupply", "inventory");
            string messageSuccess = "تم سراء المستلزمات الجديدة";
            string messageError = "هناك خظأ في شراء المستلزمات الجديدة";
            if (result)
            {
                TempData["Success"] = messageSuccess;
                return View("supplyPurchase", purchaseDto);
            }
            else
            {
                TempData["Error"] = messageError;
                return View("supplyPurchase", purchaseDto);
            }


        }

        [HttpGet]
        public IActionResult inventoryReports()
        {
            
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 )
            {
                ViewBag.PaperList = _businessLogicL.GetActivePapers();
            ViewBag.InkList = _businessLogicL.GetActiveInks();
            ViewBag.SupplyList = _businessLogicL.GetActiveSupplies();

            return View();

            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }
        
        [HttpPost]
        public IActionResult inventoryReports(string itemType, int itemId, DateOnly beginingDate, DateOnly endingDate)
        {
            InventoryReportViewModel invViewModel= _businessLogicL.invetoryReports(itemType, itemId, beginingDate, endingDate);
           
            return View(invViewModel);

        }

        [HttpGet]
        public IActionResult purchaseall()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
            {
 ViewBag.PaperList = _businessLogicL.GetActivePapers();
            ViewBag.InkList = _businessLogicL.GetActiveInks();
            ViewBag.SupplyList = _businessLogicL.GetActiveSupplies();
            ViewBag.vendorList = _businessLogicL.ViewAllVendor();

            return View();
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }

           
        }
       
        [HttpPost]
        public IActionResult purchaseall(purchaseOrderDTO dto)
        {


            dto.EmployeeId = HttpContext.Session.GetString("EmployeeID");
            var result = _businessLogicL.PurchaseAll(dto);

            if (result.success)
                TempData["Success"] = result.message;
            else
                TempData["Error"] = result.message;

            return RedirectToAction("purchaseall");
        }




        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Mariam section

        //[HttpGet]
        //public IActionResult VendorReport()
        //{

        //    return View(new InventoryReportViewModel());
        //}

        [HttpPost]
        public IActionResult VendorReport(DateOnly beginingDate, DateOnly endingDate)
        {

            InventoryReportViewModel invViewModel = _businessLogicL.VendorReportRanking(beginingDate, endingDate);

            return View(invViewModel);
        }
        [HttpGet]
        public IActionResult EditVendor(int vendorID) {

            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1)
            {
 Vendor foundVendor = _businessLogicL.GetVendorByID(vendorID);
            return View("~/Views/Inventory/EditVendor.cshtml", foundVendor);
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
           

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
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
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
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
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
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
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
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
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
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
            {try
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
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
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
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
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
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
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
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
            {
 List<Vendor> vendorList = _businessLogicL.ViewAllVendor();
            return View("~/Views/Inventory/ViewAllVendor.cshtml", vendorList);
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
           
        }

        [HttpGet]
        public async Task <IActionResult> ViewAllInk()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
            {
 List<Ink> inkList = await _businessLogicL.ViewAllInk();
            await _hubContext.Clients.All.SendAsync("CheckInkReorderPoint" , "Reorder point reached for Ink: [InkName]");
            return View(inkList);
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }

           
        }

        [HttpGet]
        public async Task<IActionResult> ViewAllPaper()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
            {
 List<Paper> paperList = await _businessLogicL.ViewAllPaper();
            await _hubContext.Clients.All.SendAsync("ReceiveReorderMessage", "Reorder point reached for paper: [PaperName]");
            return View(paperList);
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> ViewAllSupply()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
            {

            List<Supply> suppplyList = await _businessLogicL.ViewAllSupply();
            await _hubContext.Clients.All.SendAsync("CheckSupplyReorderPoint", "Reorder point reached for Supply: [SupplyName]");
            return View(suppplyList);
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }

        [HttpGet]
        public IActionResult PhysicalCount()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
            {

            ViewBag.PaperList = _businessLogicL.GetActivePapers();
            ViewBag.InkList = _businessLogicL.GetActiveInks();
            ViewBag.SupplyList = _businessLogicL.GetActiveSupplies();
            return View();
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
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
