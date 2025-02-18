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
    }
}
