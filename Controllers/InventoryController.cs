using AspNetCoreGeneratedDocument;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Diagnostics;
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
            try
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
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/Newpaper.cshtml", new Paper());
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
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/Newpaper.cshtml", newPaper);
            }

        }

        [HttpGet]
        public IActionResult NewInk()
        {
            try
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
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/NewInk.cshtml", new Ink());
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
                //List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                //ViewBag.ColorWeightSizeList = colorWeightSizes;
                string messageError = "هناك خظأ في اضافة الحبر الجديد";
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = messageError;
                return View(newInk);

            }
        }

        [HttpGet]
        public IActionResult NewSupply()
        {
            try
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
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/NewSupply.cshtml", new Supply());
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
                WriteException.WriteExceptionToFile(ex);
                string messageError = "هناك خظأ في اضافة المستلزم الجديد";
                TempData["Error"] = messageError;
                return View(newSupply);

            }

        }


        [HttpGet]
        public IActionResult inventoryReports()
        {
            try
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
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/InventoryReports.cshtml");
            }
        }

        [HttpPost]
        public IActionResult inventoryReports(string itemType, int itemId, DateOnly beginingDate, DateOnly endingDate)
        {
            try
            {
                ViewBag.VendorList = _businessLogicL.ViewAllVendor();
                ViewBag.EmployeeList = _adminBusinessLogicL.ViewAllEmployee();
                ViewBag.JobOrderList = _technicalBusinessLogicL.ViewAllJobOrder();


                InventoryReportViewModel invViewModel = _businessLogicL.invetoryReports(itemType, itemId, beginingDate, endingDate);

                ExportToExcelItems(invViewModel);
                return View(invViewModel);
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/InventoryReports.cshtml", new InventoryReportViewModel());
            }
        }

        [HttpGet]
        public IActionResult purchaseall()
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1 || jobRole == 2)
                {
                    ViewBag.PaperList = _businessLogicL.GetActivePapers();
                    ViewBag.InkList = _businessLogicL.GetActiveInks();
                    ViewBag.SupplyList = _businessLogicL.GetActiveSupplies();
                    ViewBag.vendorList = _businessLogicL.ViewAllVendor();
                    ViewBag.sparepartList = _businessLogicL.getActiveSpareParts();
                    return View();
                }
                else
                {

                    return RedirectToAction("UnauthorizedAccess", "employee");
                }
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/purchaseAll.cshtml");
            }

        }

        [HttpPost]
        public IActionResult purchaseall(purchaseOrderDTO dto)
        {

            try
            {
                dto.EmployeeId = HttpContext.Session.GetString("EmployeeID");
                var result = _businessLogicL.PurchaseAll(dto);

                if (result.success)
                    TempData["Success"] = result.message;
                else
                    TempData["Error"] = result.message;

                return RedirectToAction("purchaseall");
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/purchaseAll.cshtml", dto);
            }
        }

        [HttpPost]
        public IActionResult deleteColorWeightSize(int ColorWeightSizeId)
        {
            try
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
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return RedirectToAction("ViewAllColorWeightSize");
            }

        }



        [HttpGet]
        public IActionResult editMachineStore(int machineStoreID) {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1)
                {
                    MachineStore machine = _businessLogicL.getMachineByID(machineStoreID);
                    if (machine == null) {

                        TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                        return View(new MachineStore());
                    }
                    else { 
                        return View("~/Views/Inventory/editMachineStore.cshtml");
                    }
                }
                else {
                    return RedirectToAction("UnauthorizedAccess", "employee");
                }

            }
            catch (Exception ex) { 
            
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/editMachineStore.cshtml" , new MachineStore());
            }

          
        }


        [HttpPost]
        public IActionResult editMachineStore(int machineID, MachineStore NewMachine) {

            try
            {
                if (ModelState.IsValid)
                {

                    bool success = _businessLogicL.editMachineStore(machineID, NewMachine);

                    if (success)
                    {
                        TempData["Success"] = "تم تعديل البيانات بنجاح";
                        return View("~/Views/Inventory/editMachineStore.cshtml" , NewMachine);

                    }
                    else
                    {
                        TempData["Error"] = "حدث خطأ اثناء تعديل البيانات";
                        return View("~/Views/Inventory/editMachineStore.cshtml", NewMachine);

                    }

                }
                else {

                    return View("~/Views/Inventory/editMachineStore.cshtml", NewMachine);

                }


            }
            catch (Exception ex) { 
            
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/editMachineStore.cshtml", NewMachine);
   
            }


        }

        [HttpGet]
        public IActionResult editSpareParts(int SparePartsID) {

            try {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1)
                {
                    SparePart part = _businessLogicL.getSparePartByID(SparePartsID);
                    if (part != null)
                    {
                        return View("~/Views/Inventory/editSpareParts.cshtml", part);

                    }
                    else {
                        TempData["Error"] = "حدث خطأ يرجي المحاولة مرة اخري";
                        return View("~/Views/Inventory/editSpareParts.cshtml", new SparePart());
                    }

                }
                else
                {
                    return RedirectToAction("UnauthorizedAccess", "employee");
                }

            } catch (Exception e) {

                WriteException.WriteExceptionToFile(e);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View(new SparePart());
            
            }

        }

        [HttpPost]
        public IActionResult editSpareParts(int sparepartsID, SparePart newSpare)
        {
            try 
            {
                if (ModelState.IsValid)
                {

                    bool success = _businessLogicL.editSpareParts(sparepartsID, newSpare);
                    if (success)
                    {
                        TempData["Success"] = "تم تعديل البيانات بنجاح";
                        return View("~/Views/Inventory/editSpareParts.cshtml", newSpare);

                    }
                    else
                    {
                        TempData["Error"] = "حدث خطأ اثناء تعديل البيانات";
                        return View("~/Views/Inventory/editSpareParts.cshtml", newSpare);

                    }

                }
                else {

                    return View("~/Views/Inventory/editSpareParts.cshtml", newSpare);
                
                }


            }
            catch (Exception ex) 
            {
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/editSpareParts.cshtml", new SparePart());
            }
        
        
        
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Mariam section

        [HttpPost]
        public IActionResult CustomerReport(DateOnly beginingDate, DateOnly endingDate)
        {
            try
            {
                InventoryReportViewModel invViewModel = _businessLogicL.GetCustomerRanking(beginingDate, endingDate);

                return View("~/Views/Inventory/InventoryReports.cshtml", invViewModel);
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/InventoryReports.cshtml", new InventoryReportViewModel());
            }
        }

        [HttpPost]
        public IActionResult VendorReport(DateOnly beginingDate, DateOnly endingDate)
        {
            try
            {
                InventoryReportViewModel invViewModel = _businessLogicL.VendorReportRanking(beginingDate, endingDate);

                return View("~/Views/Inventory/InventoryReports.cshtml", invViewModel);
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/InventoryReports.cshtml", new InventoryReportViewModel());
            }
        }
        [HttpGet]
        public IActionResult EditVendor(int vendorID)
        {
            try
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
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/EditVendor.cshtml", new Vendor());
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
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات المورد";
                return View(newvendor);
            }
        }

        [HttpGet]
        public IActionResult ViewAllColorWeightSize()
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1 || jobRole == 2)
                {

                    List<ColorWeightSize> characteristicsList = _businessLogicL.ViewAllColorWeightSize();
                    return View("~/Views/Inventory/ViewAllColorWeightSize.cshtml", characteristicsList);
                }
                else
                {

                    return RedirectToAction("UnauthorizedAccess", "employee");
                }
            }

            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء عرض الخصائص";
                return View("~/Views/Admin/ViewAllEmployee.cshtml", new List<ColorWeightSize>());

            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //sherwet Section

        [HttpGet]
        public IActionResult AddVendor()
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1 || jobRole == 2)
                {
                  
                    VendorAddDTO empty = new VendorAddDTO();
                    return View("~/Views/Inventory/AddVendor.cshtml", empty);
                  
                }
                else
                {

                    return RedirectToAction("UnauthorizedAccess", "employee");
                }
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/AddVendor.cshtml", new Vendor());
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
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء اضافة بيانات العميل";
                return View("~/Views/Inventory/AddVendor.cshtml", vendor);
            }

        }

        [HttpGet]
        public IActionResult EditInk(int inkId)
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1)
                {

                    List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                    ViewBag.ColorWeightSizeList = colorWeightSizes;

                    Ink ink = _businessLogicL.GetInkByID(inkId);


                    return View("~/Views/Inventory/EditInk.cshtml", ink);
                   
                }
                else
                {
                    return RedirectToAction("UnauthorizedAccess", "employee");
                }

            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/EditInk.cshtml", new Ink());
            }

        }

        [HttpPost]
        public IActionResult EditInk(int InkId, Ink updatedInk)
        {
            try
            {
                List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                ViewBag.ColorWeightSizeList = colorWeightSizes;

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
                WriteException.WriteExceptionToFile(ex);

                List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                ViewBag.ColorWeightSizeList = colorWeightSizes;

                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات الحبر";
                return View("~/Views/Inventory/EditInk.cshtml", updatedInk);
            }
        }


        //edit paper
        
        [HttpGet]
        public IActionResult EditPaper(int paperId)
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1)
                {
                    List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                    ViewBag.ColorWeightSizeList = colorWeightSizes;
                    Paper paper = _businessLogicL.GetPaperByID(paperId);
                    return View("~/Views/Inventory/EditPaper.cshtml", paper);
                }
                else
                {
                    return RedirectToAction("UnauthorizedAccess", "employee");
                }
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/EditPaper.cshtml", new Paper());
            }
        }

        [HttpPost]
        public IActionResult EditPaper(int PaperId, Paper updatedPaper)
        {
            try
            {
                List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                ViewBag.ColorWeightSizeList = colorWeightSizes;

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
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                List<ColorWeightSize> colorWeightSizes = _businessLogicL.getAllColorWeightSize();
                ViewBag.ColorWeightSizeList = colorWeightSizes;
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات الورق";
                return View("~/Views/Inventory/EditPaper.cshtml", updatedPaper);
            }
        }


        //edit paper
        [HttpGet]
        public IActionResult EditSupply(int SuppliesId)
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1)
                {
                    //try
                    //{
                    Supply supply = _businessLogicL.GetSupplyByID(SuppliesId);

                    return View("~/Views/Inventory/EditSupply.cshtml", supply);
                    //}
                    //catch (ApplicationException ex)
                    //{
                    //    return StatusCode(500, ex.Message); // Internal server error
                    //}
                    //catch (ArgumentException ex)
                    //{
                    //    return NotFound(ex.Message);
                    //}

                }
                else
                {

                    return RedirectToAction("UnauthorizedAccess", "employee");
                }
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/EditSupply.cshtml", new Supply());
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

            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
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
                WriteException.WriteExceptionToFile(ex);
                return null;
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
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        [HttpGet]
        public IActionResult ReturnOrder()
        {
            try
            {

                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1 || jobRole == 2)
                {
                    //try
                    //{
                    ViewBag.EmployeeList = _businessLogicL.GetActiveEmployees();
                    ViewBag.JobOrderList = _businessLogicL.GetRecentJobOrdersWithCustomers();
                    ViewBag.PurchaseOrderList = _businessLogicL.GetRecentPurchaseOrderwithSuppliers();
                    ViewBag.PaperList = _businessLogicL.getAllActivePaper();
                    ViewBag.InkList = _businessLogicL.getAllActiveInk();
                    ViewBag.SupplyList = _businessLogicL.getAllActiveSupply();
                    ViewBag.SparePartList = _businessLogicL.getActiveSpareParts();


                    return View(new ReturnOrderDTO());
                    //}
                    //catch (Exception ex)
                    //{
                    //    TempData["ErrorMessage"] = $"حدث خطأ أثناء تحميل الصفحة: {ex.Message}";
                    //    //return RedirectToAction("AdminHome", "Admin");
                    //}
                }


                return RedirectToAction("UnauthorizedAccess", "employee");
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["ErrorMessage"] = $"حدث خطأ أثناء تحميل الصفحة: {ex.Message}";
                return View("~/Views/Inventory/ReturnOrder.cshtml", new ReturnOrderDTO());
            }
        }

        [HttpPost]
        public IActionResult ReturnOrder(ReturnOrderDTO returnDTO)
        {
            try
            {
                string employeeID = HttpContext.Session.GetString("EmployeeID");
                returnDTO.EmployeeId = employeeID;

                //try
                //{
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
                WriteException.WriteExceptionToFile(ex);
                return View(new ReturnOrderDTO());
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
          new SelectListItem { Value = "0", Text = "اللون" },
          new SelectListItem { Value = "1", Text = "الوزن" },
          new SelectListItem { Value = "2", Text = "القياس" }
      };

                    return View(new ColorWeightSize());
                }
                else
                {
                    return RedirectToAction("UnauthorizedAccess", "Employee");
                }
            }
            //catch (ArgumentException ex)
            //{
            //    TempData["Error"] = "حدث خطأ في المعاملات المدخلة.";
            //    return View("~/Views/Inventory/AddCharacteristic.cshtml");
            //}
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ في المعاملات المدخلة.";
                return View("~/Views/Inventory/AddCharacteristic.cshtml", new ColorWeightSize());
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
          new SelectListItem { Value = "0", Text = "اللون" },
          new SelectListItem { Value = "1", Text = "الوزن" },
          new SelectListItem { Value = "2", Text = "القياس" }
      };

                    return View("~/Views/Inventory/AddCharacteristic.cshtml", newChar);
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.TypeOptions = new List<SelectListItem>
      {
          new SelectListItem { Value = "0", Text = "اللون" },
          new SelectListItem { Value = "1", Text = "الوزن" },
          new SelectListItem { Value = "2", Text = "القياس" }
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
            //catch (ArgumentException ex)
            //{

            //    TempData["Error"] = "البيانات غير صحيحة";
            //    return View("~/Views/Inventory/AddCharacteristic.cshtml", newChar);
            //}
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
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1)
                {
                    List<Vendor> vendorList = _businessLogicL.ViewAllVendor();
                    return View("~/Views/Inventory/ViewAllVendor.cshtml", vendorList);
                }
                else if (jobRole == 2)
                {
                    List<Vendor> vendorList = _businessLogicL.ViewAllVendor();
                    return View("~/Views/Inventoryclerk/ViewAllVendor.cshtml", vendorList);

                }
                else
                {

                    return RedirectToAction("UnauthorizedAccess", "employee");
                }
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء عرض الموردين";
                return View("~/Views/Inventory/ViewAllVendor.cshtml", new List<Vendor>());

            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewAllInk()
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1)
                {
                    List<Ink> inkList = await _businessLogicL.ViewAllInk();
                    await _hubContext.Clients.All.SendAsync("CheckInkReorderPoint", "Reorder point reached for Ink: [InkName]");
                    return View(inkList);
                }
                else if (jobRole == 2)
                {
                    List<Ink> inkList = await _businessLogicL.ViewAllInk();
                    await _hubContext.Clients.All.SendAsync("CheckInkReorderPoint", "Reorder point reached for Ink: [InkName]");
                    return View("~/Views/Inventoryclerk/ViewAllInk.cshtml", inkList);

                }
                else
                {

                    return RedirectToAction("UnauthorizedAccess", "employee");
                }
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء عرض الاحبار";
                return View("~/Views/Inventory/ViewAllInk.cshtml", new List<Ink>());

            }

        }

        [HttpGet]
        public async Task<IActionResult> ViewAllPaper()
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1)
                {
                    List<Paper> paperList = await _businessLogicL.ViewAllPaper();
                    await _hubContext.Clients.All.SendAsync("ReceiveReorderMessage", "Reorder point reached for paper: [PaperName]");
                    return View(paperList);
                }
                else if (jobRole == 2)
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
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء عرض الورق";
                return View("~/Views/Inventory/ViewAllpaper.cshtml", new List<Paper>());

            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewAllSupply()
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1)
                {

                    List<Supply> suppplyList = await _businessLogicL.ViewAllSupply();
                    await _hubContext.Clients.All.SendAsync("CheckSupplyReorderPoint", "Reorder point reached for Supply: [SupplyName]");
                    return View(suppplyList);
                }
                else if (jobRole == 2)
                {

                    List<Supply> suppplyList = await _businessLogicL.ViewAllSupply();
                    await _hubContext.Clients.All.SendAsync("ReceiveReorderMessage", "Reorder point reached for Supply: [SupplyName]");
                    return View("~/Views/Inventoryclerk/ViewAllSupply.cshtml", suppplyList);
                }
                else
                {

                    return RedirectToAction("UnauthorizedAccess", "employee");
                }
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء عرض المستلزمات";
                return View("~/Views/Inventory/ViewAllSupply.cshtml", new List<Supply>());

            }
        }

        [HttpGet]
        public IActionResult PhysicalCount()
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1 || jobRole == 2)
                {

                    ViewBag.PaperList = _businessLogicL.GetActivePapers();
                    ViewBag.InkList = _businessLogicL.GetActiveInks();
                    ViewBag.SupplyList = _businessLogicL.GetActiveSupplies();
                    ViewBag.SparePartsList = _businessLogicL.getActiveSpareParts();
                    return View();
                }
                else
                {

                    return RedirectToAction("UnauthorizedAccess", "employee");
                }
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Inventory/PhysicalCount.cshtml", new PhysicalCountDTO());
            }
        }

        public JsonResult GetCurrentQuantity(string itemType, int itemId)
        {
            try
            {
                int quantity = _businessLogicL.GetCurrentQuantity(itemType, itemId);
                return Json(new { currentQuantity = quantity });
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return Json(new { currentQuantity = 0 });
            }
        }
        public JsonResult GetCurrentNumberOfUnits(string itemType, int itemId)
        {
            try
            {
                int numberOfUnits = _businessLogicL.GetCurrentNumberOfUnits(itemType, itemId);
                return Json(new { currentNumberOfUnits = numberOfUnits });
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return Json(new { currentNumberOfUnits = 0 });
            }
        }

        [HttpPost]
        public IActionResult PhysicalCount(PhysicalCountDTO phcountDto)
        {
            try
            {
                string employeeId = HttpContext.Session.GetString("EmployeeID");
                phcountDto.employeeId = employeeId;

                int currentQuantity = _businessLogicL.GetCurrentQuantity(phcountDto.itemType, phcountDto.itemId);
                int currentNumberOfUnits = 0;

                if (phcountDto.itemType == "Ink")
                {
                    currentNumberOfUnits = _businessLogicL.GetCurrentNumberOfUnits(phcountDto.itemType, phcountDto.itemId);
                }
                phcountDto.newQuantity = phcountDto.newQuantity == 0 ? currentQuantity : phcountDto.newQuantity;
                if (phcountDto.itemType == "Ink")
                {
                    phcountDto.newNumberOfUnits = phcountDto.newNumberOfUnits == 0 ? currentNumberOfUnits : phcountDto.newNumberOfUnits;
                }
                else
                {
                    phcountDto.newNumberOfUnits = 0;
                }

                var result = _businessLogicL.UpdateQuantity(phcountDto);

                if (result.Success)
                    TempData["Success"] = result.Message;
                else
                    TempData["Error"] = result.Message;

                return RedirectToAction("PhysicalCount");
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = $"حدث خطأ أثناء معالجة أمر الجرد: {ex.Message}";
                return View(new PhysicalCountDTO());
            }
        }


        [HttpGet]
        public IActionResult EditCharacteristics(int ColorWeightSizeId)
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1)
                {

                    ColorWeightSize CWS = _businessLogicL.GetCharacteristicByID(ColorWeightSizeId);
                    if (CWS == null)
                    {
                        return NotFound();
                    }
                    ViewBag.TypeOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "اللون" },
                new SelectListItem { Value = "1", Text = "الوزن" },
                new SelectListItem { Value = "2", Text = "القياس" }
            };
                    return View("~/Views/Inventory/EditCharacteristics.cshtml", CWS);
                    //    }
                    //    catch (ApplicationException ex)
                    //{
                    //    return StatusCode(500, ex.Message); // Internal server error
                    //}
                    //catch (Exception ex)
                    //{
                    //    return NotFound(ex.Message);
                    //}

                }
                else
                {

                    return RedirectToAction("UnauthorizedAccess", "employee");
                }
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = $"حدث خطأ أثناء تعديل الخصائص: {ex.Message}";
                return View("~/Views/Inventory/EditCharacteristics.cshtml", new ColorWeightSize());
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
                new SelectListItem { Value = "0", Text = "اللون" },
                new SelectListItem { Value = "1", Text = "الوزن" },
                new SelectListItem { Value = "2", Text = "القياس" }
            };
                    return View("~/Views/Inventory/EditCharacteristics.cshtml", updatedChar);
                }

                bool isEditSuccess = _businessLogicL.editCharacteristic(ColorWeightSizeId, updatedChar);

                TempData["Success"] = "تم تعديل بيانات الخصائص";
                return RedirectToAction("EditCharacteristics", "Inventory", new { ColorWeightSizeId });
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
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
                WriteException.WriteExceptionToFile(ex);
                return StatusCode(500, "An error occurred while generating the report.");
            }
        }
        [HttpGet]
        public async Task<IActionResult> ViewAllMachineStore()
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1)
                {

                    List<MachineStore> machineStoreList = _businessLogicL.ViewAllMachineStore();
                    return View(machineStoreList);
                }
                else
                {

                    return RedirectToAction("UnauthorizedAccess", "employee");
                }
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء عرض مخزون الالات";
                return View("~/Views/Inventory/ViewAllMachineStore.cshtml", new List<MachineStore>());

            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewAllSpareParts()
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1)
                {

                    List<SparePart> sparePartsList = _businessLogicL.ViewAllSpareParts();
                    return View(sparePartsList);
                }
                else
                {

                    return RedirectToAction("UnauthorizedAccess", "employee");
                }
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء عرض مخزون قطع غيار الالات";
                return View("~/Views/Inventory/ViewAllSpareParts.cshtml", new List<SparePart>());

            }
        }
    }
}
