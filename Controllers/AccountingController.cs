using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.DataTransfereObject;
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
            if (jobRole == 0 || jobRole == 7 || jobRole == 8)
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
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 7 || jobRole == 8)
            {
                JobOrderCustEmpVM jO = _techBusinessLogicL.getJobOrderVM(JobOrderId);
            ReceiptJobOrderVM RJO = new ReceiptJobOrderVM();
            RJO.JobOrderId = jO.JobOrderId;
            RJO.CustomerId = jO.CustomerId;
            RJO.StartDate = jO.StartDate;
            RJO.EndDate = jO.EndDate;
            RJO.JobOrdernotes = jO.JobOrdernotes;
            RJO.RemainingAmount = jO.RemainingAmount;
            RJO.UnearnedRevenue = jO.UnearnedRevenue;
            RJO.EarnedRevenue = jO.EarnedRevenue;
            RJO.EmployeeId = jO.EmployeeId;
            RJO.EmployeeName = jO.EmployeeName;
            RJO.CustomerName = jO.CustomerName;

            return View(RJO);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }

        [HttpPost]
        public IActionResult makeReceipt( ReceiptJobOrderVM receiptVM)
        {
            

            RecieptOrderDTO receiptDTO = new RecieptOrderDTO();
            receiptDTO.JobOrderId = receiptVM.JobOrderId;
            receiptDTO.ReceiptNotes = receiptVM.ReceiptNotes;
            receiptDTO.Amount = receiptVM.Amount;
            receiptDTO.EmployeeId = HttpContext.Session.GetString("EmployeeID");


            ModelState.Clear();
            TryValidateModel(receiptDTO);
            if (ModelState.IsValid)
            {
                // Save the receipt to the database
                bool result = _businessLogicL.makeRecipt(receiptDTO);
                if (result)
                {
                    // Redirect to a success page or show a success message
                    string message= "تم استلام الاموال";
                    TempData["Success"] = message;
                    return View(receiptVM);
                }
                else
                {
                    string message = "حدث خطأ في العملية";
                    TempData["Error"] = message;
                    // Handle the error case
                    return View(receiptVM);
                }
            }
            return View(receiptVM);

        }


/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        public IActionResult viewPurchaseOrderdsWithRemainingAmount()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 7 || jobRole == 8)
            {
                List<PaymentPurchaseOrderVM> purchaseOrders = _businessLogicL.getPurchaseOrdersWithRemainingAmount();
                return View("~/Views/Accounting/PurchaseOrderWithRemainingAmount.cshtml", purchaseOrders);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee"); 
            }
        }
        [HttpGet]
        public IActionResult makePayment(int purchaseID) {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 7 || jobRole == 8)
            {
                PaymentPurchaseOrderVM payment = _businessLogicL.gitPurchaseOrderVM(purchaseID);
            return View(payment);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee");
            }

        }
        [HttpPost]
        public IActionResult makePayment(PaymentPurchaseOrderVM paymentVM) {



            PaymentOrderDTO paymentDTO = new PaymentOrderDTO();
            paymentDTO.PurchaseId = paymentVM.PurchaseId;
            paymentDTO.PaymentNotes = paymentVM.PaymentNotes;
            paymentDTO.Amount = paymentVM.Amount;
            paymentDTO.EmployeeId = HttpContext.Session.GetString("EmployeeID");


            ModelState.Clear();
            TryValidateModel(paymentDTO);
            if (ModelState.IsValid)
            {
                // Save the receipt to the database
                bool result = _businessLogicL.makePayment(paymentDTO);
                if (result)
                {
                    // Redirect to a success page or show a success message
                    string message = "تم دفع الاموال";
                    TempData["Success"] = message;
                    return View(paymentVM);
                }
                else
                {
                    string message = "حدث خطأ في العملية";
                    TempData["Error"] = message;
                    // Handle the error case
                    return View(paymentVM);
                }
            }
            return View(paymentVM);

        }

    }
}
