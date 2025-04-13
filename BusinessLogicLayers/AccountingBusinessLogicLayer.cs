using Microsoft.EntityFrameworkCore;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.Models;
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
            
            List<JobOrder> joborderList = _context.JobOrders.Where(jo => jo.RemainingAmount > 0).ToList();

            List<Employee> employees = _context.Employees.ToList();
            List<Customer> customers = _context.Customers.ToList();
            List<JobOrderCustEmpVM> jobOrderCustEmpVMs = new List<JobOrderCustEmpVM>();

            foreach (JobOrder joborder in joborderList) {
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

        public List<PurchaseOrderEmpVendVm> getPurchaseOrdersWithRemainingAmount()
        {
            throw new NotImplementedException();
        }
        public bool makeRecipt()
        {
            throw new NotImplementedException();
        }
        public bool makePayment()
        {
            throw new NotImplementedException();
        }
        public bool editJobOrder(int orderid, JobOrder jobOrder)
        {
            throw new NotImplementedException();
        }
        public bool editPurchaseOrder(int orderid, PurchaseOrder purchaseOrder)
        {
            throw new NotImplementedException();
        }
    }
    
    }

