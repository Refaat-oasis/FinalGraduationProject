using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Threading.Tasks;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.Utilities;

namespace ThothSystemVersion1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ThothContext _context;
        private readonly AdminBusinessLogicLayer _businessLogicL;

        public EmployeeController( AdminBusinessLogicLayer admin , ThothContext th) {
            _context = th;
            _businessLogicL = admin;


        }

    
        [HttpGet]
        public IActionResult LoginPage()
        {
            return View("~/Views/SharedViews/login.cshtml", new Employee());
        }

        [HttpPost]
        public IActionResult EmployeeLogin(string EmployeeUserName, string EmployeePassword)
        {
         

            // Try to find the employee by hashed username only
            Employee loggedEmployee = _context.Employees.FirstOrDefault(
                e => e.EmployeeUserName == EmployeeUserName);

            if (loggedEmployee != null && loggedEmployee.Forgetpassword == true) { 
            
                return RedirectToAction("Forgetpassword", "Employee", routeValues: new { loggedEmployee.EmployeeId });

            }else if(loggedEmployee != null)
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
            else {
                TempData["Error"] = "اسم المستخدم أو كلمة المرور غير صحيحة";
                return RedirectToAction("LoginPage", "Employee");
            }
        }

        [HttpGet]
        public IActionResult EmployeeProfile(string employeeId) {

            Employee emp = _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
                EmployeeDTO employeeDTO = new EmployeeDTO();
            if (emp != null)
            {
                employeeDTO.EmployeeId = emp.EmployeeId;
                employeeDTO.EmployeeName = emp.EmployeeName;
                employeeDTO.EmployeeUserName = emp.EmployeeUserName;
                employeeDTO.JobRole = emp.JobRole;
                employeeDTO.Activated = emp.Activated;
                employeeDTO.EmployeePassword = emp.EmployeePassword;
            }
            else
            {
                TempData["Error"] = "رقم الهوية غير صحيح.";
                return RedirectToAction("LoginPage", "Employee");
            }


            return View("~/Views/SharedViews/EmployeeProfile.cshtml", employeeDTO);

        }

        [HttpPost]
        public IActionResult EditEmployee(string EmployeeId, EmployeeDTO updatedEmployee)
        {
            try
            {
                
                Employee existingEmployee = _context.Employees.Find(EmployeeId);
                Employee searchedUsername = _context.Employees.FirstOrDefault(e => e.EmployeeUserName == updatedEmployee.EmployeeUserName);
                if ( searchedUsername != null  && searchedUsername.EmployeeId != existingEmployee.EmployeeId) {

                    TempData["Error"] = "اسم المستخدم موجود مسبقاً";
                    return RedirectToAction("EmployeeProfile", "Employee", new { EmployeeId });

                }
                else {

                    if (updatedEmployee.EmployeePassword != null ) {

                        string password = updatedEmployee.EmployeePassword;
                        string newPassword = Hashing.HashPassword(password);
                        existingEmployee.EmployeePassword = newPassword;

                    }

                    existingEmployee.EmployeeUserName = updatedEmployee.EmployeeUserName;
                    existingEmployee.EmployeeName = updatedEmployee.EmployeeName;
                    _context.Employees.Update(existingEmployee);
                    _context.SaveChanges();
                    TempData["Success"] = "تم تعديل بيانات الموظف بنجاح";
                    return RedirectToAction("EmployeeProfile", "Employee", new { EmployeeId });

                }        
                
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات الموظف";
                // Log the exception (ex) here
                // In both redirects within EditEmployee:
                return RedirectToAction("EmployeeProfile", "Employee", new { employeeId = EmployeeId });
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
        public IActionResult ForgetPassword(string EmployeeId)
        {
            Employee employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == EmployeeId);
            return View("~/Views/SharedViews/ForgetPassword.cshtml", employee);
        }

        [HttpPost]
        public IActionResult forgetPassword(string EmployeeId) {

            Employee employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == EmployeeId);
            if (employee != null)
            {
                employee.Forgetpassword = false;
                _context.Update(employee);
                _context.SaveChanges();
                string id = EmployeeId;
                TempData["Success"] = "تم إعادة تعيين كلمة المرور.";
                return RedirectToAction("EditEmployee", "admin" , routeValues: new { id = EmployeeId });
            }
            else
            {
                TempData["Error"] = "رقم الهوية غير صحيح.";
                return RedirectToAction("EditEmployee", "admin");
            }


        }
        [HttpPost]
        public IActionResult RegisterNewPassword(string employeeId ,Employee emp) 
        {
            Employee employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee != null)
            {
                
                string pass = emp.EmployeePassword;
                string newHashedPassword = Hashing.HashPassword(pass);
                employee.Forgetpassword = false;
                employee.EmployeePassword = newHashedPassword;
                //employee.EmployeePassword
                _context.Update(employee);
                _context.SaveChanges();
                TempData["Success"] = "تم إعادة تعيين كلمة المرور.";
                return RedirectToAction("forgetPassword", "Employee", new { employee.EmployeeId });
            }
            else
            {
                TempData["Error"] = "رقم الهوية غير صحيح.";
                return RedirectToAction("forgetPassword", "Employee",new { employee.EmployeeId });
            }


        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // pages for each job role

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
