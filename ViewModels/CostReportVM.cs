using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.ViewModels
{
    public class CostReportVM
    {
        [Display(Name = "عدد الطلبيات")]
        public int JobOrdersCount { get; set; }

        [Display(Name = "المبلغ المتبقي")]
        public decimal JobOrderRemainingAmount { get; set; }

        [Display(Name = "المبلغ الغير مستحق")]
        public decimal JobOrderUnearnedRevenue { get; set; }

        [Display(Name = "مبلغ مستحق")]
        public decimal JobOrderEarnedRevenue { get; set; }


        [Display(Name = "القيمة الاجمالية")]
        public decimal FinalTotal { get; set; }

        [Display(Name = "إجمالي الإيرادات")]
        public decimal TotalRevenue { get; set; }

        [Display(Name = "إجمالي التكلفة")]
        public decimal TotalCost { get; set; }

        [Display(Name = "صافي الربح")]
        public decimal NetProfit { get; set; }
    }
}
