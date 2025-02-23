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

        //public IActionResult EmployeeLogin(string EmployeeUserName, string EmployeePassword)
        //{
        //    // Find the employee with the provided username and password
        //    Employee logedEmployee = context.Employees.FirstOrDefault(e =>
        //        e.EmployeeUserName == EmployeeUserName && e.EmployeePassword == EmployeePassword);

        //    if (logedEmployee == null)
        //    {
        //        // If no employee is found, redirect to the login page
        //        return RedirectToAction("LoginPage", "Employee");
        //        Session["EmployeeID"]=logedEmployee.EmployeeId.ToString();
        //        Session["EmployeeName"] = logedEmployee.EmployeeName.ToString();
        //        Session["EmployeeUserName"] = logedEmployee.EmployeeUserName.ToString();
        //    }
        //    else
        //    {
        //        // Redirect based on the employee's job role
        //        switch (logedEmployee.JobRole)
        //        {
        //            case JobRole.Admin: // Admin
        //                //return View("~/Views/Admin/AdminHome.cshtml");
        //                return RedirectToAction("adminhome", "admin");
        //            case JobRole.Inventory: // Inventory
        //                return RedirectToAction("LoginPage", "Employee");
        //            case JobRole.Technical: // Technical
        //                return RedirectToAction("LoginPage", "Employee");
        //            case JobRole.Cost: // Cost
        //                return RedirectToAction("LoginPage", "Employee");
        //            default:
        //                // Handle unexpected roles
        //                return RedirectToAction("LoginPage", "Employee");
        //        }

        //    }
        //}
        public IActionResult EmployeeLogin(string EmployeeUserName, string EmployeePassword)
        {
            // Find the employee with the provided username and password
            Employee logedEmployee = context.Employees.FirstOrDefault(e =>
                e.EmployeeUserName == EmployeeUserName && e.EmployeePassword == EmployeePassword);

            if (logedEmployee != null)
            {
                // Set session variables
                HttpContext.Session.SetString("EmployeeID", logedEmployee.EmployeeId.ToString());
                HttpContext.Session.SetString("EmployeeName", logedEmployee.EmployeeName.ToString());
                HttpContext.Session.SetString("EmployeeUserName", logedEmployee.EmployeeUserName.ToString());

                // Redirect based on the employee's job role
                switch (logedEmployee.JobRole)
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
                        // Handle unexpected roles
                        return RedirectToAction("LoginPage", "Employee");
                }
            }
            else
            {
                // If no employee is found, redirect to the login page
                return RedirectToAction("LoginPage", "Employee");
            }
        }


    }
}
