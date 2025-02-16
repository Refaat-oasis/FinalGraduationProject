using System.Collections.Generic;
using System.Threading.Tasks;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.Database;
    namespace ThothSystemVersion1.BusinessLogicLayers
{ 
     public class EmployeeBusinessLogicL
    {
        private readonly ThothContext _context;

        public EmployeeBusinessLogicL(ThothContext context)
        {
            _context = context;
        }

        public async Task AddEmployee(Employee employee)
        {
            if(employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            await _context.Employees.AddAsync(employee);
            _context.SaveChanges();
        }
    }
}

