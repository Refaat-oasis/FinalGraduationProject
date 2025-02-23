using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.ViewModels;


namespace ThothSystemVersion1.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly AdminBusinessLogicLayer _businessLogicL;
        private readonly IMapper _mapper;

        ThothContext Context = new ThothContext();

        public AdminController(IMapper mapper, AdminBusinessLogicLayer businessLogicL)
        {
            _mapper = mapper;
            _businessLogicL = businessLogicL;
        }
        public IActionResult AdminHome()
        {
            return View("~/Views/Admin/AdminHome.cshtml");

        }
        
        
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View("~/Views/Admin/AddEmployee.cshtml");
        }



        //[HttpPost("add-employee")]
        //public IActionResult AddEmployee([FromBody] EmployeeDTO employeeDTO)
        //{
        //    if (employeeDTO == null)
        //    {
        //        return BadRequest("Employee data is required.");
        //    }

        //    // Validate the DTO
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    // Use AutoMapper to convert DTO to Entity
        //    var employee = _mapper.Map<Employee>(employeeDTO);

        //    // Pass the Entity to the business logic layer
        //    _businessLogicL.AddEmployee(employee);

        //    return RedirectToAction("ViewAllEmployee", "admin");
        //}
        [HttpPost]

        public IActionResult AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee Data is required");
            }
            _businessLogicL.AddEmployee(employee);

            return RedirectToAction("ViewAllEmployee", "admin");

        }
        public IActionResult ViewAllEmployee()
        {

            List<Employee> employeesList = Context.Employees.ToList();
            return View("~/Views/Admin/ViewAllEmployee.cshtml", employeesList);

        }
        public IActionResult ViewAllJobOrder()
        {

            List<JobOrder> jobOrdersList = Context.JobOrders.ToList();
            List<Customer> customersList = Context.Customers.ToList();
            List<Employee> employeeList = Context.Employees.ToList();
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
            return View("~/Views/Admin/ViewAlljobOrder.cshtml", jobOrderCustomerViewModelsList);

        }



        [HttpGet]
        public IActionResult EditEmployee(string id)
        {
            try
            {
                var employee = _businessLogicL.GetEmployeeById(id); // Fetch the employee by ID
                return View("~/Views/Admin/EditEmployee.cshtml", employee);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message); // Internal server error
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); // Employee not found
            }
        }
        [HttpPost]
        public IActionResult EditEmployee(string id, Employee updatedEmployee)
        {
            if (updatedEmployee == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                Employee result = _businessLogicL.EditEmployee(id, updatedEmployee); // Update the employee
                return RedirectToAction("ViewAllEmployee"); // Redirect to the list of employees
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); // Employee not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Internal server error
            }
        }
        public IActionResult DeleteEmployee(string id)
        {
            if (id == null)
            {
                return BadRequest("Invalid employee ID.");
            }
            _businessLogicL.DeleteEmployee(id);
            return RedirectToAction("ViewAllEmployee", "Admin");
        }
    }
}
