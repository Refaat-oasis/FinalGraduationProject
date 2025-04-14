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
                            return RedirectToAction("AdminHome", "employee");

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

        [HttpGet]
        public IActionResult UnauthorizedAccess()
        {
            HttpContext.Session.Clear();
            return View("~/Views/SharedViews/UnAutorizedAccess.cshtml");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage", "Employee");
        }

        [HttpGet]
        public IActionResult AdminHome()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0)
            {

                return View("~/Views/Admin/AdminHome.cshtml");
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }

        [HttpGet]
        public IActionResult inventoryClerk()
        {

            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 2)
            {
                return View("~/views/inventoryClerk/inventoryClerkHome.cshtml");

            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }

        }

        [HttpGet]
        public IActionResult inventoryManager()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 1)
            {
                return View("~/views/inventory/inventoryManagerHome.cshtml");

            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }

        [HttpGet]
        public IActionResult technicalClerk()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 4)
            {
                return View("~/views/technicalClerk/technicalClerkHome.cshtml");
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }

        [HttpGet]
        public IActionResult technicalManager()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 3)
            {
                return View("~/views/technical/technicalManagerHome.cshtml");

            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }

        [HttpGet]
        public IActionResult costClerk()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 6)
            {
                return View("~/views/costClerk/costClerkHome.cshtml");

            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }

        [HttpGet]
        public IActionResult costManager()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 5)
            {
                return View("~/views/cost/costManagerHome.cshtml");
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }

        [HttpGet]
        public IActionResult AccountingManager()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 6)
            {
                return View("~/views/Accounting/AccountingManagerHome.cshtml");
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }

        [HttpGet]
        public IActionResult AccountingClerk()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 7)
            {
                return View("~/views/AccountingClerk/AccountingClerkHome.cshtml");
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }



    }
}
