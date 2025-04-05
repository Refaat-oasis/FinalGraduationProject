using System.ComponentModel.DataAnnotations;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.DataTransfereObject
{
    public class RequisiteOrderDTO
    {
        [Display(Name = "الرقم القومي للموظف")]
        public string EmployeeId { get; set; } = null!;

        [Display(Name = "الرقم التعريفي للتشغيلة")]
        public int JobOrderId { get; set; }


        [Display(Name = "ملاحظات عن اذن الصرف")]
        public string? RequisiteNotes { get; set; }

        public List<QuantityBridge>? BridgeList { get; set; }

    }
}