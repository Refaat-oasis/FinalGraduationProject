using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.Hubs;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.Utilities;
using ThothSystemVersion1.ViewModels;
namespace ThothSystemVersion1.Controllers
{
    public class InventoryController : Controller
    {

        private readonly InventoryBussinesLogicLayer _businessLogicL;
        private readonly AdminBusinessLogicLayer _adminBusinessLogicL;
        private readonly TechnicalBusinessLogicLayer _technicalBusinessLogicL;


        private readonly IHubContext<ProductHub> _hubContext;
        public InventoryController(InventoryBussinesLogicLayer businessLogicL,
            IHubContext<ProductHub> hubContext,
            AdminBusinessLogicLayer businessLogiclayer
            , TechnicalBusinessLogicLayer technicalBusinessLogicLayer)
        {
            _businessLogicL = businessLogicL;
            _hubContext = hubContext;
            _adminBusinessLogicL = businessLogiclayer;
            _technicalBusinessLogicL = technicalBusinessLogicLayer;
        }

        // refaat section

        [HttpGet]
        public IActionResult NewPaper()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
            {
                List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                ViewBag.ColorWeightSizeList = colorWeightSizes;
                return View(new Paper());
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }

        }

        [HttpPost]
        public IActionResult NewPaper(Paper newPaper)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    bool result = _businessLogicL.addPaper(newPaper);
                    string messageSuccess = "تم اضافة الورق الجديد";
                    string messageError = "هناك خظأ في اضافة الورق الجديد";

                    if (result)
                    {
                        TempData["Success"] = messageSuccess;
                        List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                        ViewBag.ColorWeightSizeList = colorWeightSizes;
                        return View("newpaper", newPaper);
                    }
                    else
                    {
                        TempData["Error"] = messageError;
                        List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                        ViewBag.ColorWeightSizeList = colorWeightSizes;
                        return View("newpaper", newPaper);

                    }


