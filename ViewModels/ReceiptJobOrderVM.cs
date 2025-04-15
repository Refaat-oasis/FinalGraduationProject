using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.ViewModels
{
    public class ReceiptJobOrderVM
    {
        // job order

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

        [Display(Name = "تاريخ البدء")]
        public DateOnly? StartDate { get; set; }

        [Display(Name = "تاريخ الانتهاء")]
        public DateOnly EndDate { get; set; }


        // customer

        [Display(Name = "العميل")]
        public int? CustomerId { get; set; }
        [Display(Name = "اسم العميل")]
        public string CustomerName { get; set; } = null!;

        // employee

        [Display(Name = "الرقم التعريفي الخاص بالموظف")]
        public string? EmployeeId { get; set; }

        [Display(Name = "اسم الموظف")]
        public string EmployeeName { get; set; } = null!;


        // receipt
        [Display(Name = "الرقم الخاص باستلام الاموال")]
        public int RecieptId { get; set; }

        [Display(Name = "المبلغ")]
        public decimal? Amount { get; set; }

        [Display(Name = "تاريخ الاستلام")]
        public DateOnly? ReceiptDate { get; set; }

        [Display(Name = "ملاحظات")]
        public string? ReceiptNotes { get; set; }


    }
}
