
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

        // sherwet section

  
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

        // mariam section
        [HttpGet]
        public IActionResult AddEmployee()
        {
            //int? jobRole = HttpContext.Session.GetInt32("JobRole");
            //if (jobRole == 0)
            //{

            return View("~/Views/Admin/AddEmployee.cshtml");
            //}
            //else
            //{

            //    return RedirectToAction("UnauthorizedAccess", "employee");
            //}
        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeeDTO employee)
        {

            if (!ModelState.IsValid)
            {
                //return View("~/Views/Admin/AddEmployee.cshtml");
                return View(employee);
            }

            try
            {

                bool isEmployeeAdded = _businessLogicL.AddEmployee(employee);

                if (!isEmployeeAdded)
                {
                    TempData["Error"]= ("", "اسم المستخدم او الرقم القومي تم استخدامه من قبل");
                    return View(employee); 
                }
                TempData["Success"] = "تم اضافة موظف بنجاح";
                return View(employee);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "حدث خطأ أثناء إضافة الموظف";
                return View(employee); 

            }
        }


        // sandra section 
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
                return BadRequest("بيانات الموظف غير صالحة.");
            }

            try
            {
                var result = _businessLogicL.EditEmployee(id, updatedEmployee); // Update the employee
                if (result.Success)
                {
                    TempData["Success"] = result.Message;
                    //return View("~/Views/Admin/EditEmployee.cshtml", id);
                    return RedirectToAction("EditEmployee", "admin", id);
                }
                else
                {
                    //if (result.Message.Contains("اسم المستخدم"))
                    //{
                    //    TempData["Error"] = result.Message;
                    //    return RedirectToAction("EditEmployee", new { id = id });
                    //}
                    TempData["Error"] = result.Message;
                    return RedirectToAction("EditEmployee", "admin", id);
                }

                //return RedirectToAction("ViewAllEmployee");

            }

            catch (Exception ex)
            {
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات الموظف";
                return View(updatedEmployee);
            }
            //catch (Exception ex)
            //{
            //    return StatusCode(500, ex.Message); // Internal server error
            //}
        }

    }
}
