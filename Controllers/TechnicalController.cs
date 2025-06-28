using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.DataTransfereObject;
//using ThothSystemVersion1.DTOs;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.Utilities;
using ThothSystemVersion1.ViewModels;

namespace ThothSystemVersion1.Controllers
{
    public class TechnicalController : Controller
    {

        private readonly TechnicalBusinessLogicLayer _technicalBusinessLogicLayer;
        private readonly InventoryBussinesLogicLayer _inventoryBusinessLogicLayer;

        public TechnicalController(
            TechnicalBusinessLogicLayer technicalBusinessLogicLayer,
            InventoryBussinesLogicLayer inventoryBusinessLogicLayer)
        {
            _technicalBusinessLogicLayer = technicalBusinessLogicLayer;
            _inventoryBusinessLogicLayer = inventoryBusinessLogicLayer;
        }

        [HttpGet]
        public IActionResult ViewAllJobOrder()
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 3)
                {
                    List<JobOrderCustEmpVM> jobOrderCustomerViewModelsList = _technicalBusinessLogicLayer.ViewAllJobOrder();
                    return View("~/Views/technical/ViewAlljobOrder.cshtml", jobOrderCustomerViewModelsList);
                }
                else if (jobRole == 4)
                {
                    List<JobOrderCustEmpVM> jobOrderCustomerViewModelsList = _technicalBusinessLogicLayer.ViewAllJobOrder();
                    return View("~/Views/technicalclerk/ViewAlljobOrder.cshtml", jobOrderCustomerViewModelsList);

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
                return View("~/Views/Technical/ViewAllJobOrder.cshtml", new List<JobOrderCustEmpVM>());
            }
        }

        public IActionResult ShowJobOrderSpecifications(int jobOrderId)
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 3 || jobRole == 4 || jobRole == 7 || jobRole == 8)
                {
                    JobOrderSpecificationsViewModel JobOrderSpecificationsViewModelList = _technicalBusinessLogicLayer.ShowJobOrderSpecifications(jobOrderId);
                    return View("~/Views/technical/showJobOrderSpecifications.cshtml", JobOrderSpecificationsViewModelList);
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
                return View("~/Views/Technical/showJobOrderSpecifications.cshtml", new List<JobOrderSpecificationsViewModel>());
            }
        }
        [HttpGet]
        public IActionResult ViewAllCustomer()
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 3)
                {
                    List<Customer> customerList = _technicalBusinessLogicLayer.ViewAllCustomer();
                    return View(customerList);
                }
                else if (jobRole == 4)
                {
                    List<Customer> customerList = _technicalBusinessLogicLayer.ViewAllCustomer();
                    return View("~/Views/technicalclerk/ViewAllCustomer.cshtml", customerList);
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
                return View("~/Views/Technical/ViewAllCustomer.cshtml", new List<Customer>());
            }

        }

        [HttpGet]
        public IActionResult CreateRequisite()
        {
            try 
            { 
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 3 || jobRole == 4)
            {
                ViewBag.PaperList = _inventoryBusinessLogicLayer.GetActivePapers();
                ViewBag.InkList = _inventoryBusinessLogicLayer.GetActiveInks();
                ViewBag.SupplyList = _inventoryBusinessLogicLayer.GetActiveSupplies();
                ViewBag.JobOrderList = _inventoryBusinessLogicLayer.GetRecentJobOrdersWithCustomers();
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
                return View("~/Views/Technical/CreateRequisite.cshtml", new RequisiteOrderDTO());
            }

        }


        public JsonResult GetCurrentQuantity(string itemType, int itemId)
        {
            try
            {
                int quantity = _inventoryBusinessLogicLayer.GetCurrentQuantity(itemType, itemId);
                return Json(new { currentQuantity = quantity });
            }

            catch (Exception ex)
            {
                
                WriteException.WriteExceptionToFile(ex);
                return Json(new { error = "حدث خطأ غير متوقع. يرجى المحاولة لاحقاً." });
            }
        }


        [HttpPost]
        public IActionResult CreateRequisite(RequisiteOrderDTO dto)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                dto.EmployeeId = HttpContext.Session.GetString("EmployeeID");
                var result = _technicalBusinessLogicLayer.CreateRequisite(dto);
                if (result.success)
                {
                    TempData["Success"] = result.message;
                    return RedirectToAction("CreateRequisite");
                }
                TempData["Error"] = result.message;
                //}
                return RedirectToAction("CreateRequisite");
            }

            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View(new RequisiteOrderDTO());
            }

        }
        [HttpGet]
        public IActionResult CreateNewJobOrder()
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 3 || jobRole == 4)
                {
                    ViewBag.employeeList = _technicalBusinessLogicLayer.GetAvailableEmployees();
                    ViewBag.customerList = _technicalBusinessLogicLayer.GetAvailableCustomerss();


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
                return View("~/Views/Technical/CreateNewJobOrder.cshtml", new JobOrderDTO());
            }
        }

        [HttpPost]
        public IActionResult CreateNewJobOrder(JobOrderDTO jobOrder)
        {
            try
            {
                string employeeId = HttpContext.Session.GetString("EmployeeID");
                jobOrder.EmployeeId = employeeId;

                var result = _technicalBusinessLogicLayer.AddJobOrder(jobOrder);
                //if (result.success)
                //{
                //    TempData["Success"] = result.message;
                //    return RedirectToAction("CreateNewJobOrder");
                //}
                //TempData["Error"] = result.message;
                if (result.success)
                    TempData["Success"] = result.message;
                else
                    TempData["Error"] = result.message;

                return RedirectToAction("CreateNewJobOrder");
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Technical/CreateNewJobOrder.cshtml", jobOrder);
            }

        }

        [HttpGet]
        public IActionResult EditJobOrder(int jobOrderid)
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 3)
                {

                    JobOrder existingJobOrder = _technicalBusinessLogicLayer.GetJobOrderByID(jobOrderid);
                    ViewBag.EmployeeList = _technicalBusinessLogicLayer.GetAvailableEmployees();
                    ViewBag.CustomerList = _technicalBusinessLogicLayer.GetAvailableCustomerss();


                    if (existingJobOrder.StartDate == default)
                    {
                        existingJobOrder.StartDate = DateOnly.FromDateTime(DateTime.Today);
                    }


                    return View("~/Views/Technical/EditJobOrder.cshtml", existingJobOrder);


                }
                else
                {
                    return RedirectToAction("UnauthorizedAccess", "employee");
                }
            }
            //catch (ArgumentException ex)
            //{
            //    TempData["Error"] = ex.Message;
            //    return RedirectToAction("ViewAllJobOrder"); // Redirect to list with error
            //}
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Technical/EditJobOrder.cshtml", new JobOrderDTO());
            }
        }
        [HttpPost]
        public IActionResult EditJobOrder(int jobOrderid, JobOrder jobOrder)
        {
            try
            {
                JobOrderDTO jODto = new JobOrderDTO();
                jODto.CustomerId = jobOrder.CustomerId;
                jODto.OrderProgress = jobOrder.OrderProgress;
                jODto.JobOrderId = jobOrder.JobOrderId;
                jODto.JobOrdernotes = jobOrder.JobOrdernotes;
                jODto.EarnedRevenue = jobOrder.EarnedRevenue;
                jODto.UnearnedRevenue = jobOrder.UnearnedRevenue;
                jODto.RemainingAmount = jobOrder.RemainingAmount;
                jODto.EndDate = jobOrder.EndDate;
                jODto.StartDate = jobOrder.StartDate;
                jODto.EmployeeId = jobOrder.EmployeeId;


                ModelState.Clear();
                TryValidateModel(jODto);

                //if (!ModelState.IsValid)
                //{

                //    return View("~/Views/Technical/EditJobOrder.cshtml", jobOrder);
                //}

                var result = _technicalBusinessLogicLayer.EditJobOrder(jobOrderid, jODto);

                if (result.success)
                {
                    TempData["Success"] = result.message;
                    //return RedirectToAction("EditJobOrder", "Technical", new {jobOrderid});
                    return RedirectToAction("EditJobOrder", jobOrder);

                }
                TempData["Error"] = result.message;
                return RedirectToAction("EditJobOrder", jobOrder);
            }
            //catch (Exception ex)
            //{
            //    TempData["Error"] = "حدث خطأ أثناء تعديل بيانات امر العمل";
            //    return View(jobOrder);
            //}
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Technical/EditJobOrder.cshtml", jobOrder);
            }
        }
        [HttpGet]
        public IActionResult AddCustomer()
        {
            try
            { 
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 3 || jobRole == 4)
            {
                    var model = new CustomerDTO(); 
                    return View("~/Views/Technical/AddCustomer.cshtml", model); 
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
            }
            //catch (ApplicationException ex)
            //{
            //    return StatusCode(500, ex.Message);
            //}
            //catch (ArgumentException ex)
            //{
            //    return NotFound(ex.Message);
            //}
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Technical/AddCustomer.cshtml", new CustomerDTO());
            }

        }

        [HttpPost]
        public IActionResult AddCustomer(CustomerDTO customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("~/Views/Technical/AddCustomer.cshtml", customer);
                }

                bool isCustomerAdded = _technicalBusinessLogicLayer.AddCustomer(customer);

                if (!isCustomerAdded)
                {
                    TempData["Error"]=("", "البريد الالكتروني او رقم هاتف تم استخدامه من قبل");

                    return View("~/Views/Technical/AddCustomer.cshtml", customer);
                }

                TempData["Success"] = "تم إضافة العميل بنجاح";
                return RedirectToAction("AddCustomer", "Technical");
            }
            //catch (ArgumentException ex)
            //{
            //    TempData["Error"] = "حدث خطأ أثناء إضافة العميل";
            //    return View("~/Views/Technical/AddCustomer.cshtml", customer);
            //}
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء إضافة العميل.";
                return View("~/Views/Technical/AddCustomer.cshtml", customer);
            }
        }
        [HttpGet]
        public IActionResult EditCustomer(int CustomerId)
        {
            try
            {
                int? jobRole = HttpContext.Session.GetInt32("JobRole");
                if (jobRole == 0 || jobRole == 3)
                {
                    var customer = _technicalBusinessLogicLayer.EditCustomer(CustomerId);

                    if (customer == null)
                    {
                        return NotFound();
                    }


                    CustomerDTO customerDto = new CustomerDTO
                    {
                        CustomerId = customer.CustomerId,
                        CustomerName = customer.CustomerName,
                        CustomerEmail = customer.CustomerEmail,
                        CustomerPhone = customer.CustomerPhone,
                        CustomerAddress = customer.CustomerAddress,
                        CustomerNotes = customer.CustomerNotes,
                        CustomerSource = customer.CustomerSource,
                        Activated = customer.Activated
                    };

                    return View("~/Views/Technical/EditCustomer.cshtml", customerDto);
                }
                else
                {
                    return RedirectToAction("UnauthorizedAccess", "Employee");
                }
            }
            //catch (ArgumentException ex)
            //{
            //    TempData["Error"] = "حدث خطأ في المعاملات المدخلة.";
            //    return View("~/Views/Technical/EditCustomer.cshtml");
            //}
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/Technical/EditCustomer.cshtml", new CustomerDTO());
            }
        }

        [HttpPost]
        public IActionResult EditCustomer(int CustomerId, CustomerDTO updatedCustomer)
        {
            try
            {
                if (updatedCustomer == null)
                {
                    ModelState.AddModelError("", "البيانات غير صالحة");
                    return View("~/Views/Technical/EditCustomer.cshtml", updatedCustomer);
                }

                if (!ModelState.IsValid)
                {
                    return View("~/Views/Technical/EditCustomer.cshtml", updatedCustomer);
                }


                bool isEditSuccess = _technicalBusinessLogicLayer.EditCustomer(CustomerId, updatedCustomer);

                if (!isEditSuccess)
                {

                    TempData["Error"] = ("", "البريد الالكتروني او رقم هاتف تم استخدامه من قبل");
                    return View("~/Views/Technical/EditCustomer.cshtml", updatedCustomer);
                }

                TempData["Success"] = "تم تعديل بيانات العميل";
                return RedirectToAction("EditCustomer", "Technical", new { CustomerId });
            }
            //catch (ArgumentException ex)
            //{
            //    TempData["Error"] = "حدث خطأ أثناء تعديل بيانات العميل";
            //    return View("~/Views/Technical/EditCustomer.cshtml", updatedCustomer);
            //}
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات العميل.";
                return View("~/Views/Technical/EditCustomer.cshtml", updatedCustomer);
            }
        }


        [HttpGet]
        public IActionResult TechnicalReport()
        {

            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 3)
            {

                try
                {
                    return View(new TechnicalReportViewModel());
                }
                catch (Exception e)
                {
                    WriteException.WriteExceptionToFile(e);
                    return View(new TechnicalReportViewModel());
                }

            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }

        [HttpPost]
        public IActionResult TechnicalReport(DateOnly beginingDate, DateOnly endingDate)
        {
            try
            {

                TechnicalReportViewModel technicalReportViewModel = _technicalBusinessLogicLayer.TechnicalReport(beginingDate, endingDate);
                if (technicalReportViewModel == null)
                {
                    return View(new TechnicalReportViewModel());
                }
                else
                {
                    return View(technicalReportViewModel);
                }

            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return View(new TechnicalReportViewModel());
            }



        }


    }
    //[HttpPost]
    //public IActionResult Create(RequisiteOrderDTO dto)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        var vm = new RequisitionCreateVM
    //        {
    //            JobOrders = _businessLogicLayer.GetLast10JobOrders(),
    //            AvailablePapers = _businessLogicLayer.GetAvailablePapers(),
    //            AvailableInks = _businessLogicLayer.GetAvailableInks(),
    //            AvailableSupplies = _businessLogicLayer.GetAvailableSupplies(),
    //            RequisiteOrderDTO = dto
    //        };
    //        return View(vm);
    //    }

    //    var result = _businessLogicLayer.CreateRequisition(dto);

    //    if (result.Success)
    //    {
    //        TempData["SuccessMessage"] = result.Message;
    //        return RedirectToAction("Details", new { id = result.RequisiteId });
    //    }

    //    TempData["ErrorMessage"] = result.Message;
    //    return RedirectToAction("Create");
    //}


}







