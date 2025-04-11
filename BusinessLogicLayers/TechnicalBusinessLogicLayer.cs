using Microsoft.EntityFrameworkCore;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.DataTransfereObject;
//using ThothSystemVersion1.DTOs;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.ViewModels;

namespace ThothSystemVersion1.BusinessLogicLayers
{
    public class TechnicalBusinessLogicLayer : TechnicalService
    {
        private readonly ThothContext _context;
        public TechnicalBusinessLogicLayer(ThothContext context)
        {
            //ThothContext _context = new ThothContext();
            _context = context;
        }


        public List<JobOrderCustEmpVM> ViewAllJobOrder()
        {

            List<JobOrder> jobOrdersList = _context.JobOrders.ToList();
            List<Customer> customersList = _context.Customers.ToList();
            List<Employee> employeeList = _context.Employees.ToList();
            List<JobOrderCustEmpVM> jobOrderCustomerViewModelsList = new List<JobOrderCustEmpVM>();

            foreach (JobOrder jobOrder in jobOrdersList)
            {

                Customer customer = customersList.FirstOrDefault(c => c.CustomerId == jobOrder.CustomerId);
                Employee employee = employeeList.FirstOrDefault(e => e.EmployeeId == jobOrder.EmployeeId);

                if (customer != null & employee != null)
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

                    //third add the employee data
                    jobToView.EmployeeId = jobOrder.EmployeeId;
                    jobToView.EmployeeName = employee.EmployeeName;

                    jobOrderCustomerViewModelsList.Add(jobToView);

                }

            }
            return jobOrderCustomerViewModelsList;
        }

        public List<JobOrder> GetLast10JobOrders()
        {
            return _context.JobOrders
                 .Include(j => j.Customer)
                .OrderByDescending(j => j.StartDate)
                .Take(10)
                .ToList();
        }

        public List<Paper> GetAvailablePapers()
        {
            return _context.Papers.Where(p => p.Activated).ToList();
        }

        public List<Ink> GetAvailableInks()
        {
            return _context.Inks.Where(i => i.Activated).ToList();
        }

        public List<Supply> GetAvailableSupplies()
        {
            return _context.Supplies.Where(s => s.Activated).ToList();
        }
        //public List<JobOrder> GetJobOrdersWithCustomers()
        //{
        //    return _context.JobOrders
        //           .Include(j => j.Customer)
        //            .OrderByDescending(j => j.StartDate)
        //           .Take(10)
        //           .ToList();
        //}

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
                    quantityBridgeList[i].QuantityBridgeID = null;

