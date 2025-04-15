using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.DataTransfereObject
{
    public class PaymentOrderDTO
    {
        [Display(Name = "رقم امر الشراء")]
        public int PurchaseId { get; set; }

        [Display(Name = "المبلغ")]
        public decimal? Amount { get; set; }

        [Display(Name = "تاريخ امر الدفع")]
        public DateOnly? PaymentDate { get; set; }

        [Display(Name = "ملاحظات")]
        public string? PaymentNotes { get; set; }

        [Display(Name = "الرقم التعريفي الخاص بالموظف")]
        public string? EmployeeId { get; set; }

    }
}
