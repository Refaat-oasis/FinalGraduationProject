using System.ComponentModel.DataAnnotations;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.DataTransfereObject
{
    public class PerpetualRequisiteDTO
    {
        [Display(Name = "الرقم القومي للموظف")]
        [Required(ErrorMessage = "رقم الموظف مطلوب")]
        public string EmployeeId { get; set; } = null!;

        [Display(Name = "الرقم التعريفي للألة")]
        public int MachineStoreId { get; set; }

        [Display(Name = "ملاحظات عن اذن الصرف")]
        [StringLength(500, ErrorMessage = "يجب ألا تتجاوز الملاحظات 500 حرف")]
        public string? RequisiteNotes { get; set; }

        [Required(ErrorMessage = "يجب إضافة أصناف للطلب")]
        public List<QuantityBridge>? BridgeList { get; set; }
    }
}
