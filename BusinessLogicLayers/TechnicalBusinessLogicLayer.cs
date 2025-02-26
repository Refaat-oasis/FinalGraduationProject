using Microsoft.EntityFrameworkCore;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.ViewModels;

namespace ThothSystemVersion1.BusinessLogicLayers
{
    public class TechnicalBusinessLogicLayer : TechnicalService
    {
        private readonly ThothContext _context;
        public TechnicalBusinessLogicLayer(ThothContext context)
        {
            //ThothContext _context = new ThothContext();
            _context = context;
        }


        public List<JobOrderCustEmpVM> ViewAllJobOrder()
        {

            List<JobOrder> jobOrdersList = _context.JobOrders.ToList();
            List<Customer> customersList = _context.Customers.ToList();
            List<Employee> employeeList = _context.Employees.ToList();
            List<JobOrderCustEmpVM> jobOrderCustomerViewModelsList = new List<JobOrderCustEmpVM>();

            foreach (JobOrder jobOrder in jobOrdersList)
            {

                Customer customer = customersList.FirstOrDefault(c => c.CustomerId == jobOrder.CustomerId);
                Employee employee = employeeList.FirstOrDefault(e => e.EmployeeId == jobOrder.EmployeeId);

                if (customer != null & employee != null)
                {

                    JobOrderCustEmpVM jobToView = new JobOrderCustEmpVM();

                    // first add the customer data
                    jobToView.CustomerId = customer.CustomerId;
                    jobToView.CustomerAddress = customer.CustomerAddress;
                    jobToView.CustomerPhone = customer.CustomerPhone;
                    jobToView.CustomerName = customer.CustomerName;
                    jobToView.CustomerEmail = customer.CustomerEmail;

                    // second add the job order data
                    jobToView.JobOrderId = jobOrder.JobOrderId;
                    jobToView.OrderProgress = jobOrder.OrderProgress;
                    jobToView.RemainingAmount = jobOrder.RemainingAmount;
                    jobToView.EarnedRevenue = jobOrder.EarnedRevenue;
                    jobToView.UnearnedRevenue = jobOrder.UnearnedRevenue;
                    jobToView.StartDate = jobOrder.StartDate;
                    jobToView.EndDate = jobOrder.EndDate;
                    jobToView.JobOrdernotes = jobOrder.JobOrdernotes;
                    jobToView.CustomerId = customer.CustomerId;

                    //third add the employee data
                    jobToView.EmployeeId = jobOrder.EmployeeId;
                    jobToView.EmployeeName = employee.EmployeeName;

                    jobOrderCustomerViewModelsList.Add(jobToView);

                }

            }
            return jobOrderCustomerViewModelsList;
        }

    }
}
