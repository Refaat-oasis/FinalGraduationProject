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
                    HttpContext.Session.SetInt32("JobRole", (int)loggedEmployee.JobRole);

                    switch (loggedEmployee.JobRole)
                    {
                        case JobRole.Admin: // Admin
                            return RedirectToAction("AdminHome", "Admin");

                        case JobRole.InventoryClerk: // Inventory
                            return RedirectToAction("InventoryHome", "Inventory");
                        case JobRole.InventoryManager: // Inventory Manager
                            return RedirectToAction("InventoryManagerHome", "Inventory");

                        case JobRole.TechnicalClerk: // Technical
                            return RedirectToAction("TechnicalHome", "Technical");
                        case JobRole.TechnicalManager: // Technical Manager
                            return RedirectToAction("TechnicalManagerHome", "Technical");

                        case JobRole.CostClerk: // Cost
                            return RedirectToAction("CostHome", "Cost");
                        case JobRole.CostManager:
                            return RedirectToAction("CostManagerHome", "Cost");

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

             
                    
                string message = "اسم المستخدم أو كلمة المرور غير صحيحة";

                TempData["Error"] = message;
                return RedirectToAction("LoginPage", "Employee");
            }
        }

        public IActionResult UnauthorizedAccess()
        {
            return View("~/Views/SharedViews/UnAutorizedAccess.cshtml");
        }

    }
}
