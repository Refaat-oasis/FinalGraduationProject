using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.ModifiedModels
{
    public class ModifiedReturnsOrder
    {

        [Display(Name = "الرقم التعريفي لامر المرتجع")]
        public int ReturnId { get; set; }

        [Display(Name = "تاريخ المرتجع")]
        public DateOnly? ReturnDate { get; set; }

        [Display(Name = "رقم امر التصنيع")]
        public int? JobOrderId { get; set; }

        [Display(Name = "رقم امر الشراء")]
        public int? PurchaseId { get; set; }

        [Display(Name = "مرتجع داخلي او خارجي")]
        public bool ReturnInOut { get; set; }

        [Display(Name = "الرقم التعريفي الخاص بالموظف")]
        public string EmployeeId { get; set; } = null!;

        [Display(Name = "اسم الموظف")]
        public string? EmployeeName { get; set; }

        [Display(Name = "ملاحظات")]
        public string? ReturnsNotes { get; set; }


        [Display(Name = "كمية العنصر")]
        public int balance { get; set; }

        [Display(Name = "السعر")]
        public decimal price { get; set; }


    }
}
