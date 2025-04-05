using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.DataTransfereObject;
//using ThothSystemVersion1.DTOs;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.ViewModels;

namespace ThothSystemVersion1.Controllers
{
    public class TechnicalController : Controller
    {

        private readonly TechnicalBusinessLogicLayer _businessLogicLayer;

        public TechnicalController(TechnicalBusinessLogicLayer _technicalBussinessLogicLayer)
        {

            _businessLogicLayer = _technicalBussinessLogicLayer;

        }

        [HttpGet]
        public IActionResult ViewAllJobOrder()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 3 || jobRole == 4)
            {
                List<JobOrderCustEmpVM> jobOrderCustomerViewModelsList = _businessLogicLayer.ViewAllJobOrder();
                return View("~/Views/technical/ViewAlljobOrder.cshtml", jobOrderCustomerViewModelsList);
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
                var vm = new RequisitionCreateVM
                {
                    JobOrders = _businessLogicLayer.GetJobOrdersWithCustomers(),
                    AvailablePapers = _businessLogicLayer.GetAvailablePapers(),
                    AvailableInks = _businessLogicLayer.GetAvailableInks(),
                    AvailableSupplies = _businessLogicLayer.GetAvailableSupplies()
                };

                return View(vm);
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }

        [HttpPost]
        public IActionResult CreateRequisite(RequisiteOrderDTO dto)
        {
            if (ModelState.IsValid)
            {
                dto.EmployeeId = HttpContext.Session.GetString("EmployeeID");
                var result = _businessLogicLayer.CreateRequisite(dto);
                if (result.success)
                {
                    TempData["SuccessMessage"] = result.message;
                    return RedirectToAction("CreateRequisite");
                }
                TempData["ErrorMessage"] = result.message;
            }

            // إعادة تحميل البيانات في حالة وجود خطأ
            var vm = new RequisitionCreateVM
            {
                JobOrders = _businessLogicLayer.GetLast10JobOrders(),
                AvailablePapers = _businessLogicLayer.GetAvailablePapers(),
                AvailableInks = _businessLogicLayer.GetAvailableInks(),
                AvailableSupplies = _businessLogicLayer.GetAvailableSupplies(),
                RequisiteOrderDTO = dto
            };

            return View(vm);
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
