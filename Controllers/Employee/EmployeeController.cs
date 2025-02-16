using Microsoft.AspNetCore.Mvc;

namespace ThothSystemVersion1.Controllers.Employee
{
    public class EmployeeController : Controller
    {
        public IActionResult Index() { 
            
            return View("~/Views/SharedViews/login.cshtml");
        }





    }
}
