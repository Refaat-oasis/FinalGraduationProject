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
            try
            {
                return View("~/Views/SharedViews/login.cshtml", new Employee());
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/SharedViews/login.cshtml", new Employee());
            }
        }

        [HttpPost]
        public IActionResult EmployeeLogin(string EmployeeUserName, string EmployeePassword)
        {
            try
            {
                // Try to find the employee by hashed username only
                Employee loggedEmployee = _context.Employees.FirstOrDefault(
                    e => e.EmployeeUserName == EmployeeUserName);

                if (loggedEmployee != null && loggedEmployee.Forgetpassword == true)
                {

                    return RedirectToAction("Forgetpassword", "Employee", routeValues: new { loggedEmployee.EmployeeId });

                }
                else if (loggedEmployee != null)
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
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/SharedViews/Login.cshtml", new Employee());
            }
        }

        [HttpGet]
        public IActionResult EmployeeProfile(string employeeId) {
            try
            {

                Employee emp = _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);

                if (emp != null)
                {
                    return View("~/Views/SharedViews/EmployeeProfile.cshtml", emp);
                }
                else
                {
                    TempData["Error"] = "رقم الهوية غير صحيح.";
                    return RedirectToAction("LoginPage", "Employee");
                }


            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/SharedViews/EmployeeProfile.cshtml", new Employee());
            }

        }

        [HttpPost]
        public IActionResult EditEmployee(string EmployeeId, Employee updatedEmployee)
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

                    if (updatedEmployee.EmployeePassword != null ) 
                    {
                        string password = updatedEmployee.EmployeePassword;
                        string newPassword = Hashing.HashPassword(password);
                        existingEmployee.EmployeePassword = newPassword;

                    }

                    if (ModelState.IsValid)
                    {
                        existingEmployee.EmployeeUserName = updatedEmployee.EmployeeUserName;
                        existingEmployee.EmployeeName = updatedEmployee.EmployeeName;
                        _context.Employees.Update(existingEmployee);
                        _context.SaveChanges();
                        TempData["Success"] = "تم تعديل بيانات الموظف بنجاح";
                        return RedirectToAction("EmployeeProfile", "Employee", new { EmployeeId });

                    }
                    else {
                        return RedirectToAction("EmployeeProfile", "Employee", new { EmployeeId });
                    }

                    

                }        
                
            }
          catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات الموظف";
                
                return RedirectToAction("EmployeeProfile", "Employee", new { employeeId = EmployeeId });
            }



        }

        [HttpGet]
        public IActionResult UnauthorizedAccess()
        {
            try
            {
                HttpContext.Session.Clear();
                return View("~/Views/SharedViews/UnAutorizedAccess.cshtml");
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/SharedViews/UnAutorizedAccess.cshtml");
            }

        }

        [HttpGet]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                return RedirectToAction("LoginPage", "Employee");
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/SharedViews/Login.cshtml");
            }
        }

        [HttpGet]
        public IActionResult ForgetPassword(string EmployeeId)
        {
            try
            {
                Employee employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == EmployeeId);
                return View("~/Views/SharedViews/ForgetPassword.cshtml", employee);
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/SharedViews/ForgetPassword.cshtml", new Employee());
            }
        }

        [HttpPost]
        public IActionResult forgetPassword(string EmployeeId) {
            try
            {

                Employee employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == EmployeeId);
                if (employee != null)
                {
                    employee.Forgetpassword = true;
                    _context.Update(employee);
                    _context.SaveChanges();
                    string id = EmployeeId;
                    TempData["Success"] = "تم إعادة تعيين كلمة المرور.";
                    return RedirectToAction("EditEmployee", "admin", routeValues: new { id = EmployeeId });
                }
                else
                {
                    TempData["Error"] = "رقم الهوية غير صحيح.";
                    return RedirectToAction("EditEmployee", "admin");
                }
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات الموظف";
                return View("~/Views/Admin/EditEmployee.cshtml", new Employee());

            }


        }
        [HttpPost]
        public IActionResult RegisterNewPassword(string employeeId ,Employee emp) 
        {
            try
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
                    return RedirectToAction("forgetPassword", "Employee", new { employee.EmployeeId });
                }
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات الموظف";
                return View("~/Views/SharedViews/forgetPassword.cshtml", new Employee());

            }

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // pages for each job role

        [HttpGet]
        public IActionResult AdminHome()
        {
            try
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
                    
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/SharedViews/UnAutorizedAccess.cshtml",new Employee());
}
}

        [HttpGet]
        public IActionResult inventoryManager()
        {
            try
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
                 
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/SharedViews/UnAutorizedAccess.cshtml",new Employee());
}
}

        [HttpGet]
        public IActionResult inventoryClerk()
        {
            try
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
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/SharedViews/UnAutorizedAccess.cshtml",new Employee());
}

}

        [HttpGet]
        public IActionResult technicalManager()
        {
            try
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
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/SharedViews/UnAutorizedAccess.cshtml", new Employee());
            }
        }

        [HttpGet]
        public IActionResult technicalClerk()
        {
            try
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

      catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/SharedViews/UnAutorizedAccess.cshtml", new Employee());
            }
        }

        [HttpGet]
        public IActionResult costManager()
        {
            try
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
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/SharedViews/UnAutorizedAccess.cshtml", new Employee());
            }
        }

        [HttpGet]
        public IActionResult costClerk()
        {
            try
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
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/SharedViews/UnAutorizedAccess.cshtml", new Employee());
            }
        }

        [HttpGet]
        public IActionResult AccountingManager()
        {
            try
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
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/SharedViews/UnAutorizedAccess.cshtml", new Employee());
            }
        }

        [HttpGet]
        public IActionResult AccountingClerk()
        {
            try
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
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                TempData["Error"] = "حدث خطأ غير متوقع، يرجى المحاولة لاحقًا.";
                return View("~/Views/SharedViews/UnAutorizedAccess.cshtml", new Employee());
            }
        }



    }
}
