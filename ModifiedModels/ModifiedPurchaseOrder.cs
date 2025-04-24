using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.ModifiedModels
{
    public class ModifiedPurchaseOrder
    {


        [Display(Name = "رقم امر الشراء")]
        public int PurchaseId { get; set; }

        [Display(Name = "تاريخ الشراء")]
        public DateOnly? PurchaseDate { get; set; }

        [Display(Name = "رقم التعريفي الخاص بالموظف")]
        public string EmployeeId { get; set; } = null!;

        [Display(Name ="اسم الموظف")]
        public string? EmployeeName { get; set; }


        [Display(Name = "المبلغ المتبقي")]
        public decimal? RemainingAmount { get; set; }

        [Display(Name = "المبلغ المدفوع")]
        public decimal? PaidAmount { get; set; }

        [Display(Name = "رقم التعريفي الخاص بالتاجر")]
        public int VendorId { get; set; }

        [Display(Name ="اسم المورد")]
        public string? Vendorname { get; set; }

        [Display(Name = "ملاحظات")]
        public string? PurchaseNotes { get; set; }

        [Display(Name ="اسم العنصر")]
        public string balance { get; set; } = null!;

        [Display(Name ="السعر")]
        public string price { get; set; } = null!;


    }
}
