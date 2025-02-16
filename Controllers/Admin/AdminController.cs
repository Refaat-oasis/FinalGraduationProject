using Microsoft.AspNetCore.Mvc;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly AdminBusinessLogicLayer _businessLogicL;

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
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee Data is required");
            }
            await _businessLogicL.AddEmployee(employee);
            return View("~/Views/Admin/ViewAllEmployee.cshtml");

        }
    }
}
