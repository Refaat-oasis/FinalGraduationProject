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
    }
}

