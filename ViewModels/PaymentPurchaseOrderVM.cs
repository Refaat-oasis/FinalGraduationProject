using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.ViewModels
{
    public class PaymentPurchaseOrderVM
    {
        // purchase
        [Display(Name = "رقم امر الشراء")]
        public int PurchaseId { get; set; }

        [Display(Name = "تاريخ الشراء")]
        public DateOnly? PurchaseDate { get; set; }

        [Display(Name = "المبلغ المتبقي")]
        public decimal? RemainingAmount { get; set; }

        [Display(Name = "المبلغ المدفوع")]
        public decimal? PaidAmount { get; set; }

        [Display(Name = "ملاحظات")]
        public string? PurchaseNotes { get; set; }

        // employee
        [Display(Name = "رقم التعريفي الخاص بالموظف")]
        public string EmployeeId { get; set; } = null!;

        [Display(Name = "اسم الموظف")]
        public string EmployeeName { get; set; } = null!;

        // vendor

        [Display(Name = "رقم التعريفي الخاص بالتاجر")]
        public int VendorId { get; set; }

        [Display(Name = "اسم المورد")]
        public string VendorName { get; set; } = null!;

        // payment

        [Display(Name = "رقم امر الدفع")]
        public int PaymentId { get; set; }

        [Display(Name = "المبلغ")]
        public decimal? Amount { get; set; }

        [Display(Name = "تاريخ امر الدفع")]
        public DateOnly? PaymentDate { get; set; }

        [Display(Name = "ملاحظات")]
        public string? PaymentNotes { get; set; }



    }
}
