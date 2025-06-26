using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.ModifiedModels
{
    public class ModifiedPerpetualRequisiteOrder
    {
        [Display(Name = "رقم امر الصرف الدائم")]
        public int PerpetualRequisiteId { get; set; }
        
        [Display(Name = "تاريخ امر الصرف الدائم")]
        public DateOnly? PerpetualRequisiteDate { get; set; }
        
        [Display(Name = "رقم القومي للموظف")]
        public string EmployeeId { get; set; } = null!;

        [Display(Name = "اسم الموظف")]
        public string EmployeeName { get; set; } = null!;

        [Display(Name = "ملاحظات")]
        public string? RequisiteNotes { get; set; }
        
        [Display(Name = "رقم الالة")]
        public int MachineStoreId { get; set; }

        [Display(Name = "اسم الماكينة")]
        public string Name { get; set; } = null!;

        [Display(Name = "كمية العنصر")]
        public int balance { get; set; }

        [Display(Name = "السعر")]
        public decimal price { get; set; }

    }
}
