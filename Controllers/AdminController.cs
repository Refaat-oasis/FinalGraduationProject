using AutoMapper;
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
        private readonly IMapper _mapper;

        ThothContext Context = new ThothContext();

        public AdminController(IMapper mapper, AdminBusinessLogicLayer businessLogicL)
        {
            _mapper = mapper;
            _businessLogicL = businessLogicL;
        }

        [HttpGet]
        public IActionResult AdminHome()
        {
            return View("~/Views/Admin/AdminHome.cshtml");

        }


        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View("~/Views/Admin/AddEmployee.cshtml");
        }


        //[HttpPost]
        //public IActionResult AddEmployee(Employee employee)
        //{
        //    if (employee == null)
        //    {
        //        //return BadRequest("Employee Data is required");
        //        return RedirectToAction("addemployee", "admin", employee);
        //    }
        //    _businessLogicL.AddEmployee(employee);

        //    return RedirectToAction("ViewAllEmployee", "admin");

        //}
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                return RedirectToAction("addemployee", "admin", employee);
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
            List<Employee> employeeList = _businessLogicL.ViewAllEmployee();
            return View("~/Views/Admin/ViewAllEmployee.cshtml", employeeList);

        }


        [HttpGet]
        public IActionResult ViewAllJobOrder()
        {
            List<JobOrderCustEmpVM> jobOrderCustomerViewModelsList = _businessLogicL.ViewAllJobOrder();
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
                bool isEditSuccess = _businessLogicL.EditEmployee(id, updatedEmployee); // Update the employee
               

                if (!isEditSuccess) {
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
