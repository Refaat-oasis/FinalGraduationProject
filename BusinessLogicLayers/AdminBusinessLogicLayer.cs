using System.Collections.Generic;
using System.Threading.Tasks;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.ViewModels;
namespace ThothSystemVersion1.BusinessLogicLayers
{ 
     public class AdminBusinessLogicLayer : AdminService
    {
        private readonly ThothContext _context;

        public AdminBusinessLogicLayer(ThothContext context)
        {
            _context = context;
        }

        public List<Employee> ViewAllEmployee() {

            List<Employee> employeesList = _context.Employees.ToList();
            //return View("~/Views/Admin/ViewAllEmployee.cshtml", employeesList);
            return employeesList;

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

        //public void AddEmployee(Employee employee)
        //{
        //   Employee foundEmployee = _context.Employees.Find(employee.EmployeeId );
        //    Employee foundEmployee2 = _context.Employees.Find(employee.EmployeeUserName);
        //    if (foundEmployee == null & foundEmployee2 == null)
        //    {

        //        if (employee == null)
        //        {
        //            throw new ArgumentNullException(nameof(employee));
        //        }
        //        _context.Employees.Add(employee);
        //        _context.SaveChanges();

        //    }
        //    else { 

        //    }

        //}
        public bool AddEmployee(Employee employee)
        {
            Employee foundEmployeeById = _context.Employees.Find(employee.EmployeeId);
            Employee foundEmployeeByUsername = _context.Employees.FirstOrDefault(e => e.EmployeeUserName == employee.EmployeeUserName);

            if (foundEmployeeById != null || foundEmployeeByUsername != null)
            {
                // Employee with the same ID or username already exists
                return false;
            }

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return true;
        }

        public Employee GetEmployeeById(string id)
        {
            try
            {
                Employee employee = _context.Employees.Find(id); // Synchronous Find
                if (employee == null)
                {
                    throw new ArgumentException("Employee not found.");
                }
                return employee;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new ApplicationException("An error occurred while fetching the employee.", ex);
            }
        }

        public bool EditEmployee(string id, Employee updatedEmployee)
        {
            try
            {
                Employee existingEmployee = _context.Employees.Find(id); // Find the employee by ID
                Employee existingEmployeeUserName = _context.Employees.FirstOrDefault(e => e.EmployeeUserName == updatedEmployee.EmployeeUserName);
                if (existingEmployee == null)
                {
                    throw new ArgumentException("Employee not found."); // Employee not found
                }
                if (existingEmployeeUserName != null) {

                    return false;
                }

                // Update properties
                existingEmployee.EmployeeUserName = updatedEmployee.EmployeeUserName;
                existingEmployee.EmployeeName = updatedEmployee.EmployeeName;
                existingEmployee.EmployeePassword = updatedEmployee.EmployeePassword;
                existingEmployee.Activated = updatedEmployee.Activated;
                _context.Employees.Update(existingEmployee); // Mark the entity as modified
                _context.SaveChanges(); // Save changes to the database
                return true; // Success
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new ApplicationException("An error occurred while updating the employee.", ex);
            }
        }
        
    }
}

