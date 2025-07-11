﻿using AspNetCoreGeneratedDocument;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Diagnostics;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.Hubs;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.ModifiedModels;
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
                    ViewBag.SpareList = _businessLogicL.getActiveSpareParts();

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
                        return View("~/Views/Inventory/editMachineStore.cshtml", machine);
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
                ExportToExcelItemsCustomer(invViewModel);
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
                ExportToExcelItemsVendor(invViewModel);
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
                if (jobRole == 0 || jobRole == 1)
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
                return View("~/Views/Inventory/ViewAllColorWeightSize.cshtml", new List<ColorWeightSize>());

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
                    TempData["Error"]=("", "الايميل او رقم الهاتف تم استخدامه من قبل");
                    return View(vendor);
                }
                TempData["Success"] = "تم اضافة بيانات المورد";
                return RedirectToAction("AddVendor", "inventory");
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء اضافة بيانات المورد";
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

        [HttpGet]
        public IActionResult AddMachine()
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1 || jobRole ==2)
                {

                    MachineStore empty = new MachineStore();
                    return View("~/Views/Inventory/AddMachine.cshtml", empty);

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
                return View("~/Views/Inventory/AddMachine.cshtml", new MachineStore());
            }
        }

        [HttpPost]
        public IActionResult AddMachine(MachineStore machine)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("~/Views/Inventory/AddMachine.cshtml", machine);
                }

                bool isMachineadded = _businessLogicL.AddMachine(machine);
                TempData["Success"] = "تم اضافة بيانات قطعة الاله";
                return RedirectToAction("AddMachine", "inventory");
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء اضافة بيانات الاله";
                return View("~/Views/Inventory/AddMachine.cshtml", machine);
            }

        }
       
        [HttpGet]
        public IActionResult AddSparePart()
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1 || jobRole == 2)
                {

                    SparePart empty = new SparePart();
                    return View("~/Views/Inventory/AddSparePart.cshtml", empty);

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
                return View("~/Views/Inventory/AddSparePart.cshtml", new SparePart());
            }
        }

        [HttpPost]
        public IActionResult AddSparePart(SparePart sparepart)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("~/Views/Inventory/AddSparePart.cshtml", sparepart);
                }

                bool isMachineadded = _businessLogicL.AddSparePart(sparepart);
                TempData["Success"] = "تم اضافة بيانات قطعة الغيار";
                return RedirectToAction("AddSparePart", "inventory");
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء اضافة بيانات قطعة الغيار";
                return View("~/Views/Inventory/AddSparePart.cshtml", sparepart);
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

                else if (jobRole == 2)
                {
                    List<MachineStore> machineStoreList = _businessLogicL.ViewAllMachineStore();
                    return View("~/Views/InventoryClerk/ViewAllMachineStore.cshtml", machineStoreList);
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
                if (jobRole == 0 || jobRole == 1 )
                {

                    List<SparePart> sparePartsList = _businessLogicL.ViewAllSpareParts();
                    return View(sparePartsList);
                }else if (jobRole == 2)
                {

                    List<SparePart> sparePartsList = _businessLogicL.ViewAllSpareParts();
                    return View("~/Views/InventoryClerk/ViewAllSpareParts.cshtml",sparePartsList);
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

        [HttpGet]
        public IActionResult PerpetualRequisite()
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1 || jobRole == 2)
                {
                    ViewBag.MachineStoreList = _businessLogicL.GetActiveMachines();
                    ViewBag.InkList = _businessLogicL.GetActiveInks();
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
                return View("~/Views/Inventory/PerpetualRequisite.cshtml", new PerpetualRequisiteDTO());
            }
        }

        public JsonResult GetCurrentQuantityPR(string itemType, int itemId)
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
        public JsonResult GetCurrentNumberOfUnitsPR(string itemType, int itemId)
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
        public IActionResult PerpetualRequisite(PerpetualRequisiteDTO perpetual)
        {
            try
            {
                string employeeId = HttpContext.Session.GetString("EmployeeID");
                perpetual.EmployeeId = employeeId;
               
                var result = _businessLogicL.PerpetualRequisite(perpetual);
                if (result.success)
                {
                    TempData["Success"] = result.message;
                    return RedirectToAction("PerpetualRequisite");
                }
                TempData["Error"] = result.message;
                return RedirectToAction("PerpetualRequisite");
            }
            
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = $"حدث خطأ أثناء معالجة أمر الجرد: {ex.Message}";
                return View(new PerpetualRequisiteDTO());
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //Views_Inventory_InventoryPrinting section 


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

                int numberOfRow = 3;


                ws.Cells[1, 1].Value = "اوامر الشراء";
                ws.Cells[1, 1, 1, 8].Merge = true;
                ws.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 1].Style.Font.Bold = true;
                ws.Cells[1, 1].Style.Font.Size = 14;
                ws.Cells[2, 1].Value = "اسم المورد";
                ws.Cells[2, 2].Value = "اسم الموظف";
                ws.Cells[2, 3].Value = "المبلغ المتبقي";
                ws.Cells[2, 4].Value = "المبلغ المدفوع";
                ws.Cells[2, 5].Value = "تاريخ الشراء";
                ws.Cells[2, 6].Value = "تفاصيل الشراء";
                ws.Cells[2, 7].Value = "الكمية";
                ws.Cells[2, 8].Value = "السعر";

                for (int i = 0; i < viewModel.modifiedPurchaseOrderList.Count; i++)
                {
                    ModifiedPurchaseOrder item = viewModel.modifiedPurchaseOrderList[i];
                    ws.Cells[i + numberOfRow, 1].Value = item.Vendorname;
                    ws.Cells[i + numberOfRow, 2].Value = item.EmployeeName;
                    ws.Cells[i + numberOfRow, 3].Value = item.RemainingAmount;
                    ws.Cells[i + numberOfRow, 4].Value = item.PaidAmount;
                    ws.Cells[i + numberOfRow, 5].Value = item.PurchaseDate.Value.ToDateTime(TimeOnly.MinValue);
                    ws.Cells[i + numberOfRow, 5].Style.Numberformat.Format = "yyyy-mm-dd";
                    ws.Cells[i + numberOfRow, 6].Value = item.PurchaseNotes;
                    ws.Cells[i + numberOfRow, 7].Value = item.balance;
                    ws.Cells[i + numberOfRow, 8].Value = item.price;

                    numberOfRow++;
                }
                numberOfRow = 3;
                // requisite
                ws.Cells[1, 10].Value = "اوامر الصرف";
                ws.Cells[1, 10, 1, 15].Merge = true;
                ws.Cells[1, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[1, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 10].Style.Font.Bold = true;
                ws.Cells[1, 10].Style.Font.Size = 14;
                ws.Cells[2, 10].Value = "الكمية";
                ws.Cells[2, 11].Value = "السعر";
                ws.Cells[2, 12].Value = "اسم الموظف";
                ws.Cells[2, 13].Value = "رقم الامر";
                ws.Cells[2, 14].Value = "تاريخ الصرف";
                ws.Cells[2, 15].Value = "تفاصيل الصرف";

                for (int i = 0; i < viewModel.modifiedRequisiteOrderList.Count; i++)
                {
                    ModifiedRequisiteOrder item = viewModel.modifiedRequisiteOrderList[i];
                    ws.Cells[i + numberOfRow, 10].Value = item.balance;
                    ws.Cells[i + numberOfRow, 11].Value = item.price;
                    ws.Cells[i + numberOfRow, 12].Value = item.EmployeeName;
                    ws.Cells[i + numberOfRow, 13].Value = item.RequisiteId;
                    ws.Cells[i + numberOfRow, 14].Value = item.RequisiteDate.Value.ToDateTime(TimeOnly.MinValue);
                    ws.Cells[i + numberOfRow, 14].Style.Numberformat.Format = "yyyy-mm-dd";
                    ws.Cells[i + numberOfRow, 15].Value = item.RequisiteNotes;

                    numberOfRow++;
                }
                numberOfRow = 3;

                //physicalCount

                ws.Cells[1, 17].Value = "اوامر الجرد";
                ws.Cells[1, 17, 1, 22].Merge = true;
                ws.Cells[1, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[1, 17].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 17].Style.Font.Bold = true;
                ws.Cells[1, 17].Style.Font.Size = 14;
                ws.Cells[2, 17].Value = "الكمية القديمة";
                ws.Cells[2, 18].Value = "الكمية الحالية";
                ws.Cells[2, 19].Value = "اسم الموظف";
                ws.Cells[2, 20].Value = "رقم الامر";
                ws.Cells[2, 21].Value = "تاريخ الجرد";
                ws.Cells[2, 22].Value = "تفاصيل الجرد";

                for (int i = 0; i < viewModel.modifiedPhysicalCountOrderList.Count; i++)
                {
                    ModifiedPhysicalCountOrder item = viewModel.modifiedPhysicalCountOrderList[i];
                    ws.Cells[i + numberOfRow, 17].Value = item.oldBalance;
                    ws.Cells[i + numberOfRow, 18].Value = item.balance;
                    ws.Cells[i + numberOfRow, 19].Value = item.EmployeeName;
                    ws.Cells[i + numberOfRow, 20].Value = item.PhysicalCountId;
                    ws.Cells[i + numberOfRow, 21].Value = item.PhysicalCountDate.Value.ToDateTime(TimeOnly.MinValue);
                    ws.Cells[i + numberOfRow, 21].Style.Numberformat.Format = "yyyy-mm-dd";
                    ws.Cells[i + numberOfRow, 22].Value = item.PhysicalCountNotes;

                    numberOfRow++;
                }
                numberOfRow = 3;

                //returns

                ws.Cells[1, 24].Value = "المرتجعات";
                ws.Cells[1, 24, 1, 29].Merge = true;
                ws.Cells[1, 24].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[1, 24].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 24].Style.Font.Bold = true;
                ws.Cells[1, 24].Style.Font.Size = 14;
                ws.Cells[2, 24].Value = "الكمية المرتجعة";
                ws.Cells[2, 25].Value = "اسم الموظف";
                ws.Cells[2, 26].Value = "رقم الامر";
                ws.Cells[2, 27].Value = "تاريخ المرتجع";
                ws.Cells[2, 28].Value = "تفاصيل المرتجع";
                ws.Cells[2, 29].Value = "امر داخلي ام خارجي";

                for (int i = 0; i < viewModel.modifiedReturnsOrderList.Count; i++)
                {
                    ModifiedReturnsOrder item = viewModel.modifiedReturnsOrderList[i];
                    ws.Cells[i + numberOfRow, 24].Value = item.balance;
                    ws.Cells[i + numberOfRow, 25].Value = item.EmployeeName;
                    ws.Cells[i + numberOfRow, 26].Value = item.ReturnId;
                    ws.Cells[i + numberOfRow, 27].Value = item.ReturnDate.Value.ToDateTime(TimeOnly.MinValue);
                    ws.Cells[i + numberOfRow, 27].Style.Numberformat.Format = "yyyy-mm-dd";
                    ws.Cells[i + numberOfRow, 28].Value = item.ReturnsNotes;
                    ws.Cells[i + numberOfRow, 29].Value = item.ReturnInOut ? "داخلي" : "خارجي";

                    numberOfRow++;
                }
                numberOfRow = 3;

                //perpetualrequisite

                ws.Cells[1, 33].Value = "اوامر صرف المخزن الدائم";
                ws.Cells[1, 33, 1, 38].Merge = true;
                ws.Cells[1, 33].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[1, 33].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 33].Style.Font.Bold = true;
                ws.Cells[1, 33].Style.Font.Size = 14;
                ws.Cells[2, 33].Value = "الكمية";
                ws.Cells[2, 34].Value = "السعر";
                ws.Cells[2, 35].Value = "اسم الموظف";
                ws.Cells[2, 36].Value = "رقم الامر";
                ws.Cells[2, 37].Value = "تاريخ الصرف";
                ws.Cells[2, 38].Value = "تفاصيل الصرف";

                for (int i = 0; i < viewModel.modifiedPerpetualRequisiteOrdersList.Count; i++)
                {
                    ModifiedPerpetualRequisiteOrder item = viewModel.modifiedPerpetualRequisiteOrdersList[i];
                    ws.Cells[i + numberOfRow, 33].Value = item.balance;
                    ws.Cells[i + numberOfRow, 34].Value = item.price;
                    ws.Cells[i + numberOfRow, 35].Value = item.EmployeeName;
                    ws.Cells[i + numberOfRow, 36].Value = item.PerpetualRequisiteId;
                    ws.Cells[i + numberOfRow, 37].Value = item.PerpetualRequisiteDate.Value.ToDateTime(TimeOnly.MinValue);
                    ws.Cells[i + numberOfRow, 37].Style.Numberformat.Format = "yyyy-mm-dd";
                    ws.Cells[i + numberOfRow, 38].Value = item.RequisiteNotes;

                    numberOfRow++;
                }

                //itemData
                ws.Cells[1, 40].Value = "بيانات الصنف";
                ws.Cells[1, 40, 1, 44].Merge = true;
                ws.Cells[1, 40].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[1, 40].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 40].Style.Font.Bold = true;
                ws.Cells[1, 40].Style.Font.Size = 14;
                ws.Cells[2, 40].Value = "الكمية";
                ws.Cells[2, 41].Value = "السعر";
                ws.Cells[2, 42].Value = "الكمية المتاحة";
                ws.Cells[2, 43].Value = "اسم الصنف";
                ws.Cells[2, 44].Value = "نوع الصنف";
                ws.Cells[3, 40].Value = viewModel.itemQuantity;
                ws.Cells[3, 41].Value = viewModel.itemPrice;
                ws.Cells[3, 42].Value = viewModel.itemTotalBalance;
                ws.Cells[3, 43].Value = viewModel.itemName;
                ws.Cells[3, 44].Value = viewModel.itemType;

                ws.Cells[ws.Dimension.Address].AutoFitColumns();


                // 3) Prepare file contents and name
                byte[] fileContents = package.GetAsByteArray();
                //string fileName = $"تقرير الصنف_{DateTime.Now:yyyy-MM-dd}.xlsx";

                // Arabic prefix
                string arabicTitle = $"تقرير {viewModel.itemType} {viewModel.itemName}";

                // Get current date in yyyy-MM-dd format
                string datePart = DateTime.Now.ToString("yyyy-MM-dd");

                // Get current time in HH-mm format (24-hour time)
                string timePart = DateTime.Now.ToString("HH-mm");

                // File extension
                string extension = ".xlsx";

                // Combine all parts into the final filename
                string fileName = $"{arabicTitle}_التاريخ_{datePart}_الوقت_{timePart}{extension}";


                //// Get the base application directory
                //string basePath = AppDomain.CurrentDomain.BaseDirectory;


                // 4) Save a server-side copy under wwwroot/exports
                string exportsFolder = Path.Combine("exports");
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

        public IActionResult ExportToExcelItemsVendor(InventoryReportViewModel viewModel)
        {
            try
            {

                ExcelPackage.License.SetNonCommercialPersonal("Your Name");

                using var package = new ExcelPackage();
                var ws = package.Workbook.Worksheets.Add("تقارير الموردين");

                // Add headers
                ws.Cells[1, 1].Value = "اسم المورد";
                ws.Cells[1, 2].Value = "عدد المشتريات";
                ws.Cells[1, 3].Value = "صافي المشتريات";


                // Populate rows
                for (int i = 0; i < viewModel.VendorReport.Count; i++)
                {
                    var item = viewModel.VendorReport[i];
                    ws.Cells[i + 2, 1].Value = item.Vendor.VendorName;
                    ws.Cells[i + 2, 2].Value = item.PurchaseCount;
                    ws.Cells[i + 2, 3].Value = item.TotalOldBalance;

                }

                ws.Cells[ws.Dimension.Address].AutoFitColumns();


                // 3) Prepare file contents and name
                byte[] fileContents = package.GetAsByteArray();
                string fileName = $"تقرير الموردين{DateTime.Now:yyyy-MM-dd}.xlsx";


                //// Get the base application directory
                //string basePath = AppDomain.CurrentDomain.BaseDirectory;


                // 4) Save a server-side copy under wwwroot/exports
                string exportsFolder = Path.Combine("exports");
                if (!Directory.Exists(exportsFolder))
                    Directory.CreateDirectory(exportsFolder);

                string fullPath = Path.Combine(exportsFolder, fileName);
                System.IO.File.WriteAllBytes(fullPath, fileContents);
                // fullPath is now the physical path on disk

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
        public IActionResult ExportToExcelItemsCustomer(InventoryReportViewModel viewModel)
        {
            try
            {

                ExcelPackage.License.SetNonCommercialPersonal("Your Name");

                using var package = new ExcelPackage();
                var ws = package.Workbook.Worksheets.Add("تقارير العملاء");

                // Add headers
                ws.Cells[1, 1].Value = "اسم العميل";
                ws.Cells[1, 2].Value = "عدد الطلبات";
                ws.Cells[1, 3].Value = "الرصيد المستحق";
                ws.Cells[1, 4].Value = "الرصيد غير مستحق";
                ws.Cells[1, 5].Value = "الرصيد المتبقي";



                // Populate rows
                for (int i = 0; i < viewModel.CustomerReport.Count; i++)
                {
                    var item = viewModel.CustomerReport[i];
                    ws.Cells[i + 2, 1].Value = item.Customer.CustomerName;
                    ws.Cells[i + 2, 2].Value = item.OrderCount;
                    ws.Cells[i + 2, 3].Value = item.TotalBalance;
                    ws.Cells[i + 2, 4].Value = item.unearnedBalance;
                    ws.Cells[i + 2, 5].Value = item.RemainingBalance;


                }

                ws.Cells[ws.Dimension.Address].AutoFitColumns();


                // 3) Prepare file contents and name
                byte[] fileContents = package.GetAsByteArray();
                string fileName = $"تقرير العملاء{DateTime.Now:yyyy-MM-dd}.xlsx";


                //// Get the base application directory
                //string basePath = AppDomain.CurrentDomain.BaseDirectory;


                // 4) Save a server-side copy under wwwroot/exports
                string exportsFolder = Path.Combine("exports");
                if (!Directory.Exists(exportsFolder))
                    Directory.CreateDirectory(exportsFolder);

                string fullPath = Path.Combine(exportsFolder, fileName);
                System.IO.File.WriteAllBytes(fullPath, fileContents);
                // fullPath is now the physical path on disk

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
  public IActionResult ViewAllPurchaseOrder()
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1)
                {
                    List<PurchaseOrderVM> purchaseOrderViewModelsList = _businessLogicL.ViewAllPurchaseOrder();
                    return View("~/Views/Inventory/ViewAllPurchaseOrder.cshtml", purchaseOrderViewModelsList);
                }
                else if (jobRole == 2)
                {
                    List<PurchaseOrderVM> purchaseOrderViewModelsList = _businessLogicL.ViewAllPurchaseOrder();
                    return View("~/Views/InventoryClerk/ViewAllPurchaseOrder.cshtml", purchaseOrderViewModelsList);

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
                return View("~/Views/Inventory/ViewAllPurchaseOrder.cshtml", new List<PurchaseOrderVM>());
            }
        }
        public IActionResult ShowPurchaseOrderSpecifications(int purchaseOrderId)
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 1 || jobRole == 2)
                {
                    PurchaseOrderSpecificationsViewModel PurchaseOrderSpecificationsViewModelList = _businessLogicL.ShowPurchaseOrderSpecifications(purchaseOrderId);
                    return View("~/Views/Inventory/ShowPurchaseOrderSpecifications.cshtml", PurchaseOrderSpecificationsViewModelList);
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
                return View("~/Views/Inventory/ShowPurchaseOrderSpecifications.cshtml", new List<PurchaseOrderSpecificationsViewModel>());
            }
        }



    }
}
