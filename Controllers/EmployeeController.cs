using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
                            return RedirectToAction("inventoryClerk","employee");
                        case JobRole.InventoryManager: // Inventory Manager
                            return RedirectToAction("inventoryManager", "employee");

                        case JobRole.TechnicalClerk: // Technical
                            return RedirectToAction("technicalClerk");
                        case JobRole.TechnicalManager: // Technical Manager
                            return RedirectToAction("TechnicalManager", "employee");

                        case JobRole.CostClerk: // Cost
                            return RedirectToAction("costClerk", "employee");
                        case JobRole.CostManager:
                            return RedirectToAction("CostManager", "employee");

                        default:
                            return RedirectToAction("LoginPage", "Employee");
                    }
                }else{
                    string message = "هذا الحساب غير مفعل بعد، يرجى التواصل مع مدير النظام لتفعيل الحساب";
                    TempData["Error"] = message;
                    return RedirectToAction("LoginPage", "Employee");
                }

            }else{
                string message = "اسم المستخدم أو كلمة المرور غير صحيحة";
                TempData["Error"] = message;
                return RedirectToAction("LoginPage", "Employee");
            }
        }

        public IActionResult UnauthorizedAccess()
        {
            return View("~/Views/SharedViews/UnAutorizedAccess.cshtml");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage", "Employee");
        }
        public IActionResult inventoryClerk() {

            return View("~/views/inventoryClerck/inventoryClerkHome.cshtml");
        }
        public IActionResult inventoryManager() {

            return View("~/views/inventory/inventoryManagerHome.cshtml");
        }
        public IActionResult technicalClerk() {

            return View("~/views/technicalClerk/technicalClerkHome.cshtml");
        }
        public IActionResult technicalManager() {

            return View("~/views/technicalManager/technicalManagerHome.cshtml");
        }
        public IActionResult costClerk() {

            return View("~/views/costClerk/costClerkHome.cshtml");
        }
        public IActionResult costManager() {

            return View("~/views/costManager/costManagerHome.cshtml");
        }



    }
}
