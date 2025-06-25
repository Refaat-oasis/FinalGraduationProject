using Microsoft.EntityFrameworkCore;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.Utilities;
using ThothSystemVersion1.ViewModels;

namespace ThothSystemVersion1.BusinessLogicLayers
{
    public class AccountingBusinessLogicLayer : AccountingService
    {
        private readonly ThothContext _context;
        
        public AccountingBusinessLogicLayer(ThothContext  thoth)
        {
            _context = thoth;
        }
        
        public List<JobOrderCustEmpVM> getJObOrderWithRemainingAmount()
        {
            try
            {
                List<JobOrder> joborderList = _context.JobOrders.Where(jo => jo.RemainingAmount > 0).ToList();

                List<Employee> employees = _context.Employees.ToList();
                List<Customer> customers = _context.Customers.ToList();
                List<JobOrderCustEmpVM> jobOrderCustEmpVMs = new List<JobOrderCustEmpVM>();

                foreach (JobOrder joborder in joborderList)
                {
                    JobOrderCustEmpVM custJobOrder = new JobOrderCustEmpVM();

                    custJobOrder.JobOrderId = joborder.JobOrderId;
                    custJobOrder.RemainingAmount = joborder.RemainingAmount;
                    custJobOrder.UnearnedRevenue = joborder.UnearnedRevenue;
                    custJobOrder.JobOrdernotes = joborder.JobOrdernotes;
                    custJobOrder.EarnedRevenue = joborder.EarnedRevenue;
                    custJobOrder.OrderProgress = joborder.OrderProgress;
                    custJobOrder.StartDate = joborder.StartDate;
                    custJobOrder.EndDate = joborder.EndDate;
                    custJobOrder.EmployeeId = joborder.EmployeeId;
                    custJobOrder.EmployeeName = employees.FirstOrDefault(e => e.EmployeeId == joborder.EmployeeId)?.EmployeeName;
                    custJobOrder.CustomerId = joborder.CustomerId ?? 1;
                    custJobOrder.CustomerName = customers.FirstOrDefault(c => c.CustomerId == joborder.CustomerId)?.CustomerName;
                    custJobOrder.CustomerAddress = customers.FirstOrDefault(c => c.CustomerId == joborder.CustomerId)?.CustomerAddress;
                    custJobOrder.CustomerEmail = customers.FirstOrDefault(c => c.CustomerId == joborder.CustomerId)?.CustomerEmail;
                    custJobOrder.CustomerNotes = customers.FirstOrDefault(c => c.CustomerId == joborder.CustomerId)?.CustomerNotes;
                    custJobOrder.CustomerPhone = customers.FirstOrDefault(c => c.CustomerId == joborder.CustomerId)?.CustomerPhone;

                    jobOrderCustEmpVMs.Add(custJobOrder);
                }

                return jobOrderCustEmpVMs;
            }
            catch (Exception ex) {

                WriteException.WriteExceptionToFile(ex);
                return new List<JobOrderCustEmpVM>();

            }

        }

        public List<PaymentPurchaseOrderVM> getPurchaseOrdersWithRemainingAmount()
        {
            try
            {


                List<PurchaseOrder> purchaseOrderList = _context.PurchaseOrders.Where(po => po.RemainingAmount > 0).ToList();
                List<Vendor> vendorList = _context.Vendors.ToList();
                List<Employee> employeeList = _context.Employees.ToList();
                List<PaymentPurchaseOrderVM> purchaseCompleteList = new List<PaymentPurchaseOrderVM>();

                for (int i = 0; i < purchaseOrderList.Count; i++)
                {

                    PaymentPurchaseOrderVM purchOrdVm = new PaymentPurchaseOrderVM();

                    purchOrdVm.PurchaseId = purchaseOrderList[i].PurchaseId;
                    purchOrdVm.PurchaseDate = purchaseOrderList[i].PurchaseDate;
                    purchOrdVm.RemainingAmount = purchaseOrderList[i].RemainingAmount;
                    purchOrdVm.PaidAmount = purchaseOrderList[i].PaidAmount;
                    purchOrdVm.EmployeeName = employeeList.FirstOrDefault(emp => emp.EmployeeId == employeeList[i].EmployeeId).EmployeeName;
                    purchOrdVm.VendorName = vendorList.FirstOrDefault(vend => vend.VendorId == vendorList[i].VendorId).VendorName;

                    purchaseCompleteList.Add(purchOrdVm);
                }
                return purchaseCompleteList;
            }
            catch (Exception ex) { 
            
                WriteException.WriteExceptionToFile(ex);
                return new List<PaymentPurchaseOrderVM>();

            }

        }
        
