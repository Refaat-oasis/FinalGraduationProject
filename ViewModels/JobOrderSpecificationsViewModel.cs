using System.ComponentModel.DataAnnotations;
using ThothSystemVersion1.Models;
namespace ThothSystemVersion1.ViewModels
{
    public class JobOrderSpecificationsViewModel
    {
        // the job order table 
        [Display(Name = "الرقم التعريفي للتشغيلة")]
        public int JobOrderId { get; set; }

        [Display(Name = "المبلغ المتبقي")]
        public decimal? RemainingAmount { get; set; }

        [Display(Name = "المبلغ المدفوع مقدما")]
        public decimal? UnearnedRevenue { get; set; }

        [Display(Name = "ملاحظات عن التشغيلة")]
        public string? JobOrdernotes { get; set; }

        [Display(Name = "المبلغ المستحق")]
        public decimal? EarnedRevenue { get; set; }

        [Display(Name = "مدي التقدم في التشغيلة")]
        public string? OrderProgress { get; set; }

        [Display(Name = "الرقم المميز للعميل")]
        public int? CustomerId { get; set; }

        [Display(Name = "تاريخ البدأ في التشغيلة")]
        public DateOnly? StartDate { get; set; }

        [Display(Name = "تاريخ انتهاء التشغيلة")]
        public DateOnly EndDate { get; set; }

        [Display(Name = "الرقم القومي للموظف")]
        public string? EmployeeId { get; set; }

        // the miscellaneous expense table
        public int? MiscellaneousExpensesID { get; set; }

        [Display(Name = "مصاريف تشغيل الالات")]
        public decimal? MaterialProcessingExpense { get; set; }

        [Display(Name = "مصاريف تشغيل الافلام")]
        public decimal? FilmsProcessingExpense { get; set; }

        [Display(Name = "جملة الخامات")]
        public decimal? MaterialsTotal { get; set; }

        [Display(Name = "الاجمالي")]
        public decimal TotalAfterMaterials { get; set; }

        [Display(Name = "مصاريف ادارية")]
        public decimal? AdminstrativeExpense { get; set; }

        [Display(Name = "احمالي المصروفات")]
        public decimal TotalExpenses { get; set; }

        [Display(Name = "النسبة")]
        public decimal Percentage { get; set; }

        [Display(Name = "الجملة")]
        public decimal TotalAfterPercentage { get; set; }

        [Display(Name = "وزارة المالية")]
        public decimal MinistryOfFinance { get; set; }

        [Display(Name = "صندوق تحسين العاملين")]
        public decimal EmployeeImprovmentBox { get; set; }

        [Display(Name = "ضريبة القيمة المضافة")]
        public decimal ValueAddedTax { get; set; }

        [Display(Name = "القيمة الاجمالية")]
        public decimal FinalTotal { get; set; }

        // the requisite order table
        [Display(Name = "الرقم التعريفي لازن الصرف")]
        public int RequisiteId { get; set; }

        [Display(Name = "تاريخ اذن الصرف")]
        public DateOnly? RequisiteDate { get; set; }

        [Display(Name = "ملاحظات عن اذن الصرف")]
        public string? RequisiteNotes { get; set; }

        // the returns order table

        [Display(Name = "الرقم التعريفي لاذن المردودات")]
        public int ReturnId { get; set; }

        [Display(Name = "تاريخ اذن المردودات")]
        public DateOnly? ReturnDate { get; set; }


        [Display(Name = "ملاحظات عن اذن المردودات")]
        public string? ReturnsNotes { get; set; }

        [Display(Name = "الداخلي والخارجي")]
        public bool ReturnInOut { get; set; }
        [Display(Name = "اسم العميل")]
        public string CustomerName { get; set; }

        public List<Ink> Inks { get; set; }
        public List<Paper> Papers { get; set; }
        public List<Supply> Supplies { get; set; }
        public List<Labour> Labours { get; set; }
        public List<Machine> Machines { get; set; }
        public List<Employee> Employees { get; set; }

        public List<QuantityBridge> QuantityBridges { get; set; }
        public List<ProcessBridge> ProcessBridges { get; set; }

    }
}
