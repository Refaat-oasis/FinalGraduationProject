using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Client;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.ViewModels;

namespace ThothSystemVersion1.Hubs
{
    public class ProductHub : Hub
    {
        private readonly ThothContext _context; // Add your DbContext here

        public ProductHub(ThothContext context)
        {
            _context = context;
        }

        // Method to check reorder point for Paper
        public async Task CheckPaperReorderPoint()
        {
            List<Paper> papers = _context.Papers.
                Where(paper => paper.Activated == true).ToList();
            foreach (Paper paper in papers)
            {
                if (paper.Quantity < paper.ReorderPoint)
                {
                    await Clients.All.SendAsync("ReceiveReorderMessagePaper",
                        $"تم الوصول للحد الادني من : {paper.Name} , \n بكمية :{paper.Quantity}");
                }
            }
        }

        // Method to check reorder point for Ink
        public async Task CheckInkReorderPoint()
        {
            List<Ink> inks = _context.Inks.Where(ink => ink.Activated == true).ToList();
            foreach (Ink ink in inks)
            {
                if (ink.NumberOfUnits < ink.ReorderPoint)
                {
                    await Clients.All.SendAsync("ReceiveReorderMessageInk", $"تم الوصول للحد الادني من : {ink.Name} ,\n بكمية :{ink.NumberOfUnits}");
                }
            }
        }

        // Method to check reorder point for Supply
        public async Task CheckSupplyReorderPoint()
        {
            List<Supply> supplies = _context.Supplies.
                Where(supply => supply.Activated == true).ToList();
            foreach (Supply supply in supplies)
            {
                if (supply.Quantity < supply.ReorderPoint)
                {
                    await Clients.All.SendAsync("ReceiveReorderMessageSupply", $"تم الوصول للحد الادني من : {supply.Name} ,\n بكمية : {supply.Quantity}");
                }
            }
        }

        // method to check reorder point for spareParts
        public async Task CheckSparePartsReorderPoint()
        {
            List<SparePart> spareParts = _context.SpareParts
                .Where(spare => spare.Activated == true)
                .ToList();
            foreach (SparePart spare in spareParts)
            {
                if (spare.Quantity < spare.ReorderPoint)
                {
                    await Clients.All.SendAsync("ReceiveReorderMessageSupply",
                        $"تم الوصول للحد الادني من : {spare.Name} ,\n بكمية : {spare.Quantity}");
                }
            }
        }

        public async Task allLatejobOrders()
        {
            List<JobOrderCustEmpVM> viewModelList = new List<JobOrderCustEmpVM>();

            List<JobOrder> jobOrderList = _context.JobOrders.Where(jo => jo.OrderProgress != "تم التسليم"
            && jo.EndDate < DateOnly.FromDateTime(DateTime.Now)).ToList();

            foreach (JobOrder jo in jobOrderList) {

                JobOrderCustEmpVM viewModel = new JobOrderCustEmpVM();
                viewModel.JobOrderId = jo.JobOrderId;
                viewModel.CustomerName = _context.Customers.FirstOrDefault(cu => cu.CustomerId == jo.CustomerId).CustomerName;
                viewModelList.Add(viewModel);

            }
            
            foreach(JobOrderCustEmpVM jO in viewModelList) {

                await Clients.All.SendAsync("getallLatejobOrders",$"لم يتم تسليم الطلب الخاص ب {jO.CustomerName} رقم {jO.JobOrderId}");
            
            }

        }

        public async Task allLateReceipts() {

            List<JobOrderCustEmpVM> viewModelList = new List<JobOrderCustEmpVM>();

            List<JobOrder> jobOrderList = _context.JobOrders.Where(jo => jo.RemainingAmount > 0
             && jo.EndDate <= DateOnly.FromDateTime(DateTime.Now).AddDays(30)).ToList();

            foreach (JobOrder jo in jobOrderList)
            {
                JobOrderCustEmpVM viewModel = new JobOrderCustEmpVM();
                viewModel.JobOrderId = jo.JobOrderId;
                viewModel.CustomerName = _context.Customers.FirstOrDefault(cu => cu.CustomerId == jo.CustomerId).CustomerName;
                viewModelList.Add(viewModel);
            }

            foreach (JobOrderCustEmpVM jO in viewModelList)
            {
                await Clients.All.SendAsync("getallLateReceipts", $"تاخر العميل {jO.CustomerName} عن دفع المستحقات الخاصة بالطلب رقم {jO.JobOrderId}");
            }

        }