                    //return RedirectToAction("viewallpaper", "inventory");
                }
                else
                {
                    List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                    ViewBag.ColorWeightSizeList = colorWeightSizes;
                    return View("newpaper", newPaper);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpGet]
        public IActionResult NewInk()
        {

            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
            {
                List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                ViewBag.ColorWeightSizeList = colorWeightSizes;
                return View(new Ink());
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }

        }

        [HttpPost]
        public IActionResult NewInk(Ink newInk)
        {
            try
            {
                

                if (ModelState.IsValid)
                {
                    bool result = _businessLogicL.addInk(newInk);
                    string messageSuccess = "تم اضافة الحبر الجديد";
                    string messageError = "هناك خظأ في اضافة الحبر الجديد";

                    if (result)
                    {
                        List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                        ViewBag.ColorWeightSizeList = colorWeightSizes;
                        TempData["Success"] = messageSuccess;
                        return View(newInk);
                    }
                    else
                    {
                        List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                        ViewBag.ColorWeightSizeList = colorWeightSizes;
                        TempData["Error"] = messageError;
                        return View(newInk);

                    }

                }
                else
                {
                    List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                    ViewBag.ColorWeightSizeList = colorWeightSizes;
                    return View(newInk);
                }

            }
            catch (Exception ex)
            {
                List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                ViewBag.ColorWeightSizeList = colorWeightSizes;
                string messageError = "هناك خظأ في اضافة الحبر الجديد";

                TempData["Error"] = messageError;
                return View(newInk);

            }
        }

        [HttpGet]
        public IActionResult NewSupply()
        {
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
        public IActionResult NewSupply(Supply newSupply)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    bool result = _businessLogicL.addSupply(newSupply);

                    string messageSuccess = "تم اضافة المستلزم الجديد";
                    string messageError = "هناك خظأ في اضافة المستلزم الجديد";

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

                }
                else
                {
                   return View(newSupply);
                }

            }
            catch (Exception ex)
            {
                string messageError = "هناك خظأ في اضافة المستلزم الجديد";
                TempData["Error"] = messageError;
                return View(newSupply);
                    
             }

        }


        [HttpGet]
        public IActionResult inventoryReports()
        {

            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1)
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
            ViewBag.VendorList = _businessLogicL.ViewAllVendor();
            ViewBag.EmployeeList = _adminBusinessLogicL.ViewAllEmployee();
            ViewBag.JobOrderList = _technicalBusinessLogicL.ViewAllJobOrder();


            InventoryReportViewModel invViewModel = _businessLogicL.invetoryReports(itemType, itemId, beginingDate, endingDate);

            ExportToExcelItems(invViewModel);
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
            }else{

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

        [HttpPost]
        public IActionResult deleteColorWeightSize(int ColorWeightSizeId)
        {

            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1)
            {
                bool success = _businessLogicL.DeleteColorWeightSize(ColorWeightSizeId);
                if (success)
                {
                    TempData["Success"] = "تم حذف البيانات بنجاح";
                    return RedirectToAction("ViewAllColorWeightSize", "Inventory");
                }
                else
                {
                    TempData["Error"] = "حدث خطأ أثناء حذف البيانات";
                    return RedirectToAction("ViewAllVendorColorWeightSize", "Inventory");
                }

            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee");
            }


        }


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Mariam section

        [HttpPost]
        public IActionResult CustomerReport(DateOnly beginingDate, DateOnly endingDate)
        {

            InventoryReportViewModel invViewModel = _businessLogicL.GetCustomerRanking(beginingDate, endingDate);

            return View("~/Views/Inventory/InventoryReports.cshtml", invViewModel);
        }

        [HttpPost]
        public IActionResult VendorReport(DateOnly beginingDate, DateOnly endingDate)
        {

            InventoryReportViewModel invViewModel = _businessLogicL.VendorReportRanking(beginingDate, endingDate);

            return View("~/Views/Inventory/InventoryReports.cshtml", invViewModel);
        }
        [HttpGet]
        public IActionResult EditVendor(int vendorID)
        {

            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1)
            {
                var foundVendor = _businessLogicL.GetVendorByID(vendorID);
                return View("~/Views/Inventory/EditVendor.cshtml", foundVendor);
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }


        }

        [HttpPost]
        public IActionResult EditVendor(int vendorID, VendorEditDTO newvendor)
        {
            try
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
                    TempData["Error"] = ("", "البريد الالكتروني او رقم الهاتف تم استخدامه من قبل");
                    // Also pass newvendor back here
                    return View("~/Views/Inventory/EditVendor.cshtml", newvendor);
                }
                TempData["Success"] = "تم تعديل بيانات المورد";
                return View("EditVendor", newvendor);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات المورد";
                return View(newvendor);
            }
        }
        [HttpGet]
        public IActionResult ViewAllColorWeightSize()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
            {

                List<ColorWeightSize> characteristicsList = _businessLogicL.ViewAllColorWeightSize();
                return View("~/Views/Admin/ViewAllEmployee.cshtml", characteristicsList);
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }

        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //sherwet Section

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
                TempData["Success"] = "تم اضافة بيانات العميل";
                return RedirectToAction("AddVendor", "inventory");
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = "حدث خطأ أثناء اضافة بيانات العميل";
                return View("~/Views/Inventory/AddVendor.cshtml", vendor);
            }

        }
        
        [HttpGet]
        public IActionResult EditInk(int inkId)
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1)
            {
                try
                {
                    List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                    ViewBag.ColorWeightSizeList = colorWeightSizes;

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
        public IActionResult EditInk(int InkId, Ink updatedInk)
        {
            try
            {
                if (updatedInk == null)
                {
                    ModelState.AddModelError("", "البيانات غير صالحة");
                    return View("~/Views/Inventory/EditInk.cshtml", updatedInk);
                }


                if (!ModelState.IsValid)
                {
                    return View("~/Views/Inventory/EditInk.cshtml", updatedInk);
                }

                bool isEditSuccess = _businessLogicL.EditInk(InkId, updatedInk);

                TempData["Success"] = "تم تعديل بيانات الحبر";
                return RedirectToAction("EditInk", "Inventory", new { InkId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات الحبر";
                return View("~/Views/Inventory/EditInk.cshtml", updatedInk);
            }
        }


        //edit paper
        [HttpGet]
        public IActionResult EditPaper(int paperId)
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1)
            {
                try
                {
                    List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                    ViewBag.ColorWeightSizeList = colorWeightSizes;
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
        public IActionResult EditPaper(int PaperId, Paper updatedPaper)
        {
            try
            {
                if (updatedPaper == null)
                {
                    ModelState.AddModelError("", "البيانات غير صالحة");
                    return View("~/Views/Inventory/EditPaper.cshtml", updatedPaper);
                }
                if (!ModelState.IsValid)
                {
                    return View("~/Views/Inventory/EditPaper.cshtml", updatedPaper);
                }

                bool isEditSuccess = _businessLogicL.editPaper(PaperId, updatedPaper);

                TempData["Success"] = "تم تعديل بيانات الورق";
                return RedirectToAction("EditPaper", "Inventory", new { PaperId });
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات الورق";
                return View("~/Views/Inventory/EditPaper.cshtml", updatedPaper);
            }
        }

        //edit paper
        [HttpGet]
        public IActionResult EditSupply(int SuppliesId)
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1)
            {
                try
                {
                    Supply supply = _businessLogicL.GetSupplyByID(SuppliesId);

                    return View("~/Views/Inventory/EditSupply.cshtml", supply);
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
        public IActionResult EditSupply(int SuppliesId, Supply updatedSupply)
        {
            try
            {
                if (updatedSupply == null)
                {
                    ModelState.AddModelError("", "البيانات غير صالحة");
                    return View("~/Views/Inventory/EditSupply.cshtml", updatedSupply);
                }
                if (!ModelState.IsValid)
                {
                    return View("~/Views/Inventory/EditSupply.cshtml", updatedSupply);
                }
                bool isEditSuccess = _businessLogicL.editSupply(SuppliesId, updatedSupply);
                TempData["Success"] = "تم تعديل بيانات المستلزمات";
                return RedirectToAction("EditSupply", "Inventory", new { SuppliesId });
            }

            catch (ArgumentException ex)
            {
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات المستلزمات";
                return View("~/Views/Inventory/EditSupply.cshtml", updatedSupply);
            }

        }
        //return
        
        [HttpGet]
        public IActionResult GetJobOrderItems(int jobOrderId)
        {
            try
            {

                var items = _businessLogicL.GetJobOrderItems(jobOrderId);
                return Json(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetPurchaseOrderItems(int purchaseId)
        {
            try
            {
                var items = _businessLogicL.GetPurchaseOrderItems(purchaseId);
                return Json(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult ReturnOrder()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1 || jobRole == 2)
            {
                try
                {
                    ViewBag.EmployeeList = _businessLogicL.GetActiveEmployees();
                    ViewBag.JobOrderList = _businessLogicL.GetRecentJobOrdersWithCustomers();
                    ViewBag.PurchaseOrderList = _businessLogicL.GetRecentPurchaseOrderwithSuppliers();
                    ViewBag.PaperList = _businessLogicL.getAllActivePaper();
                    ViewBag.InkList = _businessLogicL.getAllActiveInk();
                    ViewBag.SupplyList = _businessLogicL.getAllActiveSupply();

                    return View(new ReturnOrderDTO());
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"حدث خطأ أثناء تحميل الصفحة: {ex.Message}";
                    //return RedirectToAction("AdminHome", "Admin");
                }
            }


            return RedirectToAction("UnauthorizedAccess", "employee");
        }

        [HttpPost]
        public IActionResult ReturnOrder(ReturnOrderDTO returnDTO)
        {
            string employeeID = HttpContext.Session.GetString("EmployeeID");
            returnDTO.EmployeeId = employeeID;

            try
            {
                if (returnDTO.BridgeList == null || !returnDTO.BridgeList.Any())
                {
                    TempData["Error"] = "يجب إضافة صنف واحد على الأقل للإرجاع";
                    return View();
                }


                var result = _businessLogicL.ReturnOrder(returnDTO);

                if (result.success)
                {
                    TempData["Success"] = result.message;
                    return View();
                }
                else
                {
                    TempData["Error"] = result.message;
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"حدث خطأ أثناء معالجة أمر الإرجاع: {ex.Message}";
                return View();
            }
        }

        [HttpGet]
        public IActionResult AddCharacteristic()
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");

                if (jobRole == 0 || jobRole == 1 || jobRole == 2)
                {
                    ViewBag.TypeOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "حجم" },
                new SelectListItem { Value = "2", Text = "وزن" },
                new SelectListItem { Value = "3", Text = "لون" }
            };

                    return View(new ColorWeightSize());
                }
                else
                {
                    return RedirectToAction("UnauthorizedAccess", "Employee");
                }
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = "حدث خطأ في المعاملات المدخلة.";
                return View("~/Views/Inventory/AddCharacteristic.cshtml");
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return View("~/Views/Inventory/AddCharacteristic.cshtml");
            }
        }



        [HttpPost]
        public IActionResult AddCharacteristic(ColorWeightSize newChar)
        {
            try
            {
                if (newChar == null)
                {
                    ModelState.AddModelError("", "البيانات غير صالحة");
                    ViewBag.TypeOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "حجم" },
                new SelectListItem { Value = "2", Text = "وزن" },
                new SelectListItem { Value = "3", Text = "لون" }
            };

                    return View("~/Views/Inventory/AddCharacteristic.cshtml", newChar);
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.TypeOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "حجم" },
                new SelectListItem { Value = "2", Text = "وزن" },
                new SelectListItem { Value = "3", Text = "لون" }
            };

                    return View("~/Views/Inventory/AddCharacteristic.cshtml", newChar);
                }

                bool isAddSuccess = _businessLogicL.addCharacteristic(newChar);

                if (isAddSuccess)
                {
                    TempData["Success"] = "تم إضافة بيانات الخاصية بنجاح";
                    return RedirectToAction("AddCharacteristic", "Inventory");
                }
                else
                {
                    TempData["Error"] = "حدث خطأ أثناء إضافة الخاصية";
                    return View("~/Views/Inventory/AddCharacteristic.cshtml", newChar);
                }
            }
            catch (ArgumentException ex)
            {

                TempData["Error"] = "البيانات غير صحيحة";
                return View("~/Views/Inventory/AddCharacteristic.cshtml", newChar);
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/AddCharacteristic.cshtml", newChar);
            }

        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Sandra section
        [HttpGet]
        public IActionResult ViewAllVendor()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1)
            {
                List<Vendor> vendorList = _businessLogicL.ViewAllVendor();
                return View("~/Views/Inventory/ViewAllVendor.cshtml", vendorList);
            }
            else if (jobRole == 2) {
                List<Vendor> vendorList = _businessLogicL.ViewAllVendor();
                return View("~/Views/Inventoryclerk/ViewAllVendor.cshtml" , vendorList);
            
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }

        }

        [HttpGet]
        public async Task<IActionResult> ViewAllInk()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1)
            {
                List<Ink> inkList = await _businessLogicL.ViewAllInk();
                await _hubContext.Clients.All.SendAsync("CheckInkReorderPoint", "Reorder point reached for Ink: [InkName]");
                return View(inkList);
            }
            else if (jobRole == 2) {
                List<Ink> inkList = await _businessLogicL.ViewAllInk();
                await _hubContext.Clients.All.SendAsync("CheckInkReorderPoint", "Reorder point reached for Ink: [InkName]");
                return View("~/Views/Inventoryclerk/ViewAllInk.cshtml", inkList);

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
            if (jobRole == 0 || jobRole == 1)
            {
                List<Paper> paperList = await _businessLogicL.ViewAllPaper();
                await _hubContext.Clients.All.SendAsync("ReceiveReorderMessage", "Reorder point reached for paper: [PaperName]");
                return View(paperList);
            }else if ( jobRole == 2)
            {

                List<Paper> paperList = await _businessLogicL.ViewAllPaper();
                await _hubContext.Clients.All.SendAsync("ReceiveReorderMessage", "Reorder point reached for paper: [PaperName]");
                return View("~/Views/Inventoryclerk/ViewAllpaper.cshtml", paperList);

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
            if (jobRole == 0 || jobRole == 1)
            {

                List<Supply> suppplyList = await _businessLogicL.ViewAllSupply();
                await _hubContext.Clients.All.SendAsync("CheckSupplyReorderPoint", "Reorder point reached for Supply: [SupplyName]");
                return View(suppplyList);
            } else if (jobRole == 2) {

                List<Paper> paperList = await _businessLogicL.ViewAllPaper();
                await _hubContext.Clients.All.SendAsync("ReceiveReorderMessage", "Reorder point reached for paper: [PaperName]");
                return View("~/Views/Inventoryclerk/ViewAllpaper.cshtml", paperList);
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

        [HttpGet]
        public IActionResult EditCharacteristics(int CWSId)
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 1)
            {
                try
                {
                    ColorWeightSize CWS = _businessLogicL.GetCharacteristicByID(CWSId);
                    ViewBag.TypeOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "حجم" },
                new SelectListItem { Value = "2", Text = "وزن" },
                new SelectListItem { Value = "3", Text = "لون" }
            };
                    return View("~/Views/Inventory/EditCharacteristics.cshtml", CWS);
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
        public IActionResult EditCharacteristics(int ColorWeightSizeId, ColorWeightSize updatedChar)
        {
            try
            {
                if (updatedChar == null)
                {
                    ModelState.AddModelError("", "البيانات غير صالحة");
                    return View("~/Views/Inventory/EditCharacteristics.cshtml", updatedChar);
                }
                if (!ModelState.IsValid)
                {
                    ViewBag.TypeOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "حجم" },
                new SelectListItem { Value = "2", Text = "وزن" },
                new SelectListItem { Value = "3", Text = "لون" }
            };
                    return View("~/Views/Inventory/EditCharacteristics.cshtml", updatedChar);
                }

                bool isEditSuccess = _businessLogicL.editCharacteristic(ColorWeightSizeId, updatedChar);

                TempData["Success"] = "تم تعديل بيانات الخصائص";
                return RedirectToAction("EditCharacteristics", "Inventory", new { ColorWeightSizeId });
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات الخصائص";
                return View("~/Views/Inventory/EditCharacteristics.cshtml", updatedChar);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        //Views_Inventory_InventoryPrinting section 

        //public IActionResult ExportToExcelItems(InventoryReportViewModel viewModel)
        //{
        //    try {

        //        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //        using (var package = new ExcelPackage())
        //        {
        //            var worksheet = package.Workbook.Worksheets.Add($"تقارير العناصر");

        //            // Add Headers
        //            worksheet.Cells[1, 1].Value = "اسم المورد";
        //            worksheet.Cells[1, 2].Value = "اسم الموظف";
        //            worksheet.Cells[1, 3].Value = "المبلغ المتبقي";
        //            worksheet.Cells[1, 4].Value = "المبلغ المدفوع";
        //            worksheet.Cells[1, 5].Value = "الحساب";
        //            worksheet.Cells[1, 6].Value = "السعر";
        //            worksheet.Cells[1, 7].Value = "تاريخ الشراء";
        //            worksheet.Cells[1, 8].Value = "تفاصيل الشراء";


        //            // Add Data
        //            for (int i = 0; i < viewModel.modifiedPurchaseOrderList.Count; i++)
        //            {
        //                worksheet.Cells[i + 2, 1].Value = viewModel.modifiedPurchaseOrderList[i].Vendorname;
        //                worksheet.Cells[i + 2, 2].Value = viewModel.modifiedPurchaseOrderList[i].EmployeeName;
        //                worksheet.Cells[i + 2, 3].Value = viewModel.modifiedPurchaseOrderList[i].RemainingAmount;
        //                worksheet.Cells[i + 2, 4].Value = viewModel.modifiedPurchaseOrderList[i].PaidAmount;
        //                worksheet.Cells[i + 2, 5].Value = viewModel.modifiedPurchaseOrderList[i].balance;
        //                worksheet.Cells[i + 2, 6].Value = viewModel.modifiedPurchaseOrderList[i].price;
        //                worksheet.Cells[i + 2, 7].Value = viewModel.modifiedPurchaseOrderList[i].PurchaseDate;
        //                worksheet.Cells[i + 2, 8].Value = viewModel.modifiedPurchaseOrderList[i].PurchaseNotes;

        //            }

        //            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

        //            var stream = new MemoryStream(package.GetAsByteArray());
        //            return File(stream,
        //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                "Export.xlsx");
        //        }

        //    } catch (Exception ex)
        //    {
        //        WriteException.WriteExceptionToFile(ex);
        //        return null;
        //    }
        //}
        public IActionResult ExportToExcelItems(InventoryReportViewModel viewModel)
        {
            try
            {
                // 1) Configure EPPlus license BEFORE creating any ExcelPackage
                // Noncommercial personal use
                ExcelPackage.License.SetNonCommercialPersonal("Your Name");
                // —or— for organizational noncommercial use:
                // ExcelPackage.License.SetNonCommercialOrganization("Your Organization"); :contentReference[oaicite:0]{index=0}

                // 2) Build the Excel package
                using var package = new ExcelPackage();
                var ws = package.Workbook.Worksheets.Add("تقارير العناصر");

                // Add headers
                ws.Cells[1, 1].Value = "اسم المورد";
                ws.Cells[1, 2].Value = "اسم الموظف";
                ws.Cells[1, 3].Value = "المبلغ المتبقي";
                ws.Cells[1, 4].Value = "المبلغ المدفوع";
                ws.Cells[1, 5].Value = "الحساب";
                ws.Cells[1, 6].Value = "السعر";
                ws.Cells[1, 7].Value = "تاريخ الشراء";
                ws.Cells[1, 8].Value = "تفاصيل الشراء";

                // Populate rows
                for (int i = 0; i < viewModel.modifiedPurchaseOrderList.Count; i++)
                {
                    var item = viewModel.modifiedPurchaseOrderList[i];
                    ws.Cells[i + 1, 1].Value = item.Vendorname;
                    ws.Cells[i + 1, 2].Value = item.EmployeeName;
                    ws.Cells[i + 1, 3].Value = item.RemainingAmount;
                    ws.Cells[i + 1, 4].Value = item.PaidAmount;
                    ws.Cells[i + 1, 5].Value = item.balance;
                    ws.Cells[i + 1, 6].Value = item.price;
                    ws.Cells[i + 1, 7].Value = item.PurchaseDate;
                    ws.Cells[i + 1, 8].Value = item.PurchaseNotes;
                }

                ws.Cells[ws.Dimension.Address].AutoFitColumns();


                // 3) Prepare file contents and name
                byte[] fileContents = package.GetAsByteArray();
                string fileName = $"تقرير الصنف_{DateTime.Now:yyyy-MM-dd}.xlsx";


                // Get the base application directory
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                // Create Exceptions directory if it doesn't exist
                string exceptionsRoot = Path.Combine(basePath, "exports");
                Directory.CreateDirectory(exceptionsRoot);


                // 4) Save a server-side copy under wwwroot/exports
                string exportsFolder = Path.Combine(basePath, "exports");
                if (!Directory.Exists(exportsFolder))
                    Directory.CreateDirectory(exportsFolder);

                string fullPath = Path.Combine(exportsFolder, fileName);
                System.IO.File.WriteAllBytes(fullPath, fileContents);
                // `fullPath` is now the physical path on disk

                // 5) Return the Excel file to the browser
                return File(
                    fileContents,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName
                );
            }
            catch (Exception ex)
            {
                // Log your exception as needed...
                return StatusCode(500, "An error occurred while generating the report.");
            }
        }
    }
}
