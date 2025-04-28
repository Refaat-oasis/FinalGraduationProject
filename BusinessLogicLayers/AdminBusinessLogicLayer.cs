using System.Collections.Generic;
using System.Threading.Tasks;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.ViewModels;
using ThothSystemVersion1.DataTransfereObject;
using Microsoft.Extensions.Configuration.UserSecrets;
using ThothSystemVersion1.Utilities;
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
            try
            {

                List<Employee> employeesList = _context.Employees.ToList();
                return employeesList;
            }
            catch (Exception ex) {

                WriteException.WriteExceptionToFile(ex);
                return new List<Employee>();
            }

        }
        public bool AddEmployee(EmployeeDTO employee)
        {
            try
            {

                if (employee == null)
                {
                    return false;
                }

                Employee foundEmployeeById = _context.Employees.Find(employee.EmployeeId);

                Employee foundEmployeeByUsername = _context.Employees.FirstOrDefault(e => e.EmployeeUserName == employee.EmployeeUserName);

                if (foundEmployeeById != null || foundEmployeeByUsername != null)
                {
                    return false;
                }

                string hashedPassword = Hashing.HashPassword(employee.EmployeePassword);

                Employee addedOne = new Employee
                {
                    EmployeeName = employee.EmployeeName,
                    EmployeeUserName = employee.EmployeeUserName,
                    EmployeePassword = hashedPassword,
                    EmployeeId = employee.EmployeeId,
                    JobRole = employee.JobRole,
                    Forgetpassword = true,
                    Activated = true
                };

                _context.Employees.Add(addedOne);
                _context.SaveChanges();

                // ✅ Save plain username and password in file (for record or testing)
                string fileName = $"{employee.EmployeeUserName}.txt";
                string filePath = Path.Combine("EmployeeRecords", fileName);
                Directory.CreateDirectory("EmployeeRecords");

                string fileContent = $"الرقم القومي للموظف : {addedOne.EmployeeId}\n" +
                                     $"الاسم: {addedOne.EmployeeName}\n" +
                                     $"اسم المستخدم للبرنامج: {employee.EmployeeUserName}\n" + // original
                                     $"كلمة المرور: {employee.EmployeePassword}\n" + // original
                                     $"الدور الوظيفي المحدد: {addedOne.JobRole}\n";

                File.WriteAllText(filePath, fileContent);

                return true;

            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
            }
        }
        public Employee GetEmployeeById(string id)
        {
            try
            {
                Employee employee = _context.Employees.Find(id); // Synchronous Find
                if (employee == null)
                {
                    return new Employee(); // Return an empty Employee object if not found

                }
                return employee;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return new Employee(); // Return an empty Employee object if an error occurs

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
                Employee existingEmployeeUserName = _context.Employees.FirstOrDefault(e => e.EmployeeUserName == updatedEmployee.EmployeeUserName);
                if (existingEmployeeUserName != null && existingEmployeeUserName.EmployeeId != existingEmployee.EmployeeId)
                {
                    return (false, "اسم المستخدم موجود مسبقاً");
                }                                                                         //Employee existingEmployeeUserName = _context.Employees.FirstOrDefault(e => e.EmployeeUserName == updatedEmployee.EmployeeUserName);
                if (existingEmployee == null)
                {
                    return (false, "الموظف غير موجود.");
                }

                // Update properties
                existingEmployee.EmployeeUserName = updatedEmployee.EmployeeUserName;
                existingEmployee.EmployeeName = updatedEmployee.EmployeeName;
                //existingEmployee.EmployeePassword = updatedEmployee.EmployeePassword;
                existingEmployee.Activated = updatedEmployee.Activated;
                _context.Employees.Update(existingEmployee); // Mark the entity as modified
                _context.SaveChanges(); // Save changes to the database
                return (true, $"  تم تعديل الموظف {updatedEmployee.EmployeeName} بنجاح "); // Success
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);

                // Log the exception (ex) here
                return (false, $"حدث خطأ: {ex.Message}");

            }
        }

    }
}

