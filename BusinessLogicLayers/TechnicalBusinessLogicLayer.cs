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
        public List<JobOrder> GetJobOrdersWithCustomers()
        {
            return _context.JobOrders
                   .Include(j => j.Customer)
                    .OrderByDescending(j => j.StartDate)
                   .Take(10)
                   .ToList();
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
                    quantityBridgeList[i].QuantityBridgeID = null;

                    if (quantityBridgeList[i].InkId != null)
                    {
                        Ink ink = _context.Inks.FirstOrDefault(p => p.InkId == quantityBridgeList[i].InkId);

                        // Calculate new quantity and average price
                        double totalQuantity = ink.Quantity - quantityBridgeList[i].Quantity;
                        decimal totalValue = (decimal)ink.Quantity * ink.Price +
                                             (decimal)quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;
                        decimal averagePrice = totalValue / (decimal)totalQuantity;

                        decimal newtotalBalance = quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;

                        quantityBridgeList[i].TotalBalance = newtotalBalance;
                        // update to the old data in the bridge
                        quantityBridgeList[i].OldPrice = ink.Price;
                        quantityBridgeList[i].OldQuantity = ink.Quantity;
                        quantityBridgeList[i].OldTotalBalance = ink.TotalBalance;

                        // Update paper properties
                        ink.Quantity = (int)totalQuantity;
                        ink.Price = averagePrice;
                        ink.TotalBalance = (decimal)totalQuantity * averagePrice;

                        _context.Inks.Update(ink);
                        _context.QuantityBridges.Add(quantityBridgeList[i]);
                        _context.SaveChanges();

                    }
                    else if (quantityBridgeList[i].PaperId != null)
                    {
                        Paper paper = _context.Papers.FirstOrDefault(p => p.PaperId == quantityBridgeList[i].PaperId);

                        // Calculate new quantity and average price
                        double totalQuantity = paper.Quantity - quantityBridgeList[i].Quantity;
                        decimal totalValue = (decimal)paper.Quantity * paper.Price +
                                             (decimal)quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;
                        decimal averagePrice = totalValue / (decimal)totalQuantity;

                        decimal newtotalBalance = quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;

                        quantityBridgeList[i].TotalBalance = newtotalBalance;

                        // update to the old data in the bridge
                        quantityBridgeList[i].OldPrice = paper.Price;
                        quantityBridgeList[i].OldQuantity = paper.Quantity;
                        quantityBridgeList[i].OldTotalBalance = paper.TotalBalance;

                        // Update paper properties
                        paper.Quantity = (int)totalQuantity;
                        paper.Price = averagePrice;
                        paper.TotalBalance = (decimal)totalQuantity * averagePrice;
                        _context.Papers.Update(paper);
                        _context.QuantityBridges.Add(quantityBridgeList[i]);
                        _context.SaveChanges();

                    }
                    else if (quantityBridgeList[i].SuppliesId != null)
                    {
                        Supply supply = _context.Supplies.FirstOrDefault(p => p.SuppliesId == quantityBridgeList[i].SuppliesId);

                        // Calculate new quantity and average price
                        double totalQuantity = supply.Quantity - quantityBridgeList[i].Quantity;
                        decimal totalValue = (decimal)supply.Quantity * supply.Price +
                                             (decimal)quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;
                        decimal averagePrice = totalValue / (decimal)totalQuantity;


                        decimal newtotalBalance = quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;

                        quantityBridgeList[i].TotalBalance = newtotalBalance;

                        // update to the old data in the bridge
                        quantityBridgeList[i].OldPrice = supply.Price;
                        quantityBridgeList[i].OldQuantity = supply.Quantity;
                        quantityBridgeList[i].OldTotalBalance = supply.TotalBalance;

                        // Update paper properties
                        supply.Quantity = (int)totalQuantity;
                        supply.Price = averagePrice;
                        supply.TotalBalance = (decimal)totalQuantity * averagePrice;
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
    }
}




