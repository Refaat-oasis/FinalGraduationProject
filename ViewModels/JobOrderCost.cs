using System.ComponentModel.DataAnnotations;
using ThothSystemVersion1.ModifiedModels;

namespace ThothSystemVersion1.ViewModels
{
    public class JobOrderCost
    {
        //JobOrder data

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
        public DateOnly? StartDate { get; set; }
        [Display(Name = "تاريخ انتهاء التشغيلة")]
        public DateOnly EndDate { get; set; }
        [Display(Name = "الرقم القومي للموظف")]
        //employee data
        public string? EmployeeId { get; set; }
        [Display(Name = "اسم الموظف")]
        public string? EmployeeName { get; set; }
        //customer data

        [Display(Name = "الرقم التعريفي للعميل")]
        public int CustomerId { get; set; }

        [Display(Name = "اسم العميل")]
        public string CustomerName { get; set; } = null!;

        [Display(Name = "عنوان العميل")]
        public string? CustomerAddress { get; set; }

        [Display(Name = "الايميل الخاص بالعميل")]
        public string? CustomerEmail { get; set; }

        [Display(Name = "ملاحظات عن العميل")]
        public string? CustomerNotes { get; set; }

        [Display(Name = "رقم الهاتف الخاص بالعميل")]
        public string CustomerPhone { get; set; } = null!;

        [Display(Name = "قيمةا لورق الذي تم اسخدامه")]
        public decimal? paperBalance { get; set; }
        [Display(Name = "قيمة الحبر الذي تم استخدامه")]
        public decimal? inkBalance { get; set; }
        [Display(Name = "قيمة المستلزمات الذي تم استخدامه")]
        public decimal? supplyBalance { get; set; }

        public List<ModifiedQuantityBridge> modifiedQuantityBridgeList { get; set; }

    }
}