//    public class TechnicalController : Controller
//    {

//        private readonly TechnicalBusinessLogicLayer _businessLogicLayer;

//        public TechnicalController(TechnicalBusinessLogicLayer _technicalBussinessLogicLayer) {

//            _businessLogicLayer = _technicalBussinessLogicLayer; 

//        }

//        [HttpGet]
//        public IActionResult ViewAllJobOrder()
//        {
//            List<JobOrderCustEmpVM> jobOrderCustomerViewModelsList = _businessLogicLayer.ViewAllJobOrder();
//            return View("~/Views/technical/ViewAlljobOrder.cshtml", jobOrderCustomerViewModelsList);

//        }
//        [HttpGet]
//        public IActionResult Create()
//        {
//            var vm = new RequisitionCreateVM
//            {
//                JobOrders = _businessLogicLayer.GetLast10JobOrders(),
//                AvailablePapers = _businessLogicLayer.GetAvailablePapers(),
//                AvailableInks = _businessLogicLayer.GetAvailableInks(),
//                AvailableSupplies = _businessLogicLayer.GetAvailableSupplies(),
//            };

//            return View("~/Views/Technical/AddRequisiteOrder.cshtml", vm);
//        }

//        [HttpPost]
//        public IActionResult Create(RequisiteOrderDTO dto)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = _businessLogicLayer.CreateRequisition(dto);
//                if (result.Success)
//                {
//                    TempData["SuccessMessage"] = result.Message;
//                    return RedirectToAction("Details", new { id = result.RequisiteId });
//                }
//                TempData["ErrorMessage"] = result.Message;
//            }

//            // إعادة تحميل البيانات في حالة وجود خطأ
//            var vm = new RequisitionCreateVM
//            {
//                JobOrders = _businessLogicLayer.GetLast10JobOrders(),
//                AvailablePapers = _businessLogicLayer.GetAvailablePapers(),
//                AvailableInks = _businessLogicLayer.GetAvailableInks(),
//                AvailableSupplies = _businessLogicLayer.GetAvailableSupplies(),
//                RequisiteOrderDTO = dto
//            };

//            return View(vm);
//        }
//    }
//}
