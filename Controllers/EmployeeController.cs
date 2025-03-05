using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.Controllers
{
    public class EmployeeController : Controller
    {

        ThothContext context = new ThothContext();
        public IActionResult LoginPage()
        {
            return View("~/Views/SharedViews/login.cshtml", new Employee());
        }
        public IActionResult EmployeeLogin(string EmployeeUserName, string EmployeePassword)
        {
            
            Employee loggedEmployee = context.Employees.FirstOrDefault(
                e =>e.EmployeeUserName == EmployeeUserName && e.EmployeePassword == EmployeePassword);

            if (loggedEmployee != null)
            {
                if (loggedEmployee.Activated)
                {
                    HttpContext.Session.SetString("EmployeeID", loggedEmployee.EmployeeId.ToString());
                    HttpContext.Session.SetString("EmployeeName", loggedEmployee.EmployeeName.ToString());
                    HttpContext.Session.SetString("EmployeeUserName", loggedEmployee.EmployeeUserName.ToString());

                    switch (loggedEmployee.JobRole)
                    {
                        case JobRole.Admin: // Admin
                            return RedirectToAction("AdminHome", "Admin");
                        case JobRole.Inventory: // Inventory
                            return RedirectToAction("InventoryHome", "Inventory");
                        case JobRole.Technical: // Technical
                            return RedirectToAction("TechnicalHome", "Technical");
                        case JobRole.Cost: // Cost
                            return RedirectToAction("CostHome", "Cost");
                        default:
                            return RedirectToAction("LoginPage", "Employee");
                    }
                }
                else
                {
                    return RedirectToAction("LoginPage", "Employee");
                }

            }
            else {
                return RedirectToAction("LoginPage", "Employee");
            }
        }


    }
}
