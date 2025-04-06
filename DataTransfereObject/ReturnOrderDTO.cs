using System.ComponentModel.DataAnnotations;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.DataTransfereObject
{
    public class ReturnOrderDTO
    {

        [Display(Name = "الرقم القومي للموظف")]
        public string EmployeeId { get; set; } = null!;

        [Display(Name = "ملاحظات عن اذن المردودات")]
        public string? ReturnsNotes { get; set; }

        [Display(Name = "الداخلي والخارجي")]
        public bool ReturnInOut { get; set; }

        [Display(Name = " الرقم التعريفي لطلبيه الشراء")]
        public int? purchaseID { get; set; }
        [Display(Name = "الرقم التعريفي للتشغيلة ")]
        public int? JobOrderId { get; set; }

        public List<QuantityBridge>? BridgeList { get; set; }


    }
}
