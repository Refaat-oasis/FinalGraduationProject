using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.Utilities;
using ThothSystemVersion1.ViewModels;

namespace ThothSystemVersion1.Controllers
{
    public class AccountingController : Controller
    {
        private readonly AccountingBusinessLogicLayer _businessLogicL;
        private readonly TechnicalBusinessLogicLayer _techBusinessLogicL;
        private readonly CostBusinessLogicLayer _costBusinessLogicL;
        public AccountingController( AccountingBusinessLogicLayer Accbll,
            TechnicalBusinessLogicLayer tbll,
            CostBusinessLogicLayer costbll
            ) 
        {

            _businessLogicL = Accbll;
            _techBusinessLogicL = tbll;
            _costBusinessLogicL = costbll;

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
        public IActionResult makeReceipt(ReceiptJobOrderVM receiptVM)
        {
            try
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
                        string message = "تم استلام الاموال";
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
            catch (Exception ex)
            {
                TempData["Error"] = "حدث خطأ أثناء استلام الاموال";
                return View(receiptVM);

            }
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
        public IActionResult makePayment(PaymentPurchaseOrderVM paymentVM)
        {



            try
            {
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
            catch (Exception ex)
            {
                TempData["Error"] = "حدث خطأ أثناء دفع الاموال";
                return View(paymentVM);

            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        public IActionResult viewJobOrderWithCost() {


            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 7 || jobRole == 8)
            {
                List<JobOrderCustEmpVM> jobOrders = _costBusinessLogicL.GetJobOrdersWithProcessBridge();
                return View("~/Views/Accounting/JobOrderWithCost.cshtml", jobOrders);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee");
            }


        }

        [HttpGet]
        public IActionResult EditJobOrder(int jobOrderid)
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 7 || jobRole ==8)
            {
                try
                {
                    JobOrder existingJobOrder = _techBusinessLogicL.GetJobOrderByID(jobOrderid);
                    ViewBag.EmployeeList = _techBusinessLogicL.GetAvailableEmployees();
                    ViewBag.CustomerList = _techBusinessLogicL.GetAvailableCustomerss();


                    if (existingJobOrder.StartDate == default)
                    {
                        existingJobOrder.StartDate = DateOnly.FromDateTime(DateTime.Today);
                    }


                    return View("~/Views/accounting/editJobOrderAccounting.cshtml", existingJobOrder);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    return RedirectToAction("viewJobOrderWithCost"); // Redirect to list with error
                }
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee");
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

                var result = _businessLogicL.editJobOrder(jobOrderid, jODto);

                if (result.success)
                {
                    TempData["Success"] = result.message;
                    return RedirectToAction("EditJobOrder", jobOrder);
                }
                TempData["Error"] = result.message;
                return RedirectToAction("EditJobOrder", jobOrder);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات امر العمل";
                return View("EditJobOrder", jobOrder);
            }
        }

        [HttpGet]
        public IActionResult accountingReport() {

            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 7) {

                try {
                    return View(new AccountingReportViewModel());
                } catch (Exception e) {
                    WriteException.WriteExceptionToFile(e);
                    return View(new AccountingReportViewModel());
                }

            } else {
                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }


        [HttpPost]
        public IActionResult accountingReport(DateOnly beginingDate, DateOnly endingDate)
        {
            try 
            { 
            
                AccountingReportViewModel viewModel = _businessLogicL.accountReport(beginingDate, endingDate);
                if (viewModel == null)
                {
                    return View(new AccountingReportViewModel());
                }
                else { 
                    return View(viewModel);
                }

            } 
            catch (Exception ex) 
            {
                WriteException.WriteExceptionToFile(ex);
                return View(new AccountingReportViewModel());
            }
        
        
        
        }

    }
}
