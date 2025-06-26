using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.ModifiedModels
{
    public class ModifiedRequisiteOrder
    {

        [Display(Name = "رقم امر الصرف")]
        public int RequisiteId { get; set; }

        [Display(Name = "تاريخ امر الصرف")]
        public DateOnly? RequisiteDate { get; set; }

        [Display(Name = "الرقم التعريفي الخاص بالموظف")]
        public string EmployeeId { get; set; } = null!;
        [Display(Name = "اسم الموظف")]
        public string? EmployeeName { get; set; }

        [Display(Name = "رقم امر التصنيع")]
        public int JobOrderId { get; set; }

        [Display(Name = "ملاحظات")]
        public string? RequisiteNotes { get; set; }


        [Display(Name = "كمية العنصر")]
        public int balance { get; set; }

        [Display(Name = "السعر")]
        public decimal price { get; set; }

    }
}
