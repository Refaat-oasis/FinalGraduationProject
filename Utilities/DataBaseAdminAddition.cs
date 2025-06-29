using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.Utilities
{
    public static class DataBaseAdminAddition
    {

        public static void Initialize(ThothContext context)
        {
            // Ensure DB is created (optional for DB-First)
            context.Database.EnsureCreated();

            // Check if an employee already exists
            if (context.Employees.Any()) return;

            // Add a default employee
            var employee = new Employee
            {
                EmployeeId = "ADMIN",
                EmployeeUserName = "admin",
                EmployeePassword = "5sqxH4KfcW9zCLTad25FHQk5X+OTyjOdy5D3gGnYzx92RUjH4+Y9jFvhZbESSpil", // Replace with a real hashed password
                EmployeeName = "admin",
                JobRole = JobRole.Admin, // Replace with appropriate enum or int value
                Activated = true,
                Forgetpassword = false
            };

            context.Employees.Add(employee);
            context.SaveChanges();
        }

    }
}
