using ThothSystemVersion1.Models;
using ThothSystemVersion1.ViewModels;

namespace ThothSystemVersion1.InterfaceServices
{
    public interface AdminService
    {
   
        public bool AddEmployee(Employee emp);
        public Employee GetEmployeeById(string id);
        public List<Employee> ViewAllEmployee();
        public List<JobOrderCustEmpVM> ViewAllJobOrder();
        public bool EditEmployee(String id,Employee emp);

    }
}
