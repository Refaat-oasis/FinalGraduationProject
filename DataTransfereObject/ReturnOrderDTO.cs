using System.ComponentModel.DataAnnotations;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.DataTransfereObject
{
    public class ReturnOrderDTO
    {

        [Display(Name = "الرقم القومي للموظف")]
        public string EmployeeId { get; set; } = null!;

        [Display(Name = "الرقم التعريفي للتاجر")]
        public int VendorId { get; set; }

        [Display(Name = "ملاحظات عن شراء البضاعة")]
        public string? PurchaseNotes { get; set; }

        public List<QuantityBridge> requisitedList { get; set; }
        
        public List<QuantityBridge>? returnedList { get; set; }


    }
}
