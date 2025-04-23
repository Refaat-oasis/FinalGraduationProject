using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
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
            // Hash the username to match stored version
         

            // Try to find the employee by hashed username only
            Employee loggedEmployee = context.Employees.FirstOrDefault(
                e => e.EmployeeUserName == EmployeeUserName);

            if (loggedEmployee != null)
            {
                // Verify password
                bool passwordMatch = Hashing.VerifyPassword(EmployeePassword, loggedEmployee.EmployeePassword);

                if (!passwordMatch)
                {
                    TempData["Error"] = "اسم المستخدم أو كلمة المرور غير صحيحة";
                    return RedirectToAction("LoginPage", "Employee");
                }

                if (!loggedEmployee.Activated)
                {
                    TempData["Error"] = "هذا الحساب غير مفعل بعد، يرجى التواصل مع مدير النظام لتفعيل الحساب";
                    return RedirectToAction("LoginPage", "Employee");
                }

                // Set session
                HttpContext.Session.SetString("EmployeeID", loggedEmployee.EmployeeId.ToString());
                HttpContext.Session.SetString("EmployeeName", loggedEmployee.EmployeeName);
                HttpContext.Session.SetString("EmployeeUserName", EmployeeUserName); // use original not hashed
                HttpContext.Session.SetInt32("JobRole", (int)loggedEmployee.JobRole);

                // Redirect based on job role
                switch (loggedEmployee.JobRole)
                {
                    case JobRole.Admin:
                        return RedirectToAction("AdminHome", "employee");
                    case JobRole.InventoryClerk:
                        return RedirectToAction("inventoryClerk", "employee");
                    case JobRole.InventoryManager:
                        return RedirectToAction("inventoryManager", "employee");
                    case JobRole.TechnicalClerk:
                        return RedirectToAction("technicalClerk");
                    case JobRole.TechnicalManager:
                        return RedirectToAction("TechnicalManager", "employee");
                    case JobRole.CostClerk:
                        return RedirectToAction("costClerk", "employee");
                    case JobRole.CostManager:
                        return RedirectToAction("CostManager", "employee");
                    case JobRole.AcoountingManager:
                        return RedirectToAction("AccountingManager", "employee");
                    case JobRole.AccountingClerk:
                        return RedirectToAction("AccountingClerk", "employee");
                    default:
                        TempData["Error"] = "هناك خظأ في البيانات المستخدمة";
                        return RedirectToAction("LoginPage", "Employee");
                }
            }
            else
            {
                TempData["Error"] = "اسم المستخدم أو كلمة المرور غير صحيحة";
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
        public IActionResult AccountingManager()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 7)
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
            if (jobRole == 8)
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
