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
            List<JobOrderCustEmpVM> jobOrderCustomerViewModelsList = _businessLogicLayer.ViewAllJobOrder();
            return View("~/Views/technical/ViewAlljobOrder.cshtml", jobOrderCustomerViewModelsList);

        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new RequisitionCreateVM
            {
                JobOrders = _businessLogicLayer.GetLast10JobOrders(),
                AvailablePapers = _businessLogicLayer.GetAvailablePapers(),
                AvailableInks = _businessLogicLayer.GetAvailableInks(),
                AvailableSupplies = _businessLogicLayer.GetAvailableSupplies()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(RequisiteOrderDTO dto)
        {
            if (ModelState.IsValid)
            {
                var result = _businessLogicLayer.CreateRequisition(dto);
                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToAction("Details", new { id = result.RequisiteId });
                }
                TempData["ErrorMessage"] = result.Message;
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
