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

        [Display(Name = "تاريخ البدء")]
        public DateOnly? StartDate { get; set; }

        [Display(Name = "تاريخ الانتهاء")]
        public DateOnly EndDate { get; set; }

        [Display(Name = "الرقم التعريفي الخاص بالموظف")]
        public string? EmployeeId { get; set; }

        public List<ProcessBridge> processBridges { get; set; }

    }
}
