                                                                                                                                                                          using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.ViewModels
{
    public class PurchaseOrderVM
    {
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

        [Display(Name = "الرقم التعريفي الخاص بالموظف")]
        public string EmployeeId { get; set; } = null!;
        [Display(Name = "اسم الموظف")]
        public string? EmployeeName { get; set; }

        [Display(Name = "اسم المعرف للمورد")]
        public int VendorId { get; set; }

        [Display(Name = "اسم المورد")]
        public string VendorName { get; set; } = null!;

        [Display(Name = "عنوان المورد")]
        public string? VendorAddress { get; set; }

        [Display(Name = "الايميل الخاص للمورد")]
        public string? VendorEmail { get; set; }

        [Display(Name = "ملاحظات")]
        public string? VendorNotes { get; set; }

        [Display(Name = "رقم المورد")]
        public string VendorPhone { get; set; } = null!;


    }
}