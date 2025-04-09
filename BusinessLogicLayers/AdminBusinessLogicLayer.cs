using System.Collections.Generic;
using System.Threading.Tasks;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.ViewModels;
using ThothSystemVersion1.DataTransfereObject;
namespace ThothSystemVersion1.BusinessLogicLayers
{ 
     public class AdminBusinessLogicLayer : AdminService
    {
        private readonly ThothContext _context;

        public AdminBusinessLogicLayer(ThothContext context)
        {
            _context = context;
        }

        public List<Employee> ViewAllEmployee() {

            List<Employee> employeesList = _context.Employees.ToList();
            //return View("~/Views/Admin/ViewAllEmployee.cshtml", employeesList);
            return employeesList;

        }

        public bool AddEmployee(EmployeeDTO employee)
        {
            Employee foundEmployeeById = _context.Employees.Find(employee.EmployeeId);
            Employee foundEmployeeByUsername = _context.Employees.FirstOrDefault(e => e.EmployeeUserName == employee.EmployeeUserName);

            if (foundEmployeeById != null || foundEmployeeByUsername != null)
            {
                // Employee with the same ID or username already exists
                return false;
            }

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            Employee addedOne = new Employee();
            addedOne.EmployeeName = employee.EmployeeName;
            addedOne.EmployeeUserName = employee.EmployeeUserName;
            addedOne.EmployeePassword = employee.EmployeePassword;
            addedOne.EmployeeId = employee.EmployeeId;
            addedOne.JobRole = employee.JobRole;
            addedOne.Activated = true;

            _context.Employees.Add(addedOne);
            _context.SaveChanges();

            return true;

        }

        public Employee GetEmployeeById(string id)
        {
            try
            {
                Employee employee = _context.Employees.Find(id); // Synchronous Find
                if (employee == null)
                {
                    throw new ArgumentException("Employee not found.");
                }
                return employee;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new ApplicationException("An error occurred while fetching the employee.", ex);
            }
        }

        public (bool Success, string Message) EditEmployee(string id, EmployeeDTO updatedEmployee)
        {
            try
            {
                if (updatedEmployee == null)
                {
                    return (false, "بيانات الموظف المطلوبة غير متوفرة.");
                }
                Employee existingEmployee = _context.Employees.Find(id); // Find the employee by ID
                                                                         //Employee existingEmployeeUserName = _context.Employees.FirstOrDefault(e => e.EmployeeUserName == updatedEmployee.EmployeeUserName);
                if (existingEmployee == null)
                {
                    return (false, "الموظف غير موجود.");
                }
                //if (existingEmployeeUserName != null) {

                //    return false;
                //}

                // Update properties
                //existingEmployee.EmployeeUserName = updatedEmployee.EmployeeUserName;
                existingEmployee.EmployeeName = updatedEmployee.EmployeeName;
                existingEmployee.EmployeePassword = updatedEmployee.EmployeePassword;
                existingEmployee.Activated = updatedEmployee.Activated;
                _context.Employees.Update(existingEmployee); // Mark the entity as modified
                _context.SaveChanges(); // Save changes to the database
                return (true, $"تم تعديل الموظف {updatedEmployee.EmployeeName}"); // Success
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                return (false, $"حدث خطأ: {ex.Message}");
            }
        }

    }
}