                    if (quantityBridgeList[i].InkId != null)
                    {
                        Ink ink = _context.Inks.FirstOrDefault(p => p.InkId == quantityBridgeList[i].InkId);

                        // Calculate new quantity and average price
                        double newQuantity = ink.Quantity - quantityBridgeList[i].Quantity;
                        decimal totalValue = (decimal)newQuantity * ink.Price;
                        //(decimal)quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;
                        //decimal averagePrice = totalValue / (decimal)totalQuantity;

                        //decimal newtotalBalance = quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;

                        //quantityBridgeList[i].TotalBalance = newtotalBalance;
                        // update to the old data in the bridge
                        quantityBridgeList[i].OldPrice = ink.Price;
                        quantityBridgeList[i].OldQuantity = ink.Quantity;
                        quantityBridgeList[i].OldTotalBalance = ink.TotalBalance;

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

                        //decimal averagePrice = totalValue / (decimal)totalQuantity;

                        //decimal newtotalBalance = quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;

                        //quantityBridgeList[i].TotalBalance = newtotalBalance;

                        // update to the old data in the bridge
                        quantityBridgeList[i].OldPrice = paper.Price;
                        quantityBridgeList[i].OldQuantity = paper.Quantity;
                        quantityBridgeList[i].OldTotalBalance = paper.TotalBalance;

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
                        //decimal averagePrice = totalValue / (decimal)totalQuantity;


                        //decimal newtotalBalance = quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;

                        //quantityBridgeList[i].TotalBalance = newtotalBalance;

                        // update to the old data in the bridge
                        quantityBridgeList[i].OldPrice = supply.Price;
                        quantityBridgeList[i].OldQuantity = supply.Quantity;
                        quantityBridgeList[i].OldTotalBalance = supply.TotalBalance;

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
                return (false, $"حدث خطأ: {ex.Message}");
            }

        }

        public List<Customer> ViewAllCustomer()
        {
            List<Customer> customerList = _context.Customers.ToList();

            return customerList;
        }

         public JobOrderSpecificationsViewModel ShowJobOrderSpecifications(int jobOrderId)
        {

            JobOrder jobOrder = _context.JobOrders.FirstOrDefault(j => j.JobOrderId == jobOrderId);
            MiscellaneousExpense miscellaneousExpense = _context.MiscellaneousExpenses.FirstOrDefault(m => m.JobOrderId == jobOrderId);
            RequisiteOrder requisiteOrder = _context.RequisiteOrders.FirstOrDefault(r => r.JobOrderId == jobOrderId);
            ReturnsOrder returnOrder = _context.ReturnsOrders.FirstOrDefault(r => r.JobOrderId == jobOrderId);
            Customer cust = _context.Customers.FirstOrDefault(c => c.CustomerId == jobOrder.CustomerId);
            Employee empJob = _context.Employees.FirstOrDefault(e => e.EmployeeId == jobOrder.EmployeeId);
            Employee empRequisite = _context.Employees.FirstOrDefault(e => e.EmployeeId == requisiteOrder.EmployeeId);
            Employee empReturn = _context.Employees.FirstOrDefault(e => e.EmployeeId == returnOrder.EmployeeId);
            Employee empMiscellen = _context.Employees.FirstOrDefault(e => e.EmployeeId == miscellaneousExpense.EmployeeId);
            List<QuantityBridge> quantityBridges = _context.QuantityBridges.Where(
                q => q.RequisiteId == requisiteOrder.RequisiteId || q.ReturnId == returnOrder.ReturnId)
            .ToList();
            List<ProcessBridge> processBridges = _context.ProcessBridges.Where(
                p => p.JobOrderId == jobOrderId)
                .ToList();
            List<Paper> papers = new List<Paper>();
            List<Ink> inks = new List<Ink>();
            List<Supply> supplies = new List<Supply>();
            List<Employee> employees = new List<Employee>();
            List<Labour> labours = new List<Labour>();
            List<Machine> machines = new List<Machine>();
            employees.Add(empJob);
            employees.Add(empRequisite);
            employees.Add(empReturn);
            employees.Add(empMiscellen);

            foreach (ProcessBridge pb in processBridges)
            {
                if (pb.LabourId != null)
                {
                    Labour lab = _context.Labours.FirstOrDefault(e => e.LabourId == pb.LabourId);
                    labours.Add(lab);
                }
                if (pb.MachineId != null)
                {
                    Machine mach = _context.Machines.FirstOrDefault(i => i.MachineId == pb.MachineId);
                    machines.Add(mach);
                }
            }

            foreach (QuantityBridge QB in quantityBridges)
            {

                if (QB.InkId != null)
                {
                    Ink ink = _context.Inks.FirstOrDefault(i => i.InkId == QB.InkId);
                    inks.Add(ink);
                }
                else if (QB.PaperId != null)
                {
                    Paper paper = _context.Papers.FirstOrDefault(p => p.PaperId == QB.PaperId);
                    papers.Add(paper);
                }
                else if (QB.SuppliesId != null)
                {
                    Supply supply = _context.Supplies.FirstOrDefault(s => s.SuppliesId == QB.SuppliesId);
                    supplies.Add(supply);
                }


            }


            JobOrderSpecificationsViewModel joborderSpecifics = new JobOrderSpecificationsViewModel();
            joborderSpecifics.JobOrderId = jobOrder.JobOrderId;
            joborderSpecifics.RemainingAmount = jobOrder.RemainingAmount;
            joborderSpecifics.UnearnedRevenue = jobOrder.UnearnedRevenue;
            joborderSpecifics.JobOrdernotes = jobOrder.JobOrdernotes;
            joborderSpecifics.EarnedRevenue = jobOrder.EarnedRevenue;
            joborderSpecifics.OrderProgress = jobOrder.OrderProgress;
            joborderSpecifics.CustomerId = jobOrder.CustomerId;
            joborderSpecifics.StartDate = jobOrder.StartDate;
            joborderSpecifics.EndDate = jobOrder.EndDate;
            joborderSpecifics.EmployeeId = jobOrder.EmployeeId;
            joborderSpecifics.MiscellaneousExpensesID = miscellaneousExpense.MiscellaneousExpensesID;
            joborderSpecifics.MaterialProcessingExpense = miscellaneousExpense.MaterialProcessingExpense;
            joborderSpecifics.FilmsProcessingExpense = miscellaneousExpense.FilmsProcessingExpense;
            joborderSpecifics.MaterialsTotal = miscellaneousExpense.MaterialsTotal;
            joborderSpecifics.TotalAfterMaterials = miscellaneousExpense.TotalAfterMaterials;
            joborderSpecifics.AdminstrativeExpense = miscellaneousExpense.AdminstrativeExpense;
            joborderSpecifics.TotalExpenses = miscellaneousExpense.TotalExpenses;
            joborderSpecifics.Percentage = miscellaneousExpense.Percentage;
            joborderSpecifics.TotalAfterPercentage = miscellaneousExpense.TotalAfterPercentage;
            joborderSpecifics.MinistryOfFinance = miscellaneousExpense.MinistryOfFinance;
            joborderSpecifics.EmployeeImprovmentBox = miscellaneousExpense.EmployeeImprovmentBox;
            joborderSpecifics.ValueAddedTax = miscellaneousExpense.ValueAddedTax;
            joborderSpecifics.FinalTotal = miscellaneousExpense.FinalTotal;
            joborderSpecifics.RequisiteId = requisiteOrder.RequisiteId;
            joborderSpecifics.RequisiteDate = requisiteOrder.RequisiteDate;
            joborderSpecifics.RequisiteNotes = requisiteOrder.RequisiteNotes;
            joborderSpecifics.ReturnId = returnOrder.ReturnId;
            joborderSpecifics.ReturnDate = returnOrder.ReturnDate;
            joborderSpecifics.ReturnsNotes = returnOrder.ReturnsNotes;
            joborderSpecifics.ReturnInOut = returnOrder.ReturnInOut;
            joborderSpecifics.QuantityBridges = quantityBridges;
            joborderSpecifics.ProcessBridges = processBridges;
            joborderSpecifics.CustomerName = cust.CustomerName;
            joborderSpecifics.Papers = papers;
            joborderSpecifics.Inks = inks;
            joborderSpecifics.Supplies = supplies;
            joborderSpecifics.Labours = labours;
            joborderSpecifics.Machines = machines;
            joborderSpecifics.Employees = employees;
            return joborderSpecifics;
        }


        public List<Employee> GetAvailableEmployees()
        {

            List<Employee> employeelist = _context.Employees.Where(e => e.Activated).ToList();
            return employeelist;
        }
        public List<Customer> GetAvailableCustomerss()
        {

            List<Customer> customerList = _context.Customers.Where(c => c.Activated).ToList();
            return customerList;
        }

        public (bool success, string message) AddJobOrder(JobOrderDTO jobOrder)
        {

            try
            {

                if (jobOrder.StartDate > jobOrder.EndDate)
                    return (false, "تاريخ البداية يجب أن يكون قبل تاريخ النهاية");

                JobOrder addNewOne = new JobOrder();
                addNewOne.RemainingAmount = jobOrder.RemainingAmount;
                addNewOne.UnearnedRevenue = jobOrder.UnearnedRevenue;
                addNewOne.EarnedRevenue = jobOrder.EarnedRevenue;
                addNewOne.JobOrdernotes = jobOrder.JobOrdernotes;
                addNewOne.OrderProgress = jobOrder.OrderProgress;
                addNewOne.CustomerId = jobOrder.CustomerId.Value;
                addNewOne.StartDate = jobOrder.StartDate.Value;
                addNewOne.EndDate = jobOrder.EndDate;
                addNewOne.EmployeeId = jobOrder.EmployeeId;

                _context.JobOrders.Add(addNewOne);
                _context.SaveChanges();
                return (true, "تمت انشاء امر العمل بنجاح");
            }
            catch (Exception ex)
            {
                return (false, $"حدث خطأ: {ex.Message}");
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
                return (false, $"حدث خطأ: {ex.Message}");
            }
        }
        public JobOrder GetJobOrderByID(int jobOrderID)
        {
            JobOrder existingJobOrder = _context.JobOrders
                .Include(j => j.Customer)
                .Include(j => j.Employee)
                .FirstOrDefault(v => v.JobOrderId == jobOrderID);

            return existingJobOrder ?? throw new ArgumentException("امر العمل غير موجود");
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
                addedCustomer.Activated = true;
                _context.Customers.Add(addedCustomer);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new ApplicationException("An error occurred while adding the customer.", ex);
            }
        }

        public Customer EditCustomer(int CustomerId)
        {
            try
            {
                Customer customer = _context.Customers.FirstOrDefault(c => c.CustomerId == CustomerId);
                if (customer == null)
                {
                    throw new ArgumentException("Customer not found.");
                }
                return customer;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new ApplicationException("An error occurred while fetching the Customer.", ex);
            }

        }
        public bool EditCustomer(int CustomerId, Customer customer)
        {
            try
            {
                Customer foundCustomer = _context.Customers.FirstOrDefault(c => c.CustomerId == CustomerId);

                if (foundCustomer == null)
                {
                    throw new ArgumentException("Customer not found.");
                }



                //Customer foundCustomerEmail = _context.Customers.FirstOrDefault(c => c.CustomerEmail == customer.CustomerEmail);
                //Customer foundCustomerPhone = _context.Customers.FirstOrDefault(c => c.CustomerPhone == customer.CustomerPhone);


                //if (foundCustomerEmail != null || foundCustomerPhone != null)
                //{
                //    return false;
                //}


                //foundCustomer.CustomerName = customer.CustomerName;
                foundCustomer.CustomerAddress = customer.CustomerAddress;
                foundCustomer.CustomerEmail = customer.CustomerEmail;
                foundCustomer.CustomerNotes = customer.CustomerNotes;
                foundCustomer.CustomerPhone = customer.CustomerPhone;
                //foundCustomer.Activated = customer.Activated;

                _context.Customers.Update(foundCustomer);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the customer.", ex);
            }
        }
    }
}




