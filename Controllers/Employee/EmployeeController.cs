using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeBusinessLogicL _businessLogicL;

        public EmployeeController(EmployeeBusinessLogicL businessLogicL)
        {
            _businessLogicL = businessLogicL;
        }
        public IActionResult Index()
        {

            return View("~/Views/SharedViews/login.cshtml");
        }
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View("~/Views/Admin/AddEmployee.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if(employee == null)
            {
                return BadRequest("Employee Data is required");
            }
            await _businessLogicL.AddEmployee(employee);
            return View("~/Views/Admin/ViewAllEmployee.cshtml");

        }




    }
}
