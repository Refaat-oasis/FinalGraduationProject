using Microsoft.AspNetCore.Mvc;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.ViewModels;

namespace ThothSystemVersion1.Controllers
{
    public class AccountingController : Controller
    {
        private readonly AccountingBusinessLogicLayer _businessLogicL;
        private readonly TechnicalBusinessLogicLayer _techBusinessLogicL;
        public AccountingController( AccountingBusinessLogicLayer Accbll,
            TechnicalBusinessLogicLayer tbll) 
        {

            _businessLogicL = Accbll;
            _techBusinessLogicL = tbll;

        }

        [HttpGet]
        public IActionResult viewJobOrderWithRemainingAmount()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 6 || jobRole == 7)
            {
                List<JobOrderCustEmpVM> jobOrders = _businessLogicL.getJObOrderWithRemainingAmount();
                return View("~/Views/Accounting/JobOrderWithRemainingAmount.cshtml", jobOrders);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }


        [HttpGet]
        public IActionResult makeReceipt(int JobOrderId)
        {

            //JobOrderSpecificationsViewModel jO = _techBusinessLogicL.ShowJobOrderSpecifications(JobOrderId);
            JobOrder jO = _techBusinessLogicL.GetJobOrderByID(JobOrderId);
            return View(jO);

        }

        [HttpPost]
        public IActionResult makeReceipt()
        {

            //JobOrderSpecificationsViewModel jO = _techBusinessLogicL.ShowJobOrderSpecifications(JobOrderId);
            //JobOrder jO = _techBusinessLogicL.GetJobOrderByID(JobOrderId);
            return View();

        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        public IActionResult viewPurchaseOrderdsWithRemainingAmount()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 6 || jobRole == 7)
            {
                List<PurchaseOrderEmpVendVm> purchaseOrders = _businessLogicL.getPurchaseOrdersWithRemainingAmount();
                return View("~/Views/Accounting/PurchaseOrderWithRemainingAmount.cshtml", purchaseOrders);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }

    }
}
