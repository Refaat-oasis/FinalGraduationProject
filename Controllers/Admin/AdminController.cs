using Microsoft.AspNetCore.Mvc;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly AdminBusinessLogicLayer _businessLogicL;

        ThothContext Context= new ThothContext();   

        public AdminController(AdminBusinessLogicLayer businessLogicL)
        {
            _businessLogicL = businessLogicL;
        }
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View("~/Views/Admin/AddEmployee.cshtml");
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee Data is required");
            }
             _businessLogicL.AddEmployee(employee);
           
           return RedirectToAction("ViewAllEmployee" , "admin");

        }
        public IActionResult ViewAllEmployee() { 

            List<Employee> employees = Context.Employees.ToList();
            return View("~/Views/Admin/ViewAllEmployee.cshtml", employees);

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
