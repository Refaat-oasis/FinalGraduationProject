using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.DataTransfereObject
{
    public class RecieptOrderDTO
    {
        [Display(Name = "رقم امر التصنيع")]
        [Required(ErrorMessage ="خطا في رقك التعريفي لامر التصنيع")]
        public int JobOrderId { get; set; }

        [Display(Name = "المبلغ")]
        [Required(ErrorMessage = "يجب ادخال قيمة")]
        [Range(0, double.MaxValue, ErrorMessage = "المبلغ يجب أن يكون أكبر من 0")]
        public decimal? Amount { get; set; }

        [Display(Name = "ملاحظات")]
        public string? ReceiptNotes { get; set; }

        [Display(Name = "الرقم التعريفي الخاص بالموظف")]
        public string? EmployeeId { get; set; }
    }
}
