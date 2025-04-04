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
        //public (bool Success, string Message, int RequisiteId) CreateRequisition(RequisiteOrderDTO dto)
        //{
        //    try
        //    {
        //        // 1. إنشاء أذن الصرف الأساسي
        //        var order = new RequisiteOrder
        //        {
        //            EmployeeId = dto.EmployeeId,
        //            JobOrderId = dto.JobOrderId,
        //            RequisiteDate = DateOnly.FromDateTime(DateTime.Now),
        //            RequisiteNotes = dto.RequisiteNotes
        //        };

        //        _context.RequisiteOrders.Add(order);
        //        _context.SaveChanges(); // حفظ أولي للحصول على ID

        //        // 2. معالجة الأصناف المطلوبة
        //        foreach (var item in dto.Items)
        //        {
        //            // تحديث المخزون
        //            switch (item.Type)
        //            {
        //                case "Paper":
        //                    var paper = _context.Papers.Find(item.ItemId);
        //                    if (paper == null) return (false, "الورق غير موجود", 0);
        //                    if (paper.Quantity < item.Quantity) return (false, "الكمية غير متوفرة في المخزون", 0);
        //                    paper.Quantity -= item.Quantity;
        //                    _context.SaveChanges();
        //                    break;

        //                case "Ink":
        //                    var ink = _context.Inks.Find(item.ItemId);
        //                    if (ink == null) return (false, "الحبر غير موجود", 0);
        //                    if (ink.Quantity < item.Quantity) return (false, "الكمية غير متوفرة في المخزون", 0);
        //                    ink.Quantity -= item.Quantity;
        //                    _context.SaveChanges();
        //                    break;

        //                case "Supply":
        //                    var supply = _context.Supplies.Find(item.ItemId);
        //                    if (supply == null) return (false, "المستلزمات غير موجودة", 0);
        //                    if (supply.Quantity < item.Quantity) return (false, "الكمية غير متوفرة في المخزون", 0);
        //                    supply.Quantity -= item.Quantity;
        //                    _context.SaveChanges();
        //                    break;

        //                default:
        //                    return (false, "نوع الصنف غير صحيح", 0);
        //            }

        //            // إنشاء سجل الجسر
        //            var quantityBridge = new QuantityBridge
        //            {
        //                RequisiteId = order.RequisiteId,
        //                Quantity = item.Quantity,
        //                Price = GetItemPrice(item.Type, item.ItemId),
        //                PaperId = item.Type == "Paper" ? item.ItemId : null,
        //                InkId = item.Type == "Ink" ? item.ItemId : null,
        //                SuppliesId = item.Type == "Supply" ? item.ItemId : null
        //            };

        //            _context.QuantityBridges.Add(quantityBridge);
        //            _context.SaveChanges();
        //        }

        //        _context.SaveChanges(); // حفظ نهائي لجميع التغييرات

        //        return (true, "تم إنشاء أمر الصرف بنجاح", order.RequisiteId);
        //    }
        //    catch (Exception ex)
        //    {
        //        return (false, $"حدث خطأ: {ex.Message}", 0);
        //    }
        //}
        public (bool Success, string Message, int RequisiteId) CreateRequisition(RequisiteOrderDTO dto)
        {
            try
            {
                // 1. إنشاء أذن الصرف الأساسي
                RequisiteOrder order = new RequisiteOrder
                {
                    EmployeeId = dto.EmployeeId,
                    JobOrderId = dto.JobOrderId,
                    RequisiteDate = DateOnly.FromDateTime(DateTime.Now),
                    RequisiteNotes = dto.RequisiteNotes
                };

                _context.RequisiteOrders.Add(order);
                _context.SaveChanges(); // حفظ أولي للحصول على ID

                // 2. معالجة الأصناف المطلوبة
                foreach (var item in dto.Items)
                {
                    // تحديث المخزون
                    switch (item.Type)
                    {
                        case "Paper":
                            var paper = _context.Papers.Find(item.ItemId);
                            if (paper == null) return (false, "الورق غير موجود", 0);
                            if (paper.Quantity < item.Quantity) return (false, "الكمية غير متوفرة في المخزون", 0);
                            paper.Quantity -= item.Quantity;
                            _context.Papers.Update(paper);
                            break;

                        case "Ink":
                            var ink = _context.Inks.Find(item.ItemId);
                            if (ink == null) return (false, "الحبر غير موجود", 0);
                            if (ink.Quantity < item.Quantity) return (false, "الكمية غير متوفرة في المخزون", 0);
                            ink.Quantity -= item.Quantity;
                            _context.Inks.Update(ink);
                            break;

                        case "Supply":
                            var supply = _context.Supplies.Find(item.ItemId);
                            if (supply == null) return (false, "المستلزمات غير موجودة", 0);
                            if (supply.Quantity < item.Quantity) return (false, "الكمية غير متوفرة في المخزون", 0);
                            supply.Quantity -= item.Quantity;
                            _context.Supplies.Update(supply);
                            break;

                        default:
                            return (false, "نوع الصنف غير صحيح", 0);
                    }

                    // إنشاء سجل الجسر
                    var quantityBridge = new QuantityBridge
                    {
                        RequisiteId = order.RequisiteId,
                        Quantity = item.Quantity,
                        Price = GetItemPrice(item.Type, item.ItemId),
                        PaperId = item.Type == "Paper" ? item.ItemId : null,
                        InkId = item.Type == "Ink" ? item.ItemId : null,
                        SuppliesId = item.Type == "Supply" ? item.ItemId : null
                    };

                    _context.QuantityBridges.Add(quantityBridge);
                }

                _context.SaveChanges(); // حفظ نهائي لجميع التغييرات

                return (true, "تم إنشاء أمر الصرف بنجاح", order.RequisiteId);
            }
            catch (Exception ex)
            {
                // تنظيف أي تغييرات معلقة
                _context.ChangeTracker.Clear();
                return (false, $"حدث خطأ: {ex.Message}", 0);
            }
        }




        private decimal GetItemPrice(string itemType, int itemId)
        {
            switch (itemType)
            {
                case "Paper":
                    var paper = _context.Papers.Find(itemId);
                    return paper?.Price ?? 0m;
                case "Ink":
                    var ink = _context.Inks.Find(itemId);
                    return ink?.Price ?? 0m;
                case "Supply":
                    var supply = _context.Supplies.Find(itemId);
                    return supply?.Price ?? 0m;
                default:
                    return 0m;
            }
        }

    }
}




