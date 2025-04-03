
using Microsoft.AspNetCore.Mvc;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.ViewModels;


namespace ThothSystemVersion1.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminBusinessLogicLayer _businessLogicL;

        public AdminController( AdminBusinessLogicLayer businessLogicL)
        {

            _businessLogicL = businessLogicL;
        }

        [HttpGet]
        public IActionResult AdminHome()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 )
            {

            return View("~/Views/Admin/AdminHome.cshtml");
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }

        }


        [HttpGet]
        public IActionResult AddEmployee()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0)
            {

            return View("~/Views/Admin/AddEmployee.cshtml");
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }


        [HttpPost]
        public IActionResult AddEmployee(EmployeeDTO employee)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/AddEmployee.cshtml");
            }

            bool isEmployeeAdded = _businessLogicL.AddEmployee(employee);

            if (!isEmployeeAdded)
            {
                ModelState.AddModelError("", "اسم المستخدم او الرقم القومي تم استخدامه من قبل");
                return View(employee); // Return the view with the error message
            }

            return RedirectToAction("ViewAllEmployee", "admin");
        }

        [HttpGet]
        public IActionResult ViewAllEmployee()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0)
            {

            List<Employee> employeeList = _businessLogicL.ViewAllEmployee();
            return View("~/Views/Admin/ViewAllEmployee.cshtml", employeeList);
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }

        }

        [HttpGet]
        public IActionResult EditEmployee(string id)
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0)
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
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }

        }

        [HttpPost]
        public IActionResult EditEmployee(string id, EmployeeDTO updatedEmployee)
        {
            if (updatedEmployee == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                bool isEditSuccess = _businessLogicL.EditEmployee(id, updatedEmployee); // Update the employee


                if (!isEditSuccess)
                {
                    ModelState.AddModelError("", "اسم المستخدم او الرقم القومي تم استخدامه من قبل");
                    return View(updatedEmployee);
                }
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

    }
}
