//ViewModels / RequisitionCreateVM.cs
using ThothSystemVersion1.Models; // إذا كانت الكلاسات موجودة في مجلد Models
using ThothSystemVersion1.DataTransfereObject; // إذا كانت DTOs موجودة في مجلد DTOs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ThothSystemVersion1.ViewModels
{
    public class RequisitionCreateVM
    {
        public List<JobOrder> JobOrders { get; set; }
        public List<Paper> AvailablePapers { get; set; }
        public List<Ink> AvailableInks { get; set; }
        public List<Supply> AvailableSupplies { get; set; }
        public RequisiteOrderDTO RequisiteOrderDTO { get; set; } = new RequisiteOrderDTO();
    }
}