        public bool makeRecipt(RecieptOrderDTO recDTO)
        {
            try
            {

                JobOrder joborder = _context.JobOrders.FirstOrDefault(jo => jo.JobOrderId == recDTO.JobOrderId);
                if (joborder != null)
                {
                    if (joborder.RemainingAmount < recDTO.Amount)
                    {
                        return false;
                    }

                    joborder.RemainingAmount -= recDTO.Amount;
                    joborder.EarnedRevenue += recDTO.Amount;

                    _context.JobOrders.Update(joborder);
                    RecieptsOrder reciept = new RecieptsOrder();
                    reciept.ReceiptNotes = recDTO.ReceiptNotes;
                    reciept.Amount = recDTO.Amount;
                    reciept.JobOrderId = recDTO.JobOrderId;
                    reciept.EmployeeId = recDTO.EmployeeId;

                    _context.RecieptsOrders.Add(reciept);

                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex) { 
            
                WriteException.WriteExceptionToFile(ex);
                return false;

            }

        }
        
        public bool makePayment(PaymentOrderDTO  paymentdto)
        {
            try {

                PurchaseOrder purchase = _context.PurchaseOrders.FirstOrDefault(purch => purch.PurchaseId == paymentdto.PurchaseId);
                if (purchase != null)
                {
                    if (purchase.RemainingAmount < paymentdto.Amount)
                    {
                        return false;
                    }

                    purchase.RemainingAmount -= paymentdto.Amount;
                    purchase.PaidAmount += paymentdto.Amount;

                    _context.PurchaseOrders.Update(purchase);
                    PaymentOrder payment = new PaymentOrder();
                    payment.PaymentNotes = paymentdto.PaymentNotes;
                    payment.Amount = paymentdto.Amount;
                    payment.PurchaseId = paymentdto.PurchaseId;
                    payment.EmployeeId = paymentdto.EmployeeId;

                    _context.PaymentOrders.Add(payment);

                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                WriteException.WriteExceptionToFile(ex);
                return false;
            }
        }

        public (bool success, string message) editJobOrder(int jobOrderID, JobOrderDTO jobOrder)
        {

            try
            {
                JobOrder existingJobOrder = _context.JobOrders
            .Include(j => j.Customer)
            .Include(j => j.Employee)
            .FirstOrDefault(j => j.JobOrderId == jobOrderID);
                if (existingJobOrder == null)
                {
                    return (false, "امر العمل غير موجود");
                }


                existingJobOrder.RemainingAmount = jobOrder.RemainingAmount;
                existingJobOrder.UnearnedRevenue = jobOrder.UnearnedRevenue;
                existingJobOrder.EarnedRevenue = jobOrder.EarnedRevenue;
                existingJobOrder.JobOrdernotes = jobOrder.JobOrdernotes;
                _context.JobOrders.Update(existingJobOrder); // Mark the entity as modified
                _context.SaveChanges();
                return (true, "تمت تعديل امر العمل بنجاح");
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return (false, $"حدث خطأ: {ex.ToString()}");
            }
        }

        public bool editPurchaseOrder(int orderid, PurchaseOrder purchaseOrder)
        {
            throw new NotImplementedException();
        }

        public PaymentPurchaseOrderVM gitPurchaseOrderVM(int purchaseID) {
            try {

                PurchaseOrder payment = _context.PurchaseOrders.FirstOrDefault(pay => pay.PurchaseId == purchaseID);
                Employee emp = _context.Employees.FirstOrDefault(emp => emp.EmployeeId == payment.EmployeeId);
                Vendor ven = _context.Vendors.FirstOrDefault(vend => vend.VendorId == payment.VendorId);

                PaymentPurchaseOrderVM paymentVm = new PaymentPurchaseOrderVM();
                // purchase
                paymentVm.PurchaseId = payment.PurchaseId;
                paymentVm.PurchaseNotes = payment.PurchaseNotes;
                paymentVm.PurchaseDate = payment.PurchaseDate;
                paymentVm.PaidAmount = payment.PaidAmount;
                paymentVm.RemainingAmount = payment.RemainingAmount;

                //vendor
                paymentVm.VendorId = payment.VendorId;
                paymentVm.VendorName = ven.VendorName;
                // employee
                paymentVm.EmployeeId = payment.EmployeeId;
                paymentVm.EmployeeName = emp.EmployeeName;



                return paymentVm;


            }
            catch (Exception ex) {  
            
                WriteException.WriteExceptionToFile(ex);
                return new PaymentPurchaseOrderVM();

            }


        }

    }
    
    }

