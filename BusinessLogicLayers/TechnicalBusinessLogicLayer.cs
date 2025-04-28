using Microsoft.EntityFrameworkCore;
using System.Linq;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.Utilities;
using ThothSystemVersion1.ViewModels;

namespace ThothSystemVersion1.BusinessLogicLayers
{
    public class TechnicalBusinessLogicLayer : TechnicalService
    {
        private readonly ThothContext _context;
        public TechnicalBusinessLogicLayer(ThothContext context)
        {

            _context = context;
        }


        public List<JobOrderCustEmpVM> ViewAllJobOrder()
        {
            try
            {
                List<JobOrder> jobOrdersList = _context.JobOrders.OrderByDescending(j => j.StartDate).ToList();
                List<Customer> customersList = _context.Customers.ToList();
                List<Employee> employeeList = _context.Employees.ToList();
                List<JobOrderCustEmpVM> jobOrderCustomerViewModelsList = new List<JobOrderCustEmpVM>();

                foreach (JobOrder jobOrder in jobOrdersList)
                {
                    Customer customer = customersList.FirstOrDefault(c => c.CustomerId == jobOrder.CustomerId);
                    Employee employee = employeeList.FirstOrDefault(e => e.EmployeeId == jobOrder.EmployeeId);

                    if (customer != null && employee != null)
                    {
                        JobOrderCustEmpVM jobToView = new JobOrderCustEmpVM();

                        // first add the customer data
                        jobToView.CustomerId = customer.CustomerId;
                        jobToView.CustomerAddress = customer.CustomerAddress;
                        jobToView.CustomerPhone = customer.CustomerPhone;
                        jobToView.CustomerName = customer.CustomerName;
                        jobToView.CustomerEmail = customer.CustomerEmail;

                        // second add the job order data
                        jobToView.JobOrderId = jobOrder.JobOrderId;
                        jobToView.OrderProgress = jobOrder.OrderProgress;
                        jobToView.RemainingAmount = jobOrder.RemainingAmount;
                        jobToView.EarnedRevenue = jobOrder.EarnedRevenue;
                        jobToView.UnearnedRevenue = jobOrder.UnearnedRevenue;
                        jobToView.StartDate = jobOrder.StartDate;
                        jobToView.EndDate = jobOrder.EndDate;
                        jobToView.JobOrdernotes = jobOrder.JobOrdernotes;
                        jobToView.CustomerId = customer.CustomerId;

                        // third add the employee data
                        jobToView.EmployeeId = jobOrder.EmployeeId;
                        jobToView.EmployeeName = employee.EmployeeName;

                        jobOrderCustomerViewModelsList.Add(jobToView);
                    }
                }
                return jobOrderCustomerViewModelsList;
            }

            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }


