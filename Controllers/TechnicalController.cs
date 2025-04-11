using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.DataTransfereObject;
//using ThothSystemVersion1.DTOs;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.Models;
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
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 3 || jobRole == 4)
            {
                List<JobOrderCustEmpVM> jobOrderCustomerViewModelsList = _technicalBusinessLogicLayer.ViewAllJobOrder();
                return View("~/Views/technical/ViewAlljobOrder.cshtml", jobOrderCustomerViewModelsList);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }

        [HttpGet]
        public IActionResult ShowJobOrderSpecifications(int jobOrderId)
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 3 || jobRole == 4)
            {
                JobOrderSpecificationsViewModel JobOrderSpecificationsViewModelList = _technicalBusinessLogicLayer.ShowJobOrderSpecifications(jobOrderId);
                return View("~/Views/technical/showJobOrderSpecifications.cshtml", JobOrderSpecificationsViewModelList);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }

        [HttpGet]
        public IActionResult ViewAllCustomer()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 3 || jobRole == 4)
            {
                List<Customer> customerList = _technicalBusinessLogicLayer.ViewAllCustomer();
                return View(customerList);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee");
            }

        }

        [HttpGet]
        public IActionResult CreateRequisite()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 3 || jobRole == 4)
            {
                ViewBag.PaperList = _inventoryBusinessLogicLayer.GetActivePapers();
                ViewBag.InkList = _inventoryBusinessLogicLayer.GetActiveInks();
                ViewBag.SupplyList = _inventoryBusinessLogicLayer.GetActiveSupplies();
                ViewBag.JobOrderList = _technicalBusinessLogicLayer.GetLast10JobOrders();

                return View();
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }


        public JsonResult GetCurrentQuantity(string itemType, int itemId)
        {
            int quantity = _inventoryBusinessLogicLayer.GetCurrentQuantity(itemType, itemId);
            return Json(new { currentQuantity = quantity });
        }
        [HttpPost]
        public IActionResult CreateRequisite(RequisiteOrderDTO dto)
        {
            //if (ModelState.IsValid)
            //{
            dto.EmployeeId = HttpContext.Session.GetString("EmployeeID");
            var result = _technicalBusinessLogicLayer.CreateRequisite(dto);
            if (result.success)
            {
                TempData["SuccessMessage"] = result.message;
                return RedirectToAction("CreateRequisite");
            }
            TempData["ErrorMessage"] = result.message;
            //}



            return RedirectToAction("CreateRequisite");
        }
        [HttpGet]
        public IActionResult CreateNewJobOrder()
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

        [HttpPost]
        public IActionResult CreateNewJobOrder(JobOrderDTO jobOrder)
        {
            var result = _technicalBusinessLogicLayer.AddJobOrder(jobOrder);
            if (result.success)
            {
                TempData["SuccessMessage"] = result.message;
                return RedirectToAction("CreateNewJobOrder");
            }
            TempData["ErrorMessage"] = result.message;


            return RedirectToAction("CreateNewJobOrder");
        }
        [HttpGet]
        public IActionResult EditJobOrder(int jobOrderid)
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 3)
            {
                try
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
                catch (ArgumentException ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return RedirectToAction("ViewAllJobOrder"); // Redirect to list with error
                }
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }

        [HttpPost]
        public IActionResult EditJobOrder(int jobOrderid, JobOrderDTO jobOrder)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View("~/Views/Technical/EditJobOrder.cshtml", jobOrder);
                }

                var result = _technicalBusinessLogicLayer.EditJobOrder(jobOrderid, jobOrder);

                if (result.success)
                {
                    TempData["SuccessMessage"] = result.message;
                    return RedirectToAction("ViewAllJobOrder");
                }
                TempData["ErrorMessage"] = result.message;

                return RedirectToAction("ViewAllJobOrder", "Technical");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات امر العمل";
                return View(jobOrder);
            }
        }
        [HttpGet]
        public IActionResult AddCustomer()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 3 || jobRole == 4)
            {
                try
                {
                    CustomerDTO empty = new CustomerDTO();
                    return View("~/Views/Technical/AddCustomer.cshtml", empty);
                }
                catch (ApplicationException ex)
                {
                    return StatusCode(500, ex.Message);
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
        public IActionResult AddCustomer(CustomerDTO customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("~/Views/Technical/AddCustomer.cshtml", customer);
                }

                bool isVendorAdded = _technicalBusinessLogicLayer.AddCustomer(customer);

                if (!isVendorAdded)
                {
                    ModelState.AddModelError("", "الايميل او رقم الهاتف تم استخدامه من قبل");
                    return View("~/Views/Technical/AddCustomer.cshtml", customer);
                }

                TempData["Success"] = "تم إضافة العميل بنجاح";
                return RedirectToAction("AddCustomer", "Technical");
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = "حدث خطأ أثناء إضافة العميل";
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
                    Customer foundCustomer = _technicalBusinessLogicLayer.EditCustomer(CustomerId);
                    return View("~/Views/Technical/EditCustomer.cshtml", foundCustomer);
                }
                else
                {

                    return RedirectToAction("UnauthorizedAccess", "employee");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new ApplicationException("An error occurred while fetching the Customer.", ex);
            }


        }
        [HttpPost]
        public IActionResult EditCustomer(int CustomerId, Customer customer)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{

                //    return View("~/Views/Technical/EditCustomer.cshtml", customer);
                //}

                bool isCustomerEdited = _technicalBusinessLogicLayer.EditCustomer(CustomerId, customer);

                //if (!isCustomerEdited)
                //{
                //    TempData["Error"] = ("", "البريد الالكتروني او رقم الهاتف تم استخدامه من قبل");
                //    return View("~/Views/Technical/EditCustomer.cshtml", customer);
                //}
                TempData["Success"] = "تم تعديل بيانات العميل";
                return RedirectToAction("EditCustomer", "Technical", new { CustomerId });

            }
            catch (Exception ex)
            {
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات العميل";
                return View("~/Views/Technical/EditCustomer.cshtml", customer);
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
