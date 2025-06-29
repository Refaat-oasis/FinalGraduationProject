
using System.ComponentModel.DataAnnotations;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.ModifiedModels;
namespace ThothSystemVersion1.ViewModels
{
    public class PurchaseOrderSpecificationsViewModel
    {
        [Display(Name = "رقم امر الشراء")]
        public int PurchaseId { get; set; }

        [Display(Name = "تاريخ الشراء")]
        public DateOnly? PurchaseDate { get; set; }

        [Display(Name = "المبلغ المتبقي")]
        public decimal? RemainingAmount { get; set; }

        [Display(Name = "المبلغ المدفوع")]
        public decimal? PaidAmount { get; set; }

        [Display(Name = "ملاحظات")]
        public string? PurchaseNotes { get; set; }

        [Display(Name = "الرقم القومي للموظف")]
        public string? EmployeeId { get; set; }

        [Display(Name = "رقم التعريفي الخاص بالتاجر")]
        public int? VendorId { get; set; }
        [Display(Name = "اسم المورد")]
        public string VendorName { get; set; } = null!;
        [Display(Name = "الكميه المشتراه")]
        public int quantityPurchased { get; set; }

        [Display(Name = "الكميه المرتجعه")]
        public int returnedQuantity { get; set; }
        public List<Ink> Inks { get; set; }
        public List<Paper> Papers { get; set; }
        public List<Supply> Supplies { get; set; }
        public List<Employee> Employees { get; set; }

        public List<SparePart> SpareParts { get; set; }
        public List<QuantityBridge> QuantityBridge { get; set; }
        public List<ReturnsOrder> ReturnOrder { get; set; }


    }
}