        public List<JobOrder> GetLast10JobOrders()
        {
            try
            {
                return _context.JobOrders
                     .Include(j => j.Customer)
                    .OrderByDescending(j => j.StartDate)
                    .Take(10)
                    .ToList();
            }


            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public List<Paper> GetAvailablePapers()
        {
            try
            {
                return _context.Papers.Where(p => p.Activated).ToList();
            }
    
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public List<Ink> GetAvailableInks()
        {
            try
            {
                return _context.Inks.Where(i => i.Activated).ToList();
            }
 
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public List<Supply> GetAvailableSupplies()
        {
            try
            {
                return _context.Supplies.Where(s => s.Activated).ToList();
            }

            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public (bool success, string message) CreateRequisite(RequisiteOrderDTO requisiteDTO)
        {

            try
            {

                List<QuantityBridge> quantityBridgeList = requisiteDTO.BridgeList;

                RequisiteOrder requisiteOrder = new RequisiteOrder();
                requisiteOrder.EmployeeId = requisiteDTO.EmployeeId;
                requisiteOrder.JobOrderId = requisiteDTO.JobOrderId;
                requisiteOrder.RequisiteNotes = requisiteDTO.RequisiteNotes;
                _context.RequisiteOrders.Add(requisiteOrder);
                _context.SaveChanges();

                int requisiteOrderNumber = _context.RequisiteOrders
                          .OrderByDescending(po => po.RequisiteId)
                          .Select(po => po.RequisiteId)
                          .FirstOrDefault();

                for (int i = 0; i < quantityBridgeList.Count; i++)
                {
                    quantityBridgeList[i].RequisiteId = requisiteOrderNumber;
                    quantityBridgeList[i].QuantityBridgeId = null;

                    if (quantityBridgeList[i].InkId != null)
                    {
                        Ink ink = _context.Inks.FirstOrDefault(p => p.InkId == quantityBridgeList[i].InkId);

                        // Calculate new quantity and average price
                        double newQuantity = ink.Quantity - quantityBridgeList[i].Quantity;
                        decimal totalValue = (decimal)newQuantity * ink.Price;

                        quantityBridgeList[i].OldPrice = ink.Price;
                        quantityBridgeList[i].OldQuantity = ink.Quantity;
                        quantityBridgeList[i].OldTotalBalance = ink.TotalBalance;
                        quantityBridgeList[i].Price = ink.Price;
                        quantityBridgeList[i].TotalBalance = quantityBridgeList[i].Quantity * ink.Price;

                        // Update paper properties
                        ink.Quantity = (int)newQuantity;
                        //ink.Price = averagePrice;
                        ink.TotalBalance = totalValue;

                        _context.Inks.Update(ink);
                        _context.QuantityBridges.Add(quantityBridgeList[i]);
                        _context.SaveChanges();

                    }
                    else if (quantityBridgeList[i].PaperId != null)
                    {
                        Paper paper = _context.Papers.FirstOrDefault(p => p.PaperId == quantityBridgeList[i].PaperId);

                        // Calculate new quantity and average price
                        double newQuantity = paper.Quantity - quantityBridgeList[i].Quantity;
                        decimal totalValue = (decimal)newQuantity * paper.Price;

                        quantityBridgeList[i].OldPrice = paper.Price;
                        quantityBridgeList[i].OldQuantity = paper.Quantity;
                        quantityBridgeList[i].OldTotalBalance = paper.TotalBalance;
                        quantityBridgeList[i].Price = paper.Price;
                        quantityBridgeList[i].TotalBalance = quantityBridgeList[i].Quantity * paper.Price;

                        // Update paper properties
                        paper.Quantity = (int)newQuantity;
                        //paper.Price = averagePrice;
                        paper.TotalBalance = totalValue;
                        _context.Papers.Update(paper);
                        _context.QuantityBridges.Add(quantityBridgeList[i]);
                        _context.SaveChanges();

                    }
                    else if (quantityBridgeList[i].SuppliesId != null)
                    {
                        Supply supply = _context.Supplies.FirstOrDefault(p => p.SuppliesId == quantityBridgeList[i].SuppliesId);

                        // Calculate new quantity and average price
                        double newQuantity = supply.Quantity - quantityBridgeList[i].Quantity;
                        decimal totalValue = (decimal)newQuantity * supply.Price;

                        // update to the old data in the bridge
                        quantityBridgeList[i].OldPrice = supply.Price;
                        quantityBridgeList[i].OldQuantity = supply.Quantity;
                        quantityBridgeList[i].OldTotalBalance = supply.TotalBalance;
                        quantityBridgeList[i].Price = supply.Price;
                        quantityBridgeList[i].TotalBalance = quantityBridgeList[i].Quantity * supply.Price;

                        // Update paper properties
                        supply.Quantity = (int)newQuantity;
                        //supply.Price = averagePrice;
                        supply.TotalBalance = (decimal)totalValue;
                        _context.Supplies.Update(supply);
                        _context.QuantityBridges.Add(quantityBridgeList[i]);
                        _context.SaveChanges();

                    }

                }
                return (true, "تمت انشاء اذن الصرف بنجاح");
            }

            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return (false, $"حدث خطأ: {ex.ToString()}");
            }
        }

        public List<Customer> ViewAllCustomer()
        {
            try
            {
                List<Customer> customerList = _context.Customers.ToList();

                return customerList;
            }

            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }
        public JobOrderSpecificationsViewModel ShowJobOrderSpecifications(int jobOrderId)
        {
            try
            {
                // Initialize with null checks at every level
                JobOrder jobOrder = _context.JobOrders.FirstOrDefault(j => j.JobOrderId == jobOrderId);
                if (jobOrder == null) return null;

                JobOrderSpecificationsViewModel joborderSpecifics = new JobOrderSpecificationsViewModel();

                // Safe mapping of JobOrder properties
                joborderSpecifics.JobOrderId = jobOrder.JobOrderId;
                joborderSpecifics.RemainingAmount = jobOrder.RemainingAmount;
                joborderSpecifics.UnearnedRevenue = jobOrder.UnearnedRevenue;
                joborderSpecifics.JobOrdernotes = jobOrder.JobOrdernotes;
                joborderSpecifics.EarnedRevenue = jobOrder.EarnedRevenue;
                joborderSpecifics.OrderProgress = jobOrder.OrderProgress;
                joborderSpecifics.StartDate = jobOrder.StartDate;
                joborderSpecifics.EndDate = jobOrder.EndDate;
                joborderSpecifics.EmployeeId = jobOrder.EmployeeId;
                joborderSpecifics.CustomerId = jobOrder.CustomerId;

                // Safe handling of related entities
                MiscellaneousExpense miscellaneousExpense = _context.MiscellaneousExpenses.FirstOrDefault(m => m.JobOrderId == jobOrderId);
                RequisiteOrder requisiteOrder = _context.RequisiteOrders.FirstOrDefault(r => r.JobOrderId == jobOrderId);
                ReturnsOrder returnOrder = _context.ReturnsOrders.FirstOrDefault(r => r.JobOrderId == jobOrderId);

                // Safe mapping of MiscellaneousExpense
                if (miscellaneousExpense != null)
                {
                    joborderSpecifics.MiscellaneousExpensesID = miscellaneousExpense.MiscellaneousExpensesId;
                    joborderSpecifics.MaterialProcessingExpense = miscellaneousExpense.MaterialProcessingExpense;
                    // ... map all other properties similarly ...
                }

                // Safe mapping of RequisiteOrder
                if (requisiteOrder != null)
                {
                    joborderSpecifics.RequisiteId = requisiteOrder.RequisiteId;
                    joborderSpecifics.RequisiteDate = requisiteOrder.RequisiteDate;
                    joborderSpecifics.RequisiteNotes = requisiteOrder.RequisiteNotes;
                }

                // Safe mapping of ReturnOrder
                if (returnOrder != null)
                {
                    joborderSpecifics.ReturnId = returnOrder.ReturnId;
                    joborderSpecifics.ReturnDate = returnOrder.ReturnDate;
                    joborderSpecifics.ReturnsNotes = returnOrder.ReturnsNotes;
                    joborderSpecifics.ReturnInOut = returnOrder.ReturnInOut;
                }

                // Safe handling of Customer
                Customer cust = jobOrder.CustomerId.HasValue
                    ? _context.Customers.FirstOrDefault(c => c.CustomerId == jobOrder.CustomerId)
                    : null;

                joborderSpecifics.CustomerName = cust?.CustomerName;

                // Safe handling of Employees
                List<Employee> employees = new List<Employee>();

                if (jobOrder.EmployeeId != null)
                {
                    employees.Add(_context.Employees.FirstOrDefault(e => e.EmployeeId == jobOrder.EmployeeId));
                }

                if (requisiteOrder?.EmployeeId != null)
                {
                    employees.Add(_context.Employees.FirstOrDefault(e => e.EmployeeId == requisiteOrder.EmployeeId));
                }

                if (returnOrder?.EmployeeId != null)
                {
                    employees.Add(_context.Employees.FirstOrDefault(e => e.EmployeeId == returnOrder.EmployeeId));
                }

                if (miscellaneousExpense?.EmployeeId != null)
                {
                    employees.Add(_context.Employees.FirstOrDefault(e => e.EmployeeId == miscellaneousExpense.EmployeeId));
                }

                joborderSpecifics.Employees = employees.Where(e => e != null).ToList();

                // Safe handling of QuantityBridges
                List<QuantityBridge> quantityBridges = new List<QuantityBridge>();

                if (requisiteOrder != null || returnOrder != null)
                {
                    quantityBridges = _context.QuantityBridges
                        .Where(q => (requisiteOrder != null && q.RequisiteId == requisiteOrder.RequisiteId) ||
                                   (returnOrder != null && q.ReturnId == returnOrder.ReturnId))
                        .ToList();
                }

                joborderSpecifics.QuantityBridges = quantityBridges;

                // Safe handling of ProcessBridges
                joborderSpecifics.ProcessBridges = _context.ProcessBridges
                    .Where(p => p.JobOrderId == jobOrderId)
                    .ToList();

                // Safe handling of Materials
                List<Paper> papers = new List<Paper>();
                List<Ink> inks = new List<Ink>();
                List<Supply> supplies = new List<Supply>();
                List<Labour> labours = new List<Labour>();
                List<Machine> machines = new List<Machine>();

                foreach (var QB in quantityBridges)
                {
                    if (QB.InkId != null)
                    {
                        var ink = _context.Inks.FirstOrDefault(i => i.InkId == QB.InkId);
                        if (ink != null) inks.Add(ink);
                    }
                    else if (QB.PaperId != null)
                    {
                        var paper = _context.Papers.FirstOrDefault(p => p.PaperId == QB.PaperId);
                        if (paper != null) papers.Add(paper);
                    }
                    else if (QB.SuppliesId != null)
                    {
                        var supply = _context.Supplies.FirstOrDefault(s => s.SuppliesId == QB.SuppliesId);
                        if (supply != null) supplies.Add(supply);
                    }
                }

                foreach (var pb in joborderSpecifics.ProcessBridges)
                {
                    if (pb.LabourId != null)
                    {
                        var lab = _context.Labours.FirstOrDefault(e => e.LabourId == pb.LabourId);
                        if (lab != null) labours.Add(lab);
                    }

                    if (pb.MachineId != null)
                    {
                        var mach = _context.Machines.FirstOrDefault(i => i.MachineId == pb.MachineId);
                        if (mach != null) machines.Add(mach);
                    }
                }

                // Assign collected materials
                joborderSpecifics.Papers = papers;
                joborderSpecifics.Inks = inks;
                joborderSpecifics.Supplies = supplies;
                joborderSpecifics.Labours = labours;
                joborderSpecifics.Machines = machines;

                return joborderSpecifics;
            }

            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null; 
            }
        }


        public List<Employee> GetAvailableEmployees()
        {
            try
            {

                List<Employee> employeelist = _context.Employees.Where(e => e.Activated).ToList();
                return employeelist;
            }
    
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public List<Customer> GetAvailableCustomerss()
        {
            try
            {

                List<Customer> customerList = _context.Customers.Where(c => c.Activated).ToList();
                return customerList;
            }

            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public (bool success, string message) AddJobOrder(JobOrderDTO jobOrder)
        {

            try
            {

                if (jobOrder.StartDate > jobOrder.EndDate)
                    return (false, "تاريخ البداية يجب أن يكون قبل تاريخ النهاية");

                Customer customer = _context.Customers.FirstOrDefault(c => c.CustomerId == jobOrder.CustomerId.Value);
                JobOrder addNewOne = new JobOrder();
                //addNewOne.RemainingAmount = jobOrder.RemainingAmount;
                //addNewOne.UnearnedRevenue = jobOrder.UnearnedRevenue;
                //addNewOne.EarnedRevenue = jobOrder.EarnedRevenue;
                addNewOne.JobOrdernotes = jobOrder.JobOrdernotes;
                addNewOne.OrderProgress = jobOrder.OrderProgress;
                addNewOne.CustomerId = jobOrder.CustomerId.Value;
                addNewOne.StartDate = jobOrder.StartDate.Value;
                addNewOne.EndDate = jobOrder.EndDate;
                addNewOne.EmployeeId = jobOrder.EmployeeId;
                addNewOne.JobOrderSource = customer.CustomerSource;

                _context.JobOrders.Add(addNewOne);
                _context.SaveChanges();
                return (true, "تمت انشاء امر العمل بنجاح");
            }

            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return (false, $"حدث خطأ: {ex.ToString()}");
            }
        }

        public (bool success, string message) EditJobOrder(int jobOrderID, JobOrderDTO jobOrder)
        {

            try
            {
                JobOrder existingJobOrder = _context.JobOrders
            .Include(j => j.Customer)
            .Include(j => j.Employee)
            .FirstOrDefault(j => j.JobOrderId == jobOrderID);
                if (existingJobOrder == null)
                {
                    return (false, "امر العمل غير موجود");
                }
                if (jobOrder.EndDate < existingJobOrder.StartDate)
                    return (false, "تاريخ الانتهاء يجب أن يكون بعد تاريخ البداية");

                existingJobOrder.RemainingAmount = jobOrder.RemainingAmount;
                existingJobOrder.UnearnedRevenue = jobOrder.UnearnedRevenue;
                existingJobOrder.EarnedRevenue = jobOrder.EarnedRevenue;
                existingJobOrder.JobOrdernotes = jobOrder.JobOrdernotes;
                existingJobOrder.OrderProgress = jobOrder.OrderProgress;
                existingJobOrder.EndDate = jobOrder.EndDate;
                _context.JobOrders.Update(existingJobOrder); // Mark the entity as modified
                _context.SaveChanges();
                return (true, "تمت تعديل امر العمل بنجاح");
            }
    
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return (false, $"حدث خطأ: {ex.ToString()}");
            }
        }
        public JobOrder GetJobOrderByID(int jobOrderID)
        {
            try
            {
                JobOrder existingJobOrder = _context.JobOrders
                    .Include(j => j.Customer)
                    .Include(j => j.Employee)
                    .FirstOrDefault(v => v.JobOrderId == jobOrderID);

                return existingJobOrder ?? throw new ArgumentException("امر العمل غير موجود");
            }
   
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }

        }
        public JobOrderCustEmpVM getJobOrderVM(int jobOrderID) {
            try
            {

                JobOrder job = _context.JobOrders.FirstOrDefault(jo => jo.JobOrderId == jobOrderID);
                Employee emp = _context.Employees.FirstOrDefault(emp => emp.EmployeeId == job.EmployeeId);
                Customer cus = _context.Customers.FirstOrDefault(cus => cus.CustomerId == job.CustomerId);
                JobOrderCustEmpVM jobSpecific = new JobOrderCustEmpVM();

                // job order
                jobSpecific.OrderProgress = job.OrderProgress;
                jobSpecific.JobOrderId = job.JobOrderId;
                jobSpecific.RemainingAmount = job.RemainingAmount;
                jobSpecific.EarnedRevenue = job.EarnedRevenue;
                jobSpecific.EarnedRevenue = job.EarnedRevenue;
                jobSpecific.JobOrdernotes = job.JobOrdernotes;
                jobSpecific.StartDate = job.StartDate;
                jobSpecific.EndDate = job.EndDate;
                // employee
                jobSpecific.EmployeeId = emp.EmployeeId;
                jobSpecific.EmployeeName = emp.EmployeeName;
                // customer
                jobSpecific.CustomerId = cus.CustomerId;
                jobSpecific.CustomerName = cus.CustomerName;
                jobSpecific.CustomerNotes = cus.CustomerNotes;
                jobSpecific.CustomerPhone = cus.CustomerPhone;
                jobSpecific.CustomerAddress = cus.CustomerAddress;
                jobSpecific.CustomerEmail = cus.CustomerEmail;

                return jobSpecific;
            }
   
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }

        }

