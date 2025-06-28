using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.ViewModels
{
    public class AccountingReportViewModel
    {
        [Display(Name ="عدد الطلبيات")]
        public int jobOrdersCount { get; set; }
        [Display(Name = "المبلغ المتبقي")]
        public decimal jobOrderRemainingAmount { get; set; }

        [Display(Name = "المبلغ الغير مستحق")]
        public decimal jobOrderUnearnedRevenue { get; set; }

        [Display(Name = "مبلغ مستحق")]
        public decimal jobOrderEarnedRevenue { get; set; }
        [Display(Name ="عدد الطلبيات المتاخرة في التحصيل")]
        public int lateJobOrders { get; set; }

       /////////////////////////////////////////////////////////////
        
        [Display(Name = "عدد طلبيات الشراء")]
        public int purchaseOrdersCount { get; set; }

        [Display(Name = "المبلغ المتبقي")]
        public decimal purchaseRemainingAmount { get; set; }

        [Display(Name = "المبلغ المدفوع")]
        public decimal purchasePaidAmount { get; set; }
        [Display(Name ="عدد اوامر الشراء المتاخرة في الدفع")]
        public int latepurchaseOrders { get; set; }

    }
}
