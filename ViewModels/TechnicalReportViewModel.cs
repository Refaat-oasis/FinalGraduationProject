using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.ViewModels
{
    public class TechnicalReportViewModel
    {

        [Display(Name = "عدد الطلبيات")]
        public int jobOrdersCount { get; set; }
        [Display(Name = "عدد الطلبيات المتاخرة في التسليم")]
        public int lateJobOrders { get; set; }
        [Display(Name = "عدد الطلبيات تم الانتهاء منها و تسليمها   ")]
        public int onTimeJobOrders { get; set; }

        [Display(Name = "طلبيات قيد الانتظار")]
        public int pendingJobOrders { get; set; }

        [Display(Name = "طلبيات قيد التنفيذ")]
        public int inProgressJobOrders { get; set; }




    }
}