        public bool AddCustomer(CustomerDTO newCustomer)
        {
            try
            {
                Customer foundCustomerByPhone = _context.Customers.FirstOrDefault(c => c.CustomerPhone == newCustomer.CustomerPhone);
                Customer foundCustomerByEmail = _context.Customers.FirstOrDefault(c => c.CustomerEmail == newCustomer.CustomerEmail);
                if (foundCustomerByEmail != null || foundCustomerByPhone != null)
                {

                    return false;
                }
                if (newCustomer == null)
                {
                    throw new ArgumentNullException(nameof(newCustomer));
                }
                Customer addedCustomer = new Customer();
                addedCustomer.CustomerName = newCustomer.CustomerName;
                addedCustomer.CustomerAddress = newCustomer.CustomerAddress;
                addedCustomer.CustomerEmail = newCustomer.CustomerEmail;
                addedCustomer.CustomerNotes = newCustomer.CustomerNotes;
                addedCustomer.CustomerPhone = newCustomer.CustomerPhone;
                addedCustomer.CustomerSource = newCustomer.CustomerSource;
                addedCustomer.Activated = true;
                _context.Customers.Add(addedCustomer);
                _context.SaveChanges();

                return true;
            }
         
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
            }
        }


        public Customer EditCustomer(int CustomerId)
        {
            try
            {
                Customer foundcustomer = _context.Customers.FirstOrDefault(c => c.CustomerId == CustomerId);
                if (foundcustomer == null)
                {
                    throw new ArgumentException("Customer not found.");
                }
                return foundcustomer;
            }

            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }

        }

        public bool EditCustomer(int CustomerId, CustomerDTO customer)
        {
            try
            {
                Customer foundCustomer = _context.Customers.FirstOrDefault(c => c.CustomerId == CustomerId);

                if (foundCustomer == null)
                {
                    throw new ArgumentException("Customer not found.");
                }

                bool isEmailUsedByAnother = _context.Customers
      .Any(c => c.CustomerEmail == customer.CustomerEmail && c.CustomerId != CustomerId);
                bool isPhoneUsedByAnother = _context.Customers
                    .Any(c => c.CustomerPhone == customer.CustomerPhone && c.CustomerId != CustomerId);

                if (isEmailUsedByAnother || isPhoneUsedByAnother)
                {
                    return false;
                }

                foundCustomer.CustomerName = customer.CustomerName;
                foundCustomer.CustomerAddress = customer.CustomerAddress;
                foundCustomer.CustomerEmail = customer.CustomerEmail;
                foundCustomer.CustomerNotes = customer.CustomerNotes;
                foundCustomer.CustomerPhone = customer.CustomerPhone;
                foundCustomer.CustomerSource = customer.CustomerSource;
                foundCustomer.Activated = customer.Activated;


                _context.Customers.Update(foundCustomer);
                _context.SaveChanges();
                return true;
            }
  
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
            }

        }



    }
}




