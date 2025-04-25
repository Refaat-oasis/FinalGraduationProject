using System.ComponentModel.DataAnnotations;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.DataTransfereObject
{
    public class MachineLabourDTO
    {
        [Display(Name = "رقم امر التصنيع")]
        public int JobOrderId { get; set; }

        [Display(Name = "المبلغ المتبقي")]
        public decimal? RemainingAmount { get; set; }

        [Display(Name = "المبلغ الغير مستحق")]
        public decimal? UnearnedRevenue { get; set; }

        [Display(Name = "ملاحظات")]
        public string? JobOrdernotes { get; set; }

        [Display(Name = "مبلغ مستحق")]
        public decimal? EarnedRevenue { get; set; }

        [Display(Name = "مرحلة التقدم")]
        public string? OrderProgress { get; set; }

        [Display(Name = "العميل")]
        public int? CustomerId { get; set; }

        [Display(Name = "اسم العميل")]
        public string? CustomerName { get; set; }

        [Display(Name = "تاريخ البدء")]
        public DateOnly? StartDate { get; set; }

        [Display(Name = "تاريخ الانتهاء")]
        public DateOnly EndDate { get; set; }

        [Display(Name = "الرقم التعريفي الخاص بالموظف")]
        public string? EmployeeId { get; set; }
        [Display(Name = "اسم الموظف")]
        public string? EmployeeName { get; set; }

        public List<ProcessBridge> processBridges { get; set; }

        // QuantityBridge Balances
        [Display(Name = "قيمةا لورق الذي تم اسخدامه")]
        public decimal? paperBalance { get; set; }
        [Display(Name = "قيمة الحبر الذي تم استخدامه")]
        public decimal? inkBalance { get; set; }
        [Display(Name = "قيمة المستلزمات الذي تم استخدامه")]
        public decimal? supplyBalance { get; set; }

        // Miscellaneous Expense



        [Display(Name = "مصاريف تشغيل الخامات")]
        public decimal? MaterialProcessingExpense { get; set; }

        [Display(Name = "مصاريف تشغيل الافلام")]
        public decimal? FilmsProcessingExpense { get; set; }

        [Display(Name = "جملة الخامات")]
        public decimal? MaterialsTotal { get; set; }

        [Display(Name = "الاجمالي")]
        public decimal TotalAfterMaterials { get; set; }

        [Display(Name = "مصروفات ادارية")]
        public decimal? AdminstrativeExpense { get; set; }

        [Display(Name = "اجمالي المصروفات")]
        public decimal TotalExpenses { get; set; }

        [Display(Name = "النسبة")]
        public decimal Percentage { get; set; }

        [Display(Name = "الجملة")]
        public decimal TotalAfterPercentage { get; set; }

        [Display(Name = "وزارةا لمالية")]
        public decimal MinistryOfFinance { get; set; }

        [Display(Name = "صندوق تحسين العمال")]
        public decimal EmployeeImprovmentBox { get; set; }

        [Display(Name = "الاجمالي")]
        public decimal totalAfterEmplyeeImprovementbox { get; set; }

        [Display(Name = "صندوق تحسين العاملين")]
        public decimal ValueAddedTax { get; set; }

        [Display(Name = "القيمة الاجمالية")]
        public decimal FinalTotal { get; set; }

        [Display(Name = "اخرى")]
        public decimal? Other { get; set; }


    }
}
