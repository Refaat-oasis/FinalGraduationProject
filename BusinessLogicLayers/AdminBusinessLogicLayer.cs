using System.Collections.Generic;
using System.Threading.Tasks;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.Database;
    namespace ThothSystemVersion1.BusinessLogicLayers
{ 
     public class AdminBusinessLogicLayer
    {
        private readonly ThothContext _context;

        public AdminBusinessLogicLayer(ThothContext context)
        {
            _context = context;
        }

        public void AddEmployee(Employee employee)
        {
            if(employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
             _context.Employees.Add(employee);
            _context.SaveChanges();
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

        // Update Employee (Synchronous)
        public Employee EditEmployee(string id, Employee updatedEmployee)
        {
            try
            {
                Employee existingEmployee = _context.Employees.Find(id); // Find the employee by ID
                if (existingEmployee == null)
                {
                    throw new ArgumentException("Employee not found."); // Employee not found
                }

                // Update properties
                existingEmployee.EmployeeUserName = updatedEmployee.EmployeeUserName;
                existingEmployee.EmployeeName = updatedEmployee.EmployeeName;
                existingEmployee.EmployeePassword = updatedEmployee.EmployeePassword;

                _context.Employees.Update(existingEmployee); // Mark the entity as modified
                _context.SaveChanges(); // Save changes to the database
                return existingEmployee; // Success
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new ApplicationException("An error occurred while updating the employee.", ex);
            }
        }
        public void DeleteEmployee(string id)
        {
            Employee employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                throw new ArgumentNullException("Employee not found");
            }
            _context.Remove(employee);
            _context.SaveChanges();

        }
    }
}

