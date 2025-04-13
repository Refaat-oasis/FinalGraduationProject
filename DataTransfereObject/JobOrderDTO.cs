using System.ComponentModel.DataAnnotations;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.DataTransfereObject
{
    public class JobOrderDTO
    {
        [Display(Name = "الرقم التعريفي للتشغيلة")]
        public int JobOrderId { get; set; }

        [Range(typeof(decimal), "0", "1000000", ErrorMessage = "المبلغ المتبقي يجب أن يكون بين 0 و 1,000,000")]
        [Required(ErrorMessage = "يجب عليك ادخال المبلغ المتبقى")]
        [Display(Name = "المبلغ المتبقي")]
        public decimal? RemainingAmount { get; set; }

        [Range(typeof(decimal), "0", "1000000", ErrorMessage = "المبلغ المدفوع مقدما يجب أن يكون بين 0 و 1,000,000")]
        [Required(ErrorMessage = "يجب عليك ادخال المبلغ المدفوع مقدما")]
        [Display(Name = "المبلغ المدفوع مقدما")]
        public decimal? UnearnedRevenue { get; set; }

        [Display(Name = "ملاحظات عن التشغيلة")]
        public string? JobOrdernotes { get; set; }

        [Range(typeof(decimal), "0", "1000000", ErrorMessage = "المبلغ المستحق يجب أن يكون بين 0 و 1,000,000")]
        [Required(ErrorMessage = "يجب عليك ادخال المبلغ المستحق")]
        [Display(Name = "المبلغ المستحق")]
        public decimal? EarnedRevenue { get; set; }

        [Required(ErrorMessage = "يجب عليك ادخال مدي التقدم في التشغيلة")]
        [Display(Name = "مدي التقدم في التشغيلة")]
        public string? OrderProgress { get; set; }

        [Required(ErrorMessage = "يجب عليك ادخال الرقم المميز للعميل")]
        [Display(Name = "الرقم المميز للعميل")]
        public int? CustomerId { get; set; }

        [Required(ErrorMessage = "يجب عليك ادخال تاريخ البدأ في التشغيلة")]
        [Display(Name = "تاريخ البدأ في التشغيلة")]
        public DateOnly? StartDate { get; set; }

        [Required(ErrorMessage = "يجب عليك ادخال تاريخ انتهاء التشغيلة")]
        [Display(Name = "تاريخ انتهاء التشغيلة")]
        public DateOnly EndDate { get; set; }

        [Required(ErrorMessage = "يجب عليك ادخال الرقم القومي للموظف")]
        [Display(Name = "الرقم القومي للموظف")]
        public string? EmployeeId { get; set; }

    }
}