        public async Task allLatePayments() {

            List<PaymentPurchaseOrderVM> viewModelList = new List<PaymentPurchaseOrderVM>();

            List<PurchaseOrder> purchaseOrderList = _context.PurchaseOrders.Where(po => po.RemainingAmount > 0
             && po.PurchaseDate <= DateOnly.FromDateTime(DateTime.Now).AddDays(30)).ToList();
            foreach (PurchaseOrder po in purchaseOrderList)
            {
                PaymentPurchaseOrderVM viewModel = new PaymentPurchaseOrderVM();
                viewModel.PurchaseId = po.PurchaseId;
                viewModel.VendorName = _context.Vendors.FirstOrDefault(cu => cu.VendorId == po.VendorId).VendorName;
                viewModelList.Add(viewModel);
            }

            foreach (PaymentPurchaseOrderVM Po in viewModelList)
            {
                await Clients.All.SendAsync("getallLatePayments", $"لقد تأخر الدفع للمورد {Po.VendorName} عن امر شراء رقم {Po.PurchaseId}");
            }

        }

        public async Task checkInventoryBalancePaper()
        {
            int paperitems = _context.Papers.Where(paper => paper.Activated == true &&
            paper.Quantity < paper.ReorderPoint).Count();
            if (paperitems > 0)
            {
                await Clients.All.SendAsync("recieveInventoryBalances",
               $"يوجد عجز في الاوراق");
            }
        }
        public async Task checkInventoryBalanceInk()
        {
            int inkitems = _context.Inks.Where(ink => ink.Activated == true &&
            ink.NumberOfUnits < ink.ReorderPoint).Count();
            if (inkitems > 0)
            {
                await Clients.All.SendAsync("recieveInventoryBalances",
               $"يوجد عجز في الاحبار");
            }
        }
        public async Task checkInventoryBalanceSupply()
        {

            int supplyitems = _context.Supplies.Where(supply => supply.Activated == true &&
            supply.Quantity < supply.ReorderPoint).Count();
            if (supplyitems > 0)
            {
                await Clients.All.SendAsync("recieveInventoryBalances",
               $"يوجد عجز في المستلزمات");
            }
        }
        public async Task checkInventoryBalanceSpareParts()
        {
            int spareitems = _context.SpareParts.Where(spare => spare.Activated == true &&
            spare.Quantity < spare.ReorderPoint).Count();
            if (spareitems > 0)
            {
                await Clients.All.SendAsync("recieveInventoryBalances",
           $"يوجد عجز في قطع الغيار ");
            }
        }


        public async Task checkLateJobOrder() {

            int LateJobOrder = _context.JobOrders.Where(jo => jo.OrderProgress != "تم التسليم"
            && jo.EndDate < DateOnly.FromDateTime(DateTime.Now)).Count();

            if (LateJobOrder > 0) {
                await Clients.All.SendAsync("recieveLate", $"يوجد {LateJobOrder} طلبيات متأخرة");

            }
        }
       
        public async Task checkLatePayment()
        {

            int latePayment = _context.PurchaseOrders.Where(po => po.RemainingAmount > 0
             && po.PurchaseDate <= DateOnly.FromDateTime(DateTime.Now).AddDays(30)).Count();
            if (latePayment > 0)
            {
                await Clients.All.SendAsync("recieveLate", $"يوجد {latePayment} طلبيات شراء متأخرة الدفع");
            }
        }

        public async Task checkLateReceipt()
        {

            int lateReceipts = _context.JobOrders.Where(jo => jo.RemainingAmount > 0
             && jo.EndDate <= DateOnly.FromDateTime(DateTime.Now).AddDays(30)).Count();
            if (lateReceipts > 0)
            {
                await Clients.All.SendAsync("recieveLate", $"يوجد {lateReceipts} طلبيات  متأخرة التحصيل");
            }

        }


    } 
